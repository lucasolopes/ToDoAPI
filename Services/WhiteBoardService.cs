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
            var whiteBoard = await _repositoryManager.WhiteBoardRepository().GetByIdAsync(id);
            if(whiteBoard == null)
                throw new KeyNotFoundException("WhiteBoard not found");



            return whiteBoard.Convert();
        }

        public async Task<WhiteBoardResponse?> UpdateAsync(string id,WhiteBoardRequest whiteBoardRequest)
        {
            if(!await _repositoryManager.WhiteBoardRepository().ExistsAsync(id))
                throw new KeyNotFoundException("WhiteBoard not found");

            WhiteBoard? whiteBoard = await _repositoryManager.WhiteBoardRepository().UpdateAsync(id,new WhiteBoard(whiteBoardRequest));

            return whiteBoard == null ? null : whiteBoard.Convert();
        }

        public async Task DeleteAsync(string id)
        {
            if (!await _repositoryManager.WhiteBoardRepository().ExistsAsync(id))
                throw new KeyNotFoundException("WhiteBoard not found");

             await _repositoryManager.WhiteBoardRepository().DeleteAsync(id);
        }



    }
}
