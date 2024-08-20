using Shared.Requests;
using Shared.Responses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnKanBan.Domain.Entities
{
    public class WhiteBoard
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;

        public ICollection<Lista>? Listas { get; set; }

        public WhiteBoard() { }

        public WhiteBoard(string Name, string Description = default, DateTime CreatedAt = default, DateTime LastUpdatedAt = default)
        {
            this.Name = Name;
            this.Description = Description;
            this.CreatedAt = CreatedAt == default ? DateTime.Now : CreatedAt;
            this.LastUpdatedAt = LastUpdatedAt == default ? DateTime.Now : LastUpdatedAt;
        }

        public WhiteBoard(WhiteBoardRequest whiteBoardRequest)
        {
            this.Name = whiteBoardRequest.Name;
            this.Description = whiteBoardRequest.Description;
            this.CreatedAt = DateTime.Now;
            this.LastUpdatedAt = DateTime.Now;
        }

        public WhiteBoardResponse Convert()
        {
            return new WhiteBoardResponse(this.Id,this.Name, this.Description, this.CreatedAt, this.LastUpdatedAt)
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                CreatedAt = this.CreatedAt,
                LastUpdatedAt = this.LastUpdatedAt,
            };
        }
    }
}
