using Shared.Requests;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface ICardService
    {
        Task<CardResponse> CreateAsync(CardRequest cardRequest);
        Task<CardResponse> GetByIdAsync(string id);
        Task<CardResponse> UpdateAsync(string id, CardRequest cardRequest);
        Task DeleteAsync(string id);
    }
}
