using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        public StatusEnum? Status { get; set; }

        public string ListaId { get; set; }
        public Lista Lista { get; set; }

        public List<Comments>? Comments { get; set; }

    }
}
