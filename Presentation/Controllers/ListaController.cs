using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/lista")]
    public class ListaController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ListaController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _serviceManager.ListaService().GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ListaRequest listaRequest)
        {
            var result = await _serviceManager.ListaService().CreateAsync(listaRequest);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, ListaRequest listaRequest)
        {
            await _serviceManager.ListaService().UpdateAsync(id, listaRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _serviceManager.ListaService().DeleteAsync(id);
            return NoContent();
        }
    }
}
