using OnKanBan.Domain.Entities;
using Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Tests.Fixture
{
    internal static class CardFixture
    {
        internal static Card GetCard() => new Card
            {
                Id = "1",
                Title = "Test Card",
                Description = "Test Description",
                CreatedAt = new DateTime(2024, 1, 1),
                LastUpdatedAt = new DateTime(2024, 1, 1),
                ListaId = "1",
                Lista = ListaFixture.GetLista(),
                Position = 1,
                Status = StatusEnum.ToDo
            };
       

        internal static CardRequest GetCardRequest() => new CardRequest
            {
                Title = "Test Card",
                Description = "Test Description",
                ListaId = "1",
                Position = 1,
            };
        

        internal static Card PutCard() => new Card
        {
            Id = "1",
            Title = "Test Card Update",
            Description = "Test Description Update",
            CreatedAt = new DateTime(2024, 2, 2),
            LastUpdatedAt = new DateTime(2024, 2, 2),
            ListaId = "1",
            Lista = ListaFixture.GetLista(),
            Position = 2,
            Status = StatusEnum.Done
        };
    }
}
