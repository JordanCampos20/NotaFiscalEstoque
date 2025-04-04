using Confluent.Kafka;
using NotaFiscalEstoque.API.DTOs;
using NotaFiscalEstoque.API.Interfaces;
using NotaFiscalEstoque.Application.Interfaces;
using NotaFiscalEstoque.Application.Services;
using System.Text.Json;

namespace NotaFiscalEstoque.API.Services
{
    public class KafkaConsumerService(IServiceScopeFactory serviceScopeFactory) : IKafkaConsumerService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

        private readonly IConsumer<string, string> _consumer = new
            ConsumerBuilder<string, string>(new ConsumerConfig()
            {
                GroupId = "estoque-service",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            }).Build();

        private readonly string _topic = "validar-estoque";

        public async void ConsumirNotas()
        {
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                IEstoqueService _estoqueService = scope.ServiceProvider.GetRequiredService<IEstoqueService>();

                IKafkaProducerService _kafkaProducerService = scope.ServiceProvider.GetRequiredService<IKafkaProducerService>();

                _consumer.Subscribe(_topic);

                try
                {
                    while (true)
                    {
                        var consume = _consumer.Consume(CancellationToken.None);

                        var notaEstoque = JsonSerializer.Deserialize<NotaEstoqueDTO>(consume.Message.Value);

                        if (notaEstoque == null)
                            continue;

                        bool estoqueDisponivel = _estoqueService.ValidarEAtualizarEstoque(notaEstoque);

                        string topicoResposta = estoqueDisponivel ? "estoque-validado" : "estoque-insuficiente";

                        await _kafkaProducerService.EnviarNota(topicoResposta, new { NotaId = notaEstoque.Id });

                        Console.WriteLine($"📤 Estoque validado? {estoqueDisponivel}. Mensagem enviada para {topicoResposta}.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro no Consumer: {ex.Message}");
                }
            }
        }
    }
}
