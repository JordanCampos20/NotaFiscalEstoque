using Microsoft.EntityFrameworkCore;
using NotaFiscalEstoque.API.DTOs;
using NotaFiscalEstoque.API.Interfaces;
using NotaFiscalEstoque.Application.Interfaces;
using NotaFiscalEstoque.Application.Services;

namespace NotaFiscalEstoque.API.Services
{
    public class EstoqueService(IServiceScopeFactory serviceScopeFactory) : IEstoqueService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

        public bool ValidarEAtualizarEstoque(NotaEstoqueDTO notaEstoque)
        {
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                IProdutoService _produtoService = scope.ServiceProvider.GetRequiredService<IProdutoService>();

                foreach (var item in notaEstoque.Produtos)
                {
                    var produto = _produtoService.GetById(item.ProdutoId);

                    if (produto == null || produto.Saldo < item.Quantidade)
                        return false;
                }

                foreach (var item in notaEstoque.Produtos)
                {
                    var produto = _produtoService.GetById(item.ProdutoId);

                    if (produto == null)
                        return false;

                    produto.Saldo -= item.Quantidade;

                    _produtoService.Update(item.ProdutoId, produto);
                }

                return true;
            }
        }
    }
}
