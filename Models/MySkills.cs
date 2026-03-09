namespace MyPortfolioApis.Models
{
    public class MySkills
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public ICollection<MyImages> Images { get; set; }
        public int DeliveryOrder { get; set; }


    }
}
