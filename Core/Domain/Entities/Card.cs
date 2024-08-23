using Shared.Requests;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace OnKanBan.Domain.Entities
{
    public class Card
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string? Description { get; set; }
        public int Position { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
        public DateTime? DateInit { get; set; }
        public DateTime? DateCompleted { get; set; }
        public StatusEnum? Status { get; set; } = StatusEnum.ToDo;

        public string ListaId { get; set; }
        public Lista Lista { get; set; }

        public ICollection<Comments>? Comments { get; set; }

        public Card() { }

        public Card(CardRequest request) {
            
            this.Title = request.Title;
            this.Description = request.Description;
            this.Position = request.Position;
            this.ListaId = request.ListaId;
        }

        public CardResponse Convert() => new CardResponse
        {
            Id = this.Id,
            Title = this.Title,
            Description = this.Description,
            Position = this.Position,
            CreatedAt = this.CreatedAt,
            LastUpdatedAt = this.LastUpdatedAt,
            DateInit = this.DateInit,
            DateCompleted = this.DateCompleted,
            Status = this.Status.ToString()
        };

    }
}
