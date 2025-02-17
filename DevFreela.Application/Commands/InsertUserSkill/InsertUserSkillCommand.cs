using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.InsertUserSkill
{
    public class InsertUserSkillCommand : IRequest<ResultViewModel>
    {
        public int IdUser { get; set; }
        public int[] Skills { get; set; }
    }
}
