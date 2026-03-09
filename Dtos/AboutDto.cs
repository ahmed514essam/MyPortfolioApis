using System.ComponentModel.DataAnnotations.Schema;
using MyPortfolioApis.Models;

namespace MyPortfolioApis.Dtos
{
    public class AboutDto
    {
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string Phone { get; set; }
        public string From { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string FacebookLink { get; set; }
        public string LinkedinLink { get; set; }
        public string GithubLink { get; set; }
        public string WhatsLink { get; set; }
        public string InstagramLink { get; set; }
        public string WhoAmI { get; set; }
        [NotMapped]
        public List<IFormFile> ImageFiles { get; set; } = new();

        // الصور القديمة (في الـ Edit)
        [NotMapped]
        public List<MyImages> ExistingImages { get; set; } = new();
        [NotMapped]
        public List<MyCertificates> MyCertificates { get; set; } = new List<MyCertificates>();
    }
}
