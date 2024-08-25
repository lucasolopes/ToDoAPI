using Shared.Requests;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnKanBan.Domain.Entities
{
    public class Lista
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public int Position { get; set; }

        public string WhiteBoardId { get; set; }
        public WhiteBoard WhiteBoard { get; set; }

        public ICollection<Card>? Cards { get; set; }

        public ListaResponse Convert() => new ListaResponse
        {
            Id = this.Id,
            Name = this.Name,
            Position = this.Position,
            WhiteBoardId = this.WhiteBoardId
        };

        public Lista()
        {
        }

        public Lista(ListaRequest listaRequest) {

            this.Name = listaRequest.Name;
            this.Position = listaRequest.Position;
            this.WhiteBoardId = listaRequest.WhiteBoardId;
        }

        public Lista(ListaPutNameRequest listaPutNameRequest)=> this.Name = listaPutNameRequest.Name;

        public Lista(ListaPutPositionRequest listaPutPositionRequest) => this.Position = listaPutPositionRequest.Position;
    }
}
