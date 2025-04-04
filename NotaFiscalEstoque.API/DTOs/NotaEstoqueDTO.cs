namespace NotaFiscalEstoque.API.DTOs
{
    public class NotaEstoqueDTO
    {
        public int? Id { get; set; }
        public int Status { get; set; }
        public List<ItemEstoqueDTO> Produtos { get; set; } = [];
    }
}
