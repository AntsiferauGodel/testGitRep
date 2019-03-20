using System.Collections.Generic;

namespace Lab08.MVC.Domain
{
    public class Advertisement
    {
        public int AdvertisementId { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CreatorUserId { get; set; }
        public ApplicationUser CreatorUser { get; set; }
        public ICollection<File> Files { get; set; }
    }
}
