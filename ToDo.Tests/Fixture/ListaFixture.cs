using OnKanBan.Domain.Entities;
using Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Tests.Fixture
{
    internal static class ListaFixture
    {
        internal static Lista GetLista() => new Lista
            {
                Id = "1",
                Name = "Test Lista",
                Position = 1,
                WhiteBoardId = "1",
                WhiteBoard = WhiteBoardFixture.GetWhiteBoard()
            };

        internal static ListaRequest GetListaRequest() => new ListaRequest
            {
                Name = "Test Lista",
                Position = 1,
                WhiteBoardId = "1"
            };


        internal static Lista PutListaRequest() => new Lista
            {
                Id = "1",
                Name = "Test Lista Update",
                Position = 2,
                WhiteBoardId = "1",
            };
        
    }
}
