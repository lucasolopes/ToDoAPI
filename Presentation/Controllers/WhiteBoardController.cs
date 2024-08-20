using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Requests;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/whiteboard")]
    public class WhiteBoardController : ControllerBase
    {
        private readonly IServiceManager _seviceManager;
        private readonly IValidator<WhiteBoardRequest> _validator;

        public WhiteBoardController(IServiceManager serviceManager, IValidator<WhiteBoardRequest> validator) {
            _seviceManager = serviceManager;
            _validator = validator;
        }
        

        [HttpPost]
        public async Task<IActionResult> CreateWhiteBoard(WhiteBoardRequest whiteBoardRequest)
        {
            var whiteBoard = await _seviceManager.WhiteBoardService().CreateAsync(whiteBoardRequest);
            return CreatedAtAction(nameof(GetWhiteBoard), new { id = whiteBoard.Id }, whiteBoard);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWhiteBoard(string id)
        {
            var whiteBoard = await _seviceManager.WhiteBoardService().GetByIdAsync(id);
            return Ok(whiteBoard);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWhiteBoard(string id, WhiteBoardRequest whiteBoardRequest)
        {
            await _seviceManager.WhiteBoardService().UpdateAsync(id, whiteBoardRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWhiteBoard(string id)
        {
            await _seviceManager.WhiteBoardService().DeleteAsync(id);
            return NoContent();
        }
    }
}
