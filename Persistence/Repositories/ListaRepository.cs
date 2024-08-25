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
    internal sealed class ListaRepository : IListaRepository
    {
        private readonly RepositoryDbContext _context;

        public ListaRepository(RepositoryDbContext context) => _context = context;

        public async Task<Lista> GetByIdAsync(string id) { 
        
            return await _context.Lista.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Lista> CreateAsync(Lista listaRequest)
        {
            await _context.Lista.AddAsync(listaRequest);
            await _context.SaveChangesAsync();
            return _context.Lista.AsNoTracking().FirstOrDefault(l => l.Name == listaRequest.Name);
        }

        public async Task<Lista> UpdateAsync(string id, Lista listaRequest)
        {
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
            Lista lista = await GetByIdAsync(id);

            _context.Lista.Remove(lista);
            _context.SaveChanges();
        }

        public async Task UpdateNameAsync(string id, Lista listaRequest)
        {
            Lista listaOld = await GetByIdAsync(id);
            listaOld.Name = listaRequest.Name;
            _context.Entry(listaOld).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePositionAsync(string id, Lista listaRequest)
        {
            Lista listaOld = await GetByIdAsync(id);
            listaOld.Position = listaRequest.Position;
            _context.Entry(listaOld).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
