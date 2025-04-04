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
                BootstrapServers = "localhost:9092"
            }).Build();

        public async Task EnviarNota(string topic, object nota)
        {
            var message = JsonSerializer.Serialize(nota);

            await _producer.ProduceAsync(topic, new Message<string, string> { Key = Guid.NewGuid().ToString(), Value = message });
        }
    }
}
