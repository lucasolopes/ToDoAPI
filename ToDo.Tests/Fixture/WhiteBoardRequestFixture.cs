using Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Tests.Fixture
{
    internal static class WhiteBoardRequestFixture
    {
        internal static WhiteBoardRequest GetWhiteBoardRequest()
        {
            return new WhiteBoardRequest
            {
                Name = "Test WhiteBoard",
                Description = "Test Description",
            };
        }

    }
}
