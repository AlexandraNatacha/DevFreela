using DevFreela.Application.Commands.InsertUser;
using DevFreela.Application.Commands.InsertUserSkill;
using DevFreela.Application.Commands.LoginUser;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetUserById;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;
        private readonly IMediator _mediator;

        public UsersController(DevFreelaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        //GET api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        //POST api/users
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(InsertUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetById), new { id = result.Data}, command);
        }


        [HttpPost("{id}/skills")]
        public async Task<IActionResult> PostSkills(InsertUserSkillCommand command)
        {
            var result = await _mediator.Send(command);
            if(!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var loginViewModel = await _mediator.Send(command);
            if (loginViewModel is null)
            {
                var error = ResultViewModel<LoginUserViewModel>.Erro("Usuário ou senha inválidos");
                return BadRequest(error);

            }
            var result = ResultViewModel<LoginUserViewModel>.Success(loginViewModel);

            return Ok(result);
        }

        //[HttpPut("{id}/profile-picture")] 
        //public IActionResult PostProfilePicture(int id, IFormFile file)
        //{
        //    var description = $"File: {file.FileName}, Size: {file.Length}";

        //   //processar a imagem

        //    return Ok(description);
        //}
    }
}
