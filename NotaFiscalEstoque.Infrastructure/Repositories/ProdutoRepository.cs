using NotaFiscalEstoque.Domain.Entities;
using NotaFiscalEstoque.Domain.Interfaces;
using NotaFiscalEstoque.Infrastructure.Context;

namespace NotaFiscalEstoque.Infrastructure.Repositories
{
    public class ProdutoRepository(ApplicationDbContext context) : IProdutoRepository
    {
        private readonly ApplicationDbContext _context = context;


        public Produto GetById(int id)
        {
            return _context.Produtos.Find(id)!;
        }

        public IEnumerable<Produto> GetProdutos()
        {
            return [.. _context.Produtos];
        }

        public Produto? Create(Produto produto)
        {
            _context.Produtos.Add(produto);

            if (_context.SaveChanges() > 0)
                return produto;

            return null;
        }

        public Produto? Update(int id, Produto produto)
        {
            if (id != produto.Id)
                throw new Exception("Produto não condiz com o do objeto!");

            _context.Produtos.Update(produto);

            if (_context.SaveChanges() > 0)
                return produto;

            return null;
        }

        public bool Remove(int id)
        {
            Produto? produto = _context.Produtos.Find(id);

            if (produto == null)
                throw new Exception("Produto não encontrado!");

            _context.Produtos.Remove(produto);

            if (_context.SaveChanges() > 0)
                return true;

            return false;
        }
    }
}
