namespace MyPortfolioApis.Models
{
    public class MyCertificates
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CertificatesLink { get; set; }
        public int AboutId { get; set; }
        public About About { get; set; }
        public int DeliveryOrder { get; set; }

    }
}
