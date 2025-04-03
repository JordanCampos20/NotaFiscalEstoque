using NotaFiscalEstoque.Application.DTOs;

namespace NotaFiscalEstoque.Application.Interfaces
{
    public interface IProdutoService
    {
        IEnumerable<ProdutoDTO> GetProdutos();
        ProdutoDTO? GetById(int id);
        ProdutoDTO? Create(ProdutoDTO produtoDTO);
        ProdutoDTO? Update(int id, ProdutoDTO produtoDTO);
        bool Remove(int id);
    }
}
