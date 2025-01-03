using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.GetAllSkill
{
    public class GetAllSkillQuery : IRequest<ResultViewModel<List<SkillItemViewModel>>>
    {
    }
}
