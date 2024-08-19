using Domain.Repositories;
using Services.Abstractions;
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
    }
}
