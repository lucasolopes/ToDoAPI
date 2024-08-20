using Shared.Requests;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IWhiteBoardService
    {
        Task<WhiteBoardResponse?> CreateAsync(WhiteBoardRequest whiteBoardRequest);
        Task<bool?> DeleteAsync(string id);
        Task<WhiteBoardResponse?> GetByIdAsync(string id);
        Task<WhiteBoardResponse?> UpdateAsync(string id,WhiteBoardRequest whiteBoardRequest);
    }
}
