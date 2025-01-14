using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _context;
        public UserService(DevFreelaDbContext context)
        {
            _context = context;
        }

        public ResultViewModel<UserViewModel> GetById(int id)
        {
            var user = _context.Users
                 .Include(u => u.Skills)
                 .ThenInclude(u => u.Skill)
                 .SingleOrDefault(u => u.Id == id);

            if (user is null)
                return ResultViewModel<UserViewModel>.Erro("Usuário não existe!");

            var model = UserViewModel.FromEntity(user);

            return ResultViewModel<UserViewModel>.Success(model);

        }
        public ResultViewModel<int> Insert(CreateUserInputModel model)
        {
            var user = model.ToEntity();

            _context.Users.Add(user);
            _context.SaveChanges();

            return ResultViewModel<int>.Success(user.Id);
        }

        public ResultViewModel<int> InsertSkill(int id, UserSkillInputModel model)
        {
            var userSkills = model.SkillIds.Select(s => new UserSkill(id, s)).ToList();

            _context.AddRange(userSkills);
            _context.SaveChanges();

            return ResultViewModel<int>.Success(id); //n sei
        }
    }
}
