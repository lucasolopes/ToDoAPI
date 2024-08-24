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
    internal class WhiteBoardService : IWhiteBoardService
    {
        private readonly IRepositoryManager _repositoryManager;

        public WhiteBoardService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<WhiteBoardResponse> CreateAsync(WhiteBoardRequest whiteBoardRequest)
        {
            var validator = new WhiteBoardValidator();
            var validationResult = await validator.ValidateAsync(whiteBoardRequest);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            WhiteBoard whiteBoard = await _repositoryManager.WhiteBoardRepository()
            .CreateAsync(new WhiteBoard(whiteBoardRequest));

            return whiteBoard.Convert();
        }

        public async Task<WhiteBoardResponse> GetByIdAsync(string id)
        {
            await ExistsAsync(id);

            var whiteBoard = await _repositoryManager.WhiteBoardRepository().GetByIdAsync(id);

            return whiteBoard.Convert();
        }

        public async Task<WhiteBoardResponse?> UpdateAsync(string id,WhiteBoardRequest whiteBoardRequest)
        {
            var validator = new WhiteBoardValidator();
            var validationResult = await validator.ValidateAsync(whiteBoardRequest);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await ExistsAsync(id);

            WhiteBoard whiteBoard = await _repositoryManager.WhiteBoardRepository().UpdateAsync(id,new WhiteBoard(whiteBoardRequest));

            return whiteBoard.Convert();
        }

        public async Task DeleteAsync(string id)
        {
             await ExistsAsync(id);

             await _repositoryManager.WhiteBoardRepository().DeleteAsync(id);
        }

        private async Task ExistsAsync(string id)
        {
            if (!(await _repositoryManager.WhiteBoardRepository().ExistsAsync(id)))
                throw new KeyNotFoundException("WhiteBoard not found");
        }

    }
}
