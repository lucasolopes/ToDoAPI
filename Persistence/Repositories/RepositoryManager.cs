using Domain.Repositories;
using OnKanBan.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {

        //fazer isso para todas as classes de repositório para evitar a criação de instâncias desnecessárias
        private readonly Lazy<IWhiteBoardRepository> _whiteBoardRepository;
        private readonly Lazy<IListaRepository> _listaRepository;
        private readonly Lazy<ICardRepository> _cardRepository;
        

        public RepositoryManager(RepositoryDbContext context)
        {
            _whiteBoardRepository = new Lazy<IWhiteBoardRepository>(() => new WhiteBoardRepository(context));
            _listaRepository = new Lazy<IListaRepository>(() => new ListaRepository(context));
            _cardRepository = new Lazy<ICardRepository>(() => new CardRepository(context));
        }

        public IWhiteBoardRepository WhiteBoardRepository() => _whiteBoardRepository.Value;
        public IListaRepository ListaRepository() => _listaRepository.Value;
        public ICardRepository CardRepository() => _cardRepository.Value;    
    }
}
