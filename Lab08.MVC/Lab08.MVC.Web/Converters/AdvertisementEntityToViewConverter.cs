using Lab08.MVC.Business.Models;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Web.Converters
{
    public static class AdvertisementEntityToViewConverter
    {
        public static AdvertisementView ConvertToAdvertisementView(Advertisement advertisement)
        {
            return new AdvertisementView
            {
                Id = advertisement.AdvertisementId,
                Header = advertisement.Header,
                Description = advertisement.Description,
                Price = advertisement.Price,
                OwnerId = advertisement.CreatorUserId
            };
        }

        public static AdvertisementHeaderView ConvertToAdvertisementHeader(Advertisement advertisement)
        {
            return new AdvertisementHeaderView
            {
                Id = advertisement.AdvertisementId,
                Header = advertisement.Header,
                Price = advertisement.Price
            };
        }
    }
}
