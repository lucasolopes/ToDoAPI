using OnKanBan.Domain.Entities;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IListaRepository 
    {
        Task<Lista> GetByIdAsync(string id);
        Task<Lista> CreateAsync(Lista listaRequest);
        Task<Lista> UpdateAsync(string id,Lista listaRequest);
        Task<bool> ExistsAsync(string id);
        Task DeleteAsync(string id);
    }
}
