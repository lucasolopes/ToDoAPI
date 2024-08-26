using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Requests
{
    public class CardPutDateCompleteRequest
    {
        [Required(ErrorMessage = "{0} is required")]
        public DateTime Date { get; set; }
    }
}
