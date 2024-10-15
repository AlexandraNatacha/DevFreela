using DevFreela.API.Entities;

namespace DevFreela.API.Models
{
    public class CreateUserInputModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BrithDate { get; set; }

        public User ToEntity() => new(FullName, Email, BrithDate);
    }
}
