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
        Task UpdateNameAsync(string id, Card card);
        Task UpdateDescriptionAsync(string id, Card card);
        Task UpdatePositionAsync(string id, Card card);
        Task UpdateDateInitAsync(string id, Card card);
        Task UpdateDateCompleteAsync(string id, Card card);
        Task UpdateStatusAsync(string id, Card card);
       // Task UpdateListAsync(string id, Card card);
    }
}
