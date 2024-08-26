using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using OnKanBan.Domain.Entities;
using OnKanBan.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    internal sealed class CardRepository : ICardRepository
    {
        private readonly RepositoryDbContext _context;

        public CardRepository(RepositoryDbContext context) => _context = context;

        public async Task<Card> CreateAsync(Card card)
        {
            await _context.AddAsync(card);
            await _context.SaveChangesAsync();
            return _context.Cards.FirstOrDefault(c => c.Title == card.Title);
        }

        public async Task<Card> GetByIdAsync(string id)
        {
            Card card = await _context.Cards.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            return card;
        }

        public async Task<Card> UpdateAsync(string id, Card card)
        {
            card.Id = id;
            _context.Entry(card).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return card;
        }

        public Task<bool> ExistsAsync(string id)
        {
            return _context.Cards.AsNoTracking().AnyAsync(c => c.Id == id);
        }

        public async Task DeleteAsync(string id)
        {
            var card = await GetByIdAsync(id);
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNameAsync(string id, Card card)
        {
            var cardOld = await GetByIdAsync(id);
            cardOld.Title = card.Title;
            cardOld.LastUpdatedAt = card.LastUpdatedAt;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDescriptionAsync(string id, Card card)
        {
            var cardOld = await GetByIdAsync(id);
            cardOld.Description = card.Description;
            cardOld.LastUpdatedAt = card.LastUpdatedAt;
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePositionAsync(string id, Card card)
        {
            var cardOld = await GetByIdAsync(id);
            cardOld.Position = card.Position;
            cardOld.LastUpdatedAt = card.LastUpdatedAt;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDateInitAsync(string id, Card card)
        {
            var cardOld = await GetByIdAsync(id);
            cardOld.DateInit = card.DateInit;
            cardOld.LastUpdatedAt = card.LastUpdatedAt;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDateCompleteAsync(string id, Card card)
        {
            var cardOld = await GetByIdAsync(id);
            cardOld.DateCompleted = card.DateCompleted;
            cardOld.LastUpdatedAt = card.LastUpdatedAt;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(string id, Card card)
        {
            var cardOld = await GetByIdAsync(id);
            cardOld.Status = card.Status;
            cardOld.LastUpdatedAt = card.LastUpdatedAt;
            await _context.SaveChangesAsync();
        }

       /* public Task UpdateListAsync(string id, Card card)
        {
            var cardOld = GetByIdAsync(id);
        }*/
    }
}
