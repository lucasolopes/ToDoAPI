using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Responses
{
    public class CardResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int Position { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public DateTime? DateInit { get; set; }
        public DateTime? DateCompleted { get; set; }
        public string? Status { get; set; }
        
    }
}
