using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Core.Service;
using MediatR;

namespace DevFreela.Application.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public LoginUserCommandHandler(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }
        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            // passo 1: utilizar o mesmo algorítimo para criar o hash dessa senha
            var passwordHash = _authService.ComputeSha256Hash(request.Password);
            // passo 2: buscar no banco de dados um User com esse email e esse hash de senha
           var user = await _userRepository.GetUserByEmailAndPasswordAsync(request.Email, passwordHash);
            // se não existir: erro no login
            if (user == null)
                return null;
            // se existir: gerar o token usando os dados do usuário

            var token = _authService.GenerateJwtToken(user.Email, user.Role);

            var loginUserViewModel = new LoginUserViewModel(user.Email, token);

            return loginUserViewModel;
        }
    }
}
