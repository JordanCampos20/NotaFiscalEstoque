using AutoMapper;
using NotaFiscalEstoque.Application.DTOs;
using NotaFiscalEstoque.Domain.Entities;

namespace NotaFiscalEstoque.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
        }
    }
}
