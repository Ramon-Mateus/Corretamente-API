using AutoMapper;
using Corretamente.Application.DTOs.Cliente;
using Corretamente.Domain.Entities;

namespace Corretamente.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Cliente, ClienteDTO>();
        CreateMap<CreateClienteDTO, Cliente>();
        CreateMap<UpdateClienteDTO, Cliente>();
    }
}