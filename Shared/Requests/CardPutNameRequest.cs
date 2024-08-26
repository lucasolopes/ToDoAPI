using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Requests
{
    public class CardPutNameRequest
    {
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(60, ErrorMessage = "{0} must be between {2} and {1} characters", MinimumLength = 3)]
        public string Title { get; set; }
    }
}
