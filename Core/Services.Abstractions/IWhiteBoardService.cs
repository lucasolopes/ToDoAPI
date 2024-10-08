﻿using Shared.Requests;
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
        Task<WhiteBoardResponse> CreateAsync(WhiteBoardRequest whiteBoardRequest);
        Task DeleteAsync(string id);
        Task<WhiteBoardResponse> GetByIdAsync(string id);
        Task<WhiteBoardResponse> UpdateAsync(string id,WhiteBoardRequest whiteBoardRequest);
        Task UpdateNameAsync(string id,WhiteBoardPutNameRequest whiteBoardPutNameRequest);
        Task UpdateDescriptionAsync(string id,WhiteBoardPutDescriptionRequest whiteBoardPutDescriptionRequest);
    }
}
