using Domain.Repositories;
using OnKanBan.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    internal sealed class WhiteBoardRepository : IWhiteBoardRepository
    {
        private readonly RepositoryDbContext _context;

        public WhiteBoardRepository(RepositoryDbContext context) => _context = context;


    }
}
