using Lab08.MVC.Business.Models;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Web.Converters
{
    public static class AdvertisementViewToEntityConverter
    {
        public static Advertisement Convert(AdvertisementView advertisement)
        {
            return new Advertisement
            {
                AdvertisementId = advertisement.Id,
                Header = advertisement.Header,
                Description = advertisement.Description,
                Price = advertisement.Price,
                CreatorUserId = advertisement.OwnerId
            };
        }
    }
}
