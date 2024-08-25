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
            await ExistAsync(id);

            var lista = await _repositoryManager.ListaRepository().GetByIdAsync(id);

            return lista.Convert();
        }


        public async Task<ListaResponse> CreateAsync(ListaRequest listaRequest)
        {
            var validator = new ListaValidator();
            var validationResult =  await validator.ValidateAsync(listaRequest);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            Lista lista = await _repositoryManager.ListaRepository().CreateAsync(new Lista(listaRequest));

            return lista.Convert();
        }

        public async Task<ListaResponse> UpdateAsync(string id, ListaRequest listaRequest)
        {
            var validator = new ListaValidator();
            var validationResult = await validator.ValidateAsync(listaRequest);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await ExistAsync(id);

            Lista lista = await _repositoryManager.ListaRepository().UpdateAsync(id, new Lista(listaRequest));

            return lista.Convert();
        }

        public async Task DeleteAsync(string id)
        {
            await ExistAsync(id);

            await _repositoryManager.ListaRepository().DeleteAsync(id);
        }

        public async Task UpdateNameAsync(string id, ListaPutNameRequest listaPutNameRequest)
        {
            var validator = new ListaPutNameValidator();
            var validationResult = await validator.ValidateAsync(listaPutNameRequest);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await ExistAsync(id);

            await _repositoryManager.ListaRepository().UpdateNameAsync(id, new Lista(listaPutNameRequest));


        }

        public async Task UpdatePositionAsync(string id, ListaPutPositionRequest listaPutPositionRequest)
        {
            var validator = new ListaPutPositionValidator();
            var validationResult = await validator.ValidateAsync(listaPutPositionRequest);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await ExistAsync(id);

            await _repositoryManager.ListaRepository().UpdatePositionAsync(id, new Lista(listaPutPositionRequest));
        }

        private async Task ExistAsync(string id)
        {
            if (!(await _repositoryManager.ListaRepository().ExistsAsync(id)))
                throw new KeyNotFoundException("Lista not found");
        }

   
    }
}
