namespace MyPortfolioApis.Models
{
    public class Home
    {
        public int Id { get; set; }
        public ICollection<MyImages> Images { get; set; }
        public string subTitle { get; set; }
        public string Summary { get; set; }
        public string FacebookLink { get; set; }
        public string LinkedinLink { get; set; }
        public string GithubLink { get; set; }
        public string WhatsLink { get; set; }
        public string InstagramLink { get; set; }
        public string DownloadResume { get; set; }
        public int DeliveryOrder { get; set; }
    }
}
