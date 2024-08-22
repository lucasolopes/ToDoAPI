using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using OnKanBan.Domain.Entities;
using OnKanBan.Persistence;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    internal class ListaRepository : IListaRepository
    {
        private readonly RepositoryDbContext _context;

        public ListaRepository(RepositoryDbContext context) => _context = context;

        public async Task<Lista?> GetByIdAsync(string id) { 
        
            return await _context.Lista.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Lista?> CreateAsync(Lista listaRequest)
        {
            await _context.Lista.AddAsync(listaRequest);
            await _context.SaveChangesAsync();
            return _context.Lista.AsNoTracking().FirstOrDefault(l => l.Name == listaRequest.Name);
        }

        public async Task<Lista?> UpdateAsync(string id, Lista listaRequest)
        {
            var exist = await GetByIdAsync(id);
            if(exist == null) return null;

            listaRequest.Id = id;
            _context.Entry(listaRequest).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return listaRequest;
        }

        public Task<bool> ExistsAsync(string id)
        {
            return _context.Lista.AnyAsync(l => l.Id == id);
        }

        public async Task DeleteAsync(string id)
        {
            var lista = await GetByIdAsync(id);
            if (lista != null)
            {
                _context.Lista.Remove(lista);
                _context.SaveChanges();
            }
        }
    }
}
