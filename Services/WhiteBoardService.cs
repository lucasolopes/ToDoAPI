using Domain.Repositories;
using OnKanBan.Domain.Entities;
using Services.Abstractions;
using Shared.Requests;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class WhiteBoardService : IWhiteBoardService
    {
        private readonly IRepositoryManager _repositoryManager;

        public WhiteBoardService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<WhiteBoardResponse?> CreateAsync(WhiteBoardRequest whiteBoardRequest)
        {
            WhiteBoard? whiteBoard = await _repositoryManager.WhiteBoardRepository()
            .CreateAsync(new WhiteBoard(whiteBoardRequest));
            return whiteBoard == null ? null : whiteBoard.Convert();
        }

        public async Task<WhiteBoardResponse?> GetByIdAsync(string id)
        {
            WhiteBoardExist(id);

            WhiteBoard? whiteBoard = await _repositoryManager.WhiteBoardRepository().GetByIdAsync(id);

            return whiteBoard == null ? null : whiteBoard.Convert();
        }

        public async Task<WhiteBoardResponse?> UpdateAsync(string id,WhiteBoardRequest whiteBoardRequest)
        {
            //WhiteBoardExist(id);

            WhiteBoard? whiteBoard = await _repositoryManager.WhiteBoardRepository().UpdateAsync(id,new WhiteBoard(whiteBoardRequest));

            return whiteBoard == null ? null : whiteBoard.Convert();
        }

        public async Task<bool?> DeleteAsync(string id)
        {
            WhiteBoardExist(id);

            return await _repositoryManager.WhiteBoardRepository().DeleteAsync(id);
        }

        private async void WhiteBoardExist(string id)
        {
            if(await _repositoryManager.WhiteBoardRepository().GetByIdAsync(id) == null)
                throw new Exception("WhiteBoard Nao Exite!");
        }

    }
}
