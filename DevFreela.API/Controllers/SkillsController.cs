using DevFreela.Application.Commands.InsertSkill;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetAllSkill;
using DevFreela.Application.Services;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/skills")]
    public class SkillsController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;
        //private readonly ISkillService _service; 
        private readonly IMediator _mediator;

        public SkillsController(DevFreelaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }


        //GET api/skills
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllSkillQuery();
            var result = await _mediator.Send(query);

            //var result = _service.GetAll();

            return Ok(result);
        }

        //POST api/skills
        [HttpPost]
        public async Task<IActionResult> Post(InsertSkillCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetAll), new { id = result.Data }, command);
        }

    }
}
