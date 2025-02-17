using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IUserRepository
    {
        Task<int> Add(User user);
        Task AddSkills(List<UserSkill> skills);
        Task<User?> GetById(int id);
        Task<List<int>> IdSkillsExists(int[] skills, int idUser);
    }
}
