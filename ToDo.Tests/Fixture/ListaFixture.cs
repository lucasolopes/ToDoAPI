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


        internal static Lista PutLista() => new Lista
            {
                Id = "1",
                Name = "Test Lista Update",
                Position = 2,
                WhiteBoardId = "1",
            };

        internal static ListaResponse GetListaResponse() => new ListaResponse
            {
                Id = "1",
                Name = "Test Lista",
                Position = 1,
                WhiteBoardId = "1"
            };

        internal static ListaResponse PutListaResponse() => new ListaResponse
            {
                Id = "1",
                Name = "Test Lista Update",
                Position = 2,
                WhiteBoardId = "1"
            };
        
        internal static ListaPutNameRequest PutNameListaRequest() => new ListaPutNameRequest
            {
                Name = "Test Lista Update"
            };

        internal static ListaPutPositionRequest PutPositionListaRequest() => new ListaPutPositionRequest
            {
                Position = 2
            };
    }
}
