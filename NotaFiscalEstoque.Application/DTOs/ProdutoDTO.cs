namespace NotaFiscalEstoque.Application.DTOs
{
    public class ProdutoDTO
    {
        public int? Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Saldo { get; set; }
        public string Descricao { get; set; } = string.Empty;
    }
}
