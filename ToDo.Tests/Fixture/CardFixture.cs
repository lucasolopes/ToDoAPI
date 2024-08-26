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
                Status = StatusEnum.ToDo,
                DateCompleted = new DateTime(2024, 4, 4),
                DateInit = new DateTime(2024, 3, 3),
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
            Status = StatusEnum.Done,
            DateCompleted = new DateTime(2024, 4, 4),
            DateInit = new DateTime(2024, 3, 3),
        };

        internal static CardResponse GetCardResponse() => new CardResponse
        {
            Id = "1",
            Title = "Test Card",
            Description = "Test Description",
            CreatedAt = new DateTime(2024, 1, 1),
            LastUpdatedAt = new DateTime(2024, 1, 1),
            DateCompleted = new DateTime(2024, 4, 4),
            DateInit = new DateTime(2024, 3, 3),
            Position = 1,
            Status = StatusEnum.ToDo.ToString(),
        };

        internal static CardResponse PutCardResponse() => new CardResponse
        {
            Id = "1",
            Title = "Test Card Update",
            Description = "Test Description Update",
            CreatedAt = new DateTime(2024, 2, 2),
            LastUpdatedAt = new DateTime(2024, 2, 2),
            DateCompleted = new DateTime(2024, 4, 4),
            DateInit = new DateTime(2024, 3, 3),
            Position = 2,
            Status = StatusEnum.Done.ToString(),
        };

        internal static CardPutNameRequest PutCardNameRequest() => new CardPutNameRequest
        {
            Title = "Test Card Update",
        };

        internal static CardPutDescriptionRequest PutCardDescriptionRequest() => new CardPutDescriptionRequest
        {
            Description = "Test Description Update",
        };

        internal static CardPutPositionRequest PutCardPositionRequest() => new CardPutPositionRequest
        {
            Position = 2,
        };

        internal static CardPutDateInitRequest PutCardDateInitRequest() => new CardPutDateInitRequest
        {
            Date = new DateTime(2024, 3, 3)
        };

        internal static CardPutDateInitRequest PutCardDateInitRequestInvalid() => new CardPutDateInitRequest
        {
            Date = new DateTime(2024, 5, 5)
        };

        internal static CardPutDateCompleteRequest PutCardDateCompleteRequest() => new CardPutDateCompleteRequest
        {
            Date = new DateTime(2024, 5, 5)
        };

        internal static CardPutDateCompleteRequest PutCardDateCompleteRequestInvalid() => new CardPutDateCompleteRequest
        {
            Date = new DateTime(2024, 2, 2)
        };

        internal static CardPutStatusRequest PutCardStatusRequest() => new CardPutStatusRequest
        {
            Status = (int) StatusEnum.Done
        };

        internal static CardPutStatusRequest PutCardStatusRequestInvalid() => new CardPutStatusRequest
        {
            Status = 4
        };
    }
}
