﻿using Domain.Repositories;
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

        public async Task UpdateNameAsync(string id, CardPutNameRequest cardPutNameRequest)
        {
            var validator = new CardPutNameValidator();
            var validationResult = await validator.ValidateAsync(cardPutNameRequest);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await ExistsAsync(id);

            await _repositoryManager.CardRepository().UpdateNameAsync(id, new Card(cardPutNameRequest));
        }

        public async Task UpdateDescriptionAsync(string id, CardPutDescriptionRequest cardPutDescriptionRequest)
        {
            var validator = new CardPutDescriptionValidator();
            var validationResult = await validator.ValidateAsync(cardPutDescriptionRequest);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await ExistsAsync(id);

            await _repositoryManager.CardRepository().UpdateDescriptionAsync(id, new Card(cardPutDescriptionRequest));
        }

        public async Task UpdatePositionAsync(string id, CardPutPositionRequest cardPutPositionRequest)
        {
            var validator = new CardPutPositionValidator();
            var validationResult = await validator.ValidateAsync(cardPutPositionRequest);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await ExistsAsync(id);

            await _repositoryManager.CardRepository().UpdatePositionAsync(id, new Card(cardPutPositionRequest));
        }

        public async Task UpdateDateInitAsync(string id, CardPutDateInitRequest cardPutDateInitRequest)
        {
            var validator = new CardPutDateInitValidator();
            var validationResult = validator.Validate(cardPutDateInitRequest);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await ExistsAsync(id);

            var oldCard = await _repositoryManager.CardRepository().GetByIdAsync(id);

            if (oldCard.DateCompleted != null && cardPutDateInitRequest.Date > oldCard.DateCompleted)
                throw new ValidationException("DateInit cannot be after Complete");

            await _repositoryManager.CardRepository().UpdateDateInitAsync(id, new Card(cardPutDateInitRequest));
        }

        public async Task UpdateDateCompleteAsync(string id, CardPutDateCompleteRequest cardPutDateCompleteRequest)
        {
            var validator = new CardPutDateCompleteValidator();
            var validationResult = validator.Validate(cardPutDateCompleteRequest);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await ExistsAsync(id);

            var oldCard = await _repositoryManager.CardRepository().GetByIdAsync(id);

            if (oldCard.DateInit != null && cardPutDateCompleteRequest.Date < oldCard.DateInit)
                throw new ValidationException("DateComplete cannot be before Init");

            await _repositoryManager.CardRepository().UpdateDateCompleteAsync(id, new Card(cardPutDateCompleteRequest));

        }

        public async Task UpdateStatusAsync(string id, CardPutStatusRequest cardPutStatusRequest)
        {
            var validator = new CardPutStatusValidator();
            var validationResult = await validator.ValidateAsync(cardPutStatusRequest);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await ExistsAsync(id);

            await _repositoryManager.CardRepository().UpdateStatusAsync(id, new Card(cardPutStatusRequest));
        }

        private async Task ExistsAsync(string id)
        {
            if ( ! (await _repositoryManager.CardRepository().ExistsAsync(id)) )
                throw new KeyNotFoundException("Card not found");
        }
    }
}
