using OnKanBan.Domain.Entities;
using Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IWhiteBoardRepository
    {
        Task<WhiteBoard?> CreateAsync(WhiteBoard whiteBoard);
        Task DeleteAsync(string id);
        Task<WhiteBoard?> GetByIdAsync(string id);
        Task<WhiteBoard?> UpdateAsync(string id,WhiteBoard whiteBoard);
        Task<bool> ExistsAsync(string id);
    }
}
