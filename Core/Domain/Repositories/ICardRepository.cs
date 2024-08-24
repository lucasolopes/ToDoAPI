using OnKanBan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ICardRepository
    {
        Task<Card> CreateAsync(Card card);
        Task<Card> GetByIdAsync(string id);
        Task<Card> UpdateAsync(string id, Card card);
        Task<bool> ExistsAsync(string id);
        Task DeleteAsync(string id);
    }
}
