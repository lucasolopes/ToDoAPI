using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnKanBan.Domain.Entities
{
    public class Comments
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string CardId { get; set; }
        public Card Card { get; set; }

        //public string UserId { get; set; }
        //public User User { get; set; }
    }
}
