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
    [Route("api/card")]
    public class CardController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CardController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _serviceManager.CardService().GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CardRequest cardRequest)
        {
            var result = await _serviceManager.CardService().CreateAsync(cardRequest);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, CardRequest cardRequest)
        {
            await _serviceManager.CardService().UpdateAsync(id, cardRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _serviceManager.CardService().DeleteAsync(id);
            return NoContent();
        }
    }
}
