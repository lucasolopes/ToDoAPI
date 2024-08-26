using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Requests
{
    public class CardPutStatusRequest
    {
        [Required(ErrorMessage = "{0} is required")]
        [Range(0, 2, ErrorMessage = "{0} must be 0 or 2")]
        public int Status { get; set; }
    }
}
