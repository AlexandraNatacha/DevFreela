using Azure.Core;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _context;
        public UserRepository(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task AddSkills(List<UserSkill> skills)
        {
            await _context.UserSkills.AddRangeAsync(skills);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetById(int id)
        {
            var user = await _context.Users
                .Include(u => u.Skills)
                .ThenInclude(us => us.Skill)
                .SingleOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<User?> GetUserByEmailAndPasswordAsync(string email, string passwordHash)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Email.Equals(email) && u.Password.Equals(passwordHash));
            return user;
        }

        public async Task<List<int>> IdSkillsExists(int[] skills, int idUser)
        {
            var existingSkills = await _context.UserSkills
            .Where(us => us.IdUser == idUser && us.IsDeleted == false && skills.Contains(us.IdSkill))
            .Select(us => us.IdSkill)
            .ToListAsync();

            return existingSkills;
        }
    }
}
