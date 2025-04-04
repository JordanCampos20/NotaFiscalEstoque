using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotaFiscalEstoque.Domain.Entities
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string Nome { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }
        [Required]
        public int Saldo { get; set; }
        [StringLength(300)]
        public string Descricao { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow.AddHours(-3);
    }
}
