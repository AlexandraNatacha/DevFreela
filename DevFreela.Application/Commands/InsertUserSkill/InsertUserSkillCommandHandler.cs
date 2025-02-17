using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.InsertUserSkill
{
    
    public class InsertUserSkillCommandHandler : IRequestHandler<InsertUserSkillCommand, ResultViewModel>
    {
        private readonly IUserRepository _repository;
        
        public InsertUserSkillCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel> Handle(InsertUserSkillCommand request, CancellationToken cancellationToken)
        {
            var existingSkills = await _repository.IdSkillsExists(request.Skills, request.IdUser);

            var newUserSkills = request.Skills
                .Where(x => !existingSkills.Contains(x))
                .Select(x => new UserSkill(request.IdUser, x)).ToList();

            await _repository.AddSkills(newUserSkills);

            return ResultViewModel.Success();

        }
    }
}
