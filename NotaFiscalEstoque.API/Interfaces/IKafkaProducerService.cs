namespace NotaFiscalEstoque.API.Interfaces
{
    public interface IKafkaProducerService
    {
        Task EnviarNota(string topic, object nota);
    }
}
