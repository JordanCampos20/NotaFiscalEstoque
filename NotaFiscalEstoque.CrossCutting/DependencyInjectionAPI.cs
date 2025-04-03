using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotaFiscalEstoque.Application.Interfaces;
using NotaFiscalEstoque.Application.Mappings;
using NotaFiscalEstoque.Application.Services;
using NotaFiscalEstoque.Domain.Interfaces;
using NotaFiscalEstoque.Infrastructure.Context;
using NotaFiscalEstoque.Infrastructure.Repositories;

namespace NotaFiscalEstoque.CrossCutting
{
    public static class DependencyInjectionAPI
    {
        public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("CONEXAO_BANCO")
                ?? Environment.GetEnvironmentVariable("CONEXAO_BANCO_NOTA_FISCAL");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoService, ProdutoService>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }
    }
}
