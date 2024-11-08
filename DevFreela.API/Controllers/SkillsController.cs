using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/skills")]
    public class SkillsController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;

        public SkillsController(DevFreelaDbContext context)
        {
                _context = context;
        }


        //GET api/skills
        [HttpGet]
        public IActionResult GetAll()
        {
            var skills = _context.Skills.Where(s => !s.IsDeleted).ToList();

            return Ok(skills);
        }

        //POST api/skills
        [HttpPost]
        public IActionResult Post(CreateSkillInputModel model)
        {
            var skill = model.ToEntity();

            _context.Skills.Add(skill);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
