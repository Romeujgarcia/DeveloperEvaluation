// MappingProfile.cs
using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Models; // Supondo que você tenha modelos de domínio
using Ambev.DeveloperEvaluation.Domain.Entities; // Supondo que você tenha suas entidades de domínio

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Cart, CartDto>().ReverseMap(); // Mapeamento entre Cart e CartDto
        CreateMap<CartProduct, CartProductDto>().ReverseMap(); // Mapeamento entre CartProduct e CartProductDto
    }
}