using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
    public class SkillItemViewModel
    {
        public SkillItemViewModel(int id, string description)
        {
            Id = id;
            Description = description;
        }
        public int Id { get; set; }
        public string Description { get; set; }

        public static SkillItemViewModel FromEntity(Skill skill) => new(skill.Id, skill.Description);
    }
}
