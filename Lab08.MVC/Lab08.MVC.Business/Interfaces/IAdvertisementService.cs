using System.Collections.Generic;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Business.Interfaces
{
    public interface IAdvertisementService
    {
        int GetAdvertisementsTotalPagesNumber(int recordsOnPage);
        int GetAdvertisementsTotalPagesNumberForCurrentUser(int recordsOnPage, string userId);
        int GetAdvertisementsTotalPagesNumberByPartialHeader(string searchword, int recordsOnPage);
        Advertisement GetAdvertisementById(int id);
        IList<Advertisement> GetAdvertisements(int pageNumber, int recordsOnPage);
        IList<Advertisement> GetAdvertisementsForCurrentUser(string userId, int pageNumber, int recordsOnPage);
        IList<Advertisement> GetAdvertisementsByPartialHeader(string searchWord, int pageNumber, int recordsOnPage);
        void RemoveAdvertisementById(int advertisementId);
        void EditAdvertisementById(Advertisement advertisement);
        void Create(Advertisement advertisement);
    }
}
