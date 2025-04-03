using Microsoft.EntityFrameworkCore;
using NotaFiscalEstoque.Domain.Entities;

namespace NotaFiscalEstoque.Infrastructure.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Produto> Produtos { get; init; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
