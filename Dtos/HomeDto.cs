using System.ComponentModel.DataAnnotations.Schema;
using MyPortfolioApis.Models;

namespace MyPortfolioApis.Dtos
{
    public class HomeDto
    {
        public string subTitle { get; set; }
        public string Summary { get; set; }
        public string FacebookLink { get; set; }
        public string LinkedinLink { get; set; }
        public string GithubLink { get; set; }
        public string WhatsLink { get; set; }
        public string InstagramLink { get; set; }

        public string DownloadResume { get; set; }
        [NotMapped]
        public List<IFormFile> ImageFiles { get; set; } = new();
        [NotMapped]
        public List<MyImages> ExistingImages { get; set; } = new();

    }
}
