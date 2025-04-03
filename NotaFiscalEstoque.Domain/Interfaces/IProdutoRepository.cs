using NotaFiscalEstoque.Domain.Entities;

namespace NotaFiscalEstoque.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        IEnumerable<Produto> GetProdutos();
        Produto? GetById(int id);
        Produto? Create(Produto produto);
        Produto? Update(int id, Produto produto);
        bool Remove(int id);
    }
}
