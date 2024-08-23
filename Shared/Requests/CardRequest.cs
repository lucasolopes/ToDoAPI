using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Requests
{
    public class CardRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public int Position { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string ListaId { get; set; }
    }
}
