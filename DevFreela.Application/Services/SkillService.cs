using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DevFreela.Application.Services
{
    public class SkillService : ISkillService
    {
        private readonly DevFreelaDbContext _context;
        public SkillService(DevFreelaDbContext context)
        {
            _context = context;
        }
        public ResultViewModel<List<SkillItemViewModel>> GetAll()
        {
            var skills = _context.Skills.Where(s => !s.IsDeleted).ToList();
            var model = skills.Select(SkillItemViewModel.FromEntity).ToList();

            return ResultViewModel<List<SkillItemViewModel>>.Success(model);
        }

        public ResultViewModel<int> Insert(CreateSkillInputModel model)
        {
            var skill = model.ToEntity();

            _context.Skills.Add(skill);
            _context.SaveChanges();

            return ResultViewModel<int>.Success(model.Id);
        }
    }
}
