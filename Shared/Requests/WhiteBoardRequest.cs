using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Requests
{
    public class WhiteBoardRequest
    {
        [Required(ErrorMessage = "O Campos {0} é obrigatorio!")]
        [MaxLength(60, ErrorMessage = "O campo {0} deve ter no maximo {1} caracteres!")]
        public string Name { get; set; }
        [MaxLength(500, ErrorMessage = "O campo {0} deve ter no maximo {1} caracteres!")]
        public string Description { get; set; }

        public WhiteBoardRequest()
        {
        }
        
        public WhiteBoardRequest(string name, string description)
        
        {
            Name = name;
            Description = description;
        }

    }
}
