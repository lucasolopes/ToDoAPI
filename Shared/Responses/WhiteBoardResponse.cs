using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Responses
{
    public class WhiteBoardResponse
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        public WhiteBoardResponse()
        {
        }

        public WhiteBoardResponse(string id, string name, string? description, DateTime createdAt, DateTime lastUpdatedAt)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.CreatedAt = createdAt;
            this.LastUpdatedAt = lastUpdatedAt;
        }


    }
}
