namespace MyPortfolioApis.Models
{
    public class MyImages
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string PublicId { get; set; }

        public string EntityType { get; set; }  

        public int EntityId { get; set; }
        public int DeliveryOrder { get; set; }

    }
}
