using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Requests
{
    public class CardPutDateInitRequest
    {
        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
    }
}
