using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Service;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System.Data;

namespace DevFreela.Application.Commands.InsertUser
{
    public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand, ResultViewModel<int>>
    {
        private readonly IUserRepository _repository;
        private readonly IAuthService _authService;
        public InsertUserCommandHandler(IUserRepository repository, IAuthService authService)
        {
            _repository = repository;
            _authService = authService;
        }
        public async Task<ResultViewModel<int>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _authService.ComputeSha256Hash(request.Password);

            var user = new User(request.FullName, request.Email, request.BrithDate, passwordHash, request.Role);

            await _repository.Add(user);

            return ResultViewModel<int>.Success(user.Id);
        }
    }
}
