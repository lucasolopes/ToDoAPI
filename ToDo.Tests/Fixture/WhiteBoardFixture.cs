using OnKanBan.Domain.Entities;
using Shared.Requests;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Tests.Fixture
{
    internal static class WhiteBoardFixture
    {
        internal static WhiteBoard GetWhiteBoard()
        {
            return new WhiteBoard
            {
                Id = "1",
                Name = "Test WhiteBoard",
                Description = "Test Description",
                CreatedAt = new DateTime(2024, 1, 1),
                LastUpdatedAt = new DateTime(2024, 1, 1),
            };
        }

        internal static WhiteBoard PutWhiteBoard()
        {
            return new WhiteBoard
            {
                Id = "1",
                Name = "Test WhiteBoard Updated",
                Description = "Test Description Updated",
                CreatedAt = new DateTime(2024, 1, 1),
                LastUpdatedAt = new DateTime(2024, 2, 2),
            };
        }

        internal static WhiteBoardRequest GetWhiteBoardRequest()
        {
            return new WhiteBoardRequest
            {
                Name = "Test WhiteBoard",
                Description = "Test Description",
            };
        }

        
        internal static WhiteBoardResponse GetWhiteBoardResponse() => new WhiteBoardResponse
        {
            Id = "1",
            Name = "Test WhiteBoard",
            Description = "Test Description",
            CreatedAt = new DateTime(2024, 1, 1),
            LastUpdatedAt = new DateTime(2024, 1, 1)
        };

        internal static WhiteBoardResponse PutWhiteBoardResponse() => new WhiteBoardResponse
        {
            Id = "1",
            Name = "Test WhiteBoard Updated",
            Description = "Test Description Updated",
            CreatedAt = new DateTime(2024, 1, 1),
            LastUpdatedAt = new DateTime(2024, 2, 2)
        };


        internal static WhiteBoardPutNameRequest PutWhiteBoardNameRequest() => new WhiteBoardPutNameRequest
        {
            Name = "Test WhiteBoard Updated"
        };


        internal static WhiteBoardPutDescriptionRequest PutWhiteBoardDescriptionRequest() => new WhiteBoardPutDescriptionRequest
        {
            Description = "Test Description Updated"
        };

    }
}
