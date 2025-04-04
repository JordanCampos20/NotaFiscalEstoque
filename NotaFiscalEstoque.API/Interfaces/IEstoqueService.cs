using NotaFiscalEstoque.API.DTOs;

namespace NotaFiscalEstoque.API.Interfaces
{
    public interface IEstoqueService
    {
        bool ValidarEAtualizarEstoque(NotaEstoqueDTO notaEstoque);
    }
}
