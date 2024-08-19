using Domain.Repositories;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        //fazer isso para todas as classes service para evitar a criação de instâncias desnecessárias
        private readonly Lazy<IWhiteBoardService> whiteBoardService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            whiteBoardService = new Lazy<IWhiteBoardService>(() => new WhiteBoardService(repositoryManager));
        }

        public IWhiteBoardService WhiteBoardService() => whiteBoardService.Value;

    }
}
