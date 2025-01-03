using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllSkill
{
    public class GetAllSkillHandler : IRequestHandler<GetAllSkillQuery, ResultViewModel<List<SkillItemViewModel>>>
    {
        private readonly DevFreelaDbContext _context;
        public GetAllSkillHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel<List<SkillItemViewModel>>> Handle(GetAllSkillQuery request, CancellationToken cancellationToken)
        {
            var skills = await _context.Skills.Where(x => !x.IsDeleted).ToListAsync();

            var model = skills.Select(SkillItemViewModel.FromEntity).ToList();

            return ResultViewModel<List<SkillItemViewModel>>.Success(model);
        }
    }
}
