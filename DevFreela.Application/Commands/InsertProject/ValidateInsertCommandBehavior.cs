using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject
{
    public class ValidateInsertCommandBehavior :  IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _context;
        public ValidateInsertCommandBehavior(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)
        {
            var clientExists = _context.Users.Any(x => x.Id == request.IdClient);
            var freelancerExists = _context.Users.Any(x => x.Id == request.IdFreelancer);
            
            if(!clientExists || !freelancerExists )
            {
                return ResultViewModel<int>.Erro("Id do cliente ou Freelancer inválidos.");
            }
            return await next();
        }
    }
}
