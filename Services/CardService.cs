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
    internal class CardService : ICardService
    {
        private readonly IRepositoryManager _repositoryManager;

        public CardService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<CardResponse> CreateAsync(CardRequest cardRequest)
        {
            var validator = new CardValidator();
            var validationResult = validator.Validate(cardRequest);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            Card card = await _repositoryManager.CardRepository()
                .CreateAsync(new Card(cardRequest));

            return card.Convert();
        }

        public async Task<CardResponse> GetByIdAsync(string id)
        {
            await ExistsAsync(id);

            var card = await _repositoryManager.CardRepository().GetByIdAsync(id);

            return card.Convert();
        }

        public async Task<CardResponse> UpdateAsync(string id, CardRequest cardRequest)
        {
            var validator = new CardValidator();
            var validationResult = validator.Validate(cardRequest);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await ExistsAsync(id);

            Card card = await _repositoryManager.CardRepository().UpdateAsync(id, new Card(cardRequest));

            return card.Convert();
        }

        public async Task DeleteAsync(string id)
        {
            await ExistsAsync(id);
            await _repositoryManager.CardRepository().DeleteAsync(id);
        }

        private async Task ExistsAsync(string id)
        {
            if ( ! (await _repositoryManager.CardRepository().ExistsAsync(id)) )
                throw new KeyNotFoundException("Card not found");
        }

    }
}
