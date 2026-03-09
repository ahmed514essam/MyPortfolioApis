namespace MyPortfolioApis.Models
{
    public class MyProjects
    {
        public int Id { get; set; }
        public ICollection<MyImages> Images { get; set; }
        public string Name { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string DemoLink { get; set; }
        public string RepoLink { get; set; }
        public int DeliveryOrder { get; set; }


    }
}
