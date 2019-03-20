using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab08.MVC.Business.Models
{
    public class AdvertisementView
    {
        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public string Header { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Range(typeof(decimal), "0", "99999999", ErrorMessage = "{0} must be a number between {1} and {2}")]
        public decimal Price { get; set; }
        public string OwnerId { get; set; }
        public ICollection<PictureView> Pictures { get; set; }
    }
}
