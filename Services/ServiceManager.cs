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
        private readonly Lazy<IListaService> listaService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            whiteBoardService = new Lazy<IWhiteBoardService>(() => new WhiteBoardService(repositoryManager));
            listaService = new Lazy<IListaService>(() => new ListaService(repositoryManager));
        }

        public IWhiteBoardService WhiteBoardService() => whiteBoardService.Value;
        public IListaService ListaService() => listaService.Value;

    }
}
