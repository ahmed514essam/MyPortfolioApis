using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortfolioApis.Dtos
{
    public class MySkillsDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        [NotMapped]
        public List<IFormFile> ImageFiles { get; set; } = new();
         [NotMapped]
         public List<int> ExistingImageIds { get; set; } = new();
    }
}
