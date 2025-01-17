﻿using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;

        public UsersController(DevFreelaDbContext context)
        {
            _context = context;
        }

        //GET api/users/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _context.Users
                .Include(u => u.Skills)
                .ThenInclude(u => u.Skill)
                .SingleOrDefault(u => u.Id == id);

            if(user is null)
                return NotFound();

            var model = UserViewModel.FromEntity(user);

            return Ok(model);
        }


        //POST api/users
        [HttpPost]
        public IActionResult Post(CreateUserInputModel model)
        {
            var user = model.ToEntity();

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost("{id}/skills")]
        public IActionResult PostSkills(int id, UserSkillInputModel model)
        {
            var userSkills = model.SkillIds.Select(s => new UserSkill(id, s)).ToList();

            _context.AddRange(userSkills);
            _context.SaveChanges();

            return NoContent();
        }

        //[HttpPut("{id}/profile-picture")] 
        //public IActionResult PostProfilePicture(int id,     IFormFile file)
        //{
        //    var description = $"File: {file.FileName}, Size: {file.Length}";

        //    //processar a imagem

        //    return Ok(description);
        //}
    }
}
