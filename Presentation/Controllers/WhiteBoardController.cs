using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/whiteboard")]
    public class WhiteBoardController : ControllerBase
    {
        private readonly IServiceManager _seviceManager;

        public WhiteBoardController(IServiceManager serviceManager) => _seviceManager = serviceManager;
    }
}
