using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.InsertUser
{
    public class InsertUserCommand : IRequest<ResultViewModel<int>>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime BrithDate { get; set; }

        
    }
}
