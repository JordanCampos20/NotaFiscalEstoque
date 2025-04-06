using Confluent.Kafka;
using NotaFiscalEstoque.API.Interfaces;
using System.Text.Json;

namespace NotaFiscalEstoque.API.Services
{
    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly IProducer<string, string> _producer = new 
            ProducerBuilder<string, string>(new ProducerConfig()
            {
                BootstrapServers = Environment.GetEnvironmentVariable("CONEXAO_KAFKA"),
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = Environment.GetEnvironmentVariable("USERNAME_KAFKA"),
                SaslPassword = Environment.GetEnvironmentVariable("PASSWORD_KAFKA"),
                MessageTimeoutMs = 45000,
                ClientId = Environment.GetEnvironmentVariable("CLIENTID_KAFKA")
            }).Build();

        public async Task EnviarNota(string topic, object nota)
        {
            var message = JsonSerializer.Serialize(nota);

            await _producer.ProduceAsync(topic, new Message<string, string> { Key = Guid.NewGuid().ToString(), Value = message });
        }
    }
}
