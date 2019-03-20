using System;
using System.Collections.Generic;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Data.Interfaces
{
    public interface IAdvertisementManager : IDisposable
    {
        int GetAdvertisementsRecordsNumber(int recordsOnPage);
        int GetAdvertisementsRecordsNumberForCurrentUser(int recordsOnPage, string userId);
        int GetAdvertisementsRecordsNumberByPartialHeader(string searchword, int recordsOnPage);
        void Create(Advertisement advertisement);
        IList<Advertisement> GetAdvertisements();
        IList<Advertisement> GetAdvertisements(int pageNumber, int recordsOnPage);
        IList<Advertisement> GetAdvertisementsForCurrentUser(string userId, int pageNumber, int recordsOnPage);
        IList<Advertisement> GetAdvertisementsByPartialHeader(string searchWord, int pageNumber, int recordsOnPage);
        void EditAdvertisementById(Advertisement advertisement);
        Advertisement GetAdvertisementById(int id);
        void RemoveAdvertisementById(int advertisementId);
    }
}
