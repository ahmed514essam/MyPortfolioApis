using System.ComponentModel.DataAnnotations.Schema;
using MyPortfolioApis.Models;

namespace MyPortfolioApis.Dtos
{
    public class MyProjectsDto
    {
        public string Name { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string DemoLink { get; set; }
        public string RepoLink { get; set; }
        [NotMapped]
        public List<IFormFile> ImageFiles { get; set; } = new();    
            [NotMapped]
        public List<MyImages> ExistingImages { get; set; } = new();

    }
}
