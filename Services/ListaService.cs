using Domain.Repositories;
using FluentValidation;
using OnKanBan.Domain.Entities;
using Services.Abstractions;
using Shared.Requests;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validators;

namespace Services
{
    internal class ListaService : IListaService
    {
        private readonly IRepositoryManager _repositoryManager;

        public ListaService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<ListaResponse> GetByIdAsync(string id)
        {
            var lista = await _repositoryManager.ListaRepository().GetByIdAsync(id);

            if(lista == null)
                throw new KeyNotFoundException("Lista not found");

            return lista.Convert();
        }


        public async Task<ListaResponse> CreateAsync(ListaRequest listaRequest)
        {
            var validator = new ListaValidator();
            var validationResult = validator.Validate(listaRequest);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            Lista lista = await _repositoryManager.ListaRepository().CreateAsync(new Lista(listaRequest));

            return lista.Convert();
        }

        public async Task<ListaResponse> UpdateAsync(string id, ListaRequest listaRequest)
        {
            ExistAsync(id);

            Lista? lista = await _repositoryManager.ListaRepository().UpdateAsync(id, new Lista(listaRequest));

            return lista == null ? null : lista.Convert();
        }

        public async Task DeleteAsync(string id)
        {
            await ExistAsync(id);

            await _repositoryManager.ListaRepository().DeleteAsync(id);
        }

        private async Task ExistAsync(string id)
        {
            if (!(await _repositoryManager.ListaRepository().ExistsAsync(id)))
                throw new KeyNotFoundException("Lista not found");
        }
    }
}
