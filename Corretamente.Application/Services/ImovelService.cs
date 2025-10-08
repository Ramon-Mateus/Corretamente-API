using AutoMapper;
using Corretamente.Application.DTOs.Imovel;
using Corretamente.Application.Interfaces.Services;
using Corretamente.Domain.Entities;
using Corretamente.Domain.Interfaces.Repositories;

namespace Corretamente.Application.Services
{
    public class ImovelService : IImovelService
    {
        private readonly IImovelRepository _imovelRepository;
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ImovelService(
            IImovelRepository imovelRepository,
            IClienteService clienteService,
            IMapper mapper)
        {
            _imovelRepository = imovelRepository;
            _clienteService = clienteService;
            _mapper = mapper;
        }

        public async Task<ImovelDTO?> GetByIdAsync(int id)
        {
            var imovel = await _imovelRepository.GetByIdAsync(id);

            if (imovel == null) return null;

            return _mapper.Map<ImovelDTO>(imovel);
        }

        public async Task<IEnumerable<ImovelDTO>> GetAllAsync()
        {
            var imoveis = await _imovelRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ImovelDTO>>(imoveis);
        }

        public async Task<ImovelDTO> CreateAsync(CreateImovelDTO imovelDto)
        {
            await VerificarClienteExistente(imovelDto.LocatarioId);
            await VerificarClienteExistente(imovelDto.ProprietarioId);

            var imovel = _mapper.Map<Imovel>(imovelDto);
            await _imovelRepository.CreateAsync(imovel);
            return _mapper.Map<ImovelDTO>(imovel);
        }

        public async Task<ImovelDTO?> UpdateAsync(int id, UpdateImovelDTO imovelDto)
        {
            var existingImovel = await _imovelRepository.GetByIdAsync(id);

            await VerificarClienteExistente(imovelDto.LocatarioId);
            await VerificarClienteExistente(imovelDto.ProprietarioId);

            if (existingImovel == null) return null;

            _mapper.Map(imovelDto, existingImovel);
            await _imovelRepository.UpdateAsync(existingImovel);

            return _mapper.Map<ImovelDTO>(existingImovel);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var imovel = await _imovelRepository.GetByIdAsync(id);

            if (imovel == null) return false;

            await _imovelRepository.DeleteAsync(id);
            return true;
        }

        public async Task VerificarClienteExistente(int? clienteId)
        {
            if (clienteId.HasValue)
            {
                var existingCliente = await _clienteService.GetByIdAsync(clienteId.Value);
                if (existingCliente == null)
                {
                    throw new KeyNotFoundException($"Cliente com ID {clienteId.Value} não foi encontrado.");
                }
            }
        }
    }
}
