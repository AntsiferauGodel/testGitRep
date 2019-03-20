using System.Collections.Generic;
using Lab08.MVC.Business.Interfaces;
using Lab08.MVC.Data.Interfaces;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Business.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public AdvertisementService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public void Create(Advertisement advertisement)
        {
            UnitOfWork.AdvertisementManager.Create(advertisement);
        }

        public Advertisement GetAdvertisementById(int id)
        {
            return UnitOfWork.AdvertisementManager.GetAdvertisementById(id);
        }

        public int GetAdvertisementsTotalPagesNumberForCurrentUser(int recordsOnPage, string userId)
        {
            var recordsNumber = UnitOfWork.AdvertisementManager.GetAdvertisementsRecordsNumberForCurrentUser(recordsOnPage, userId);
            var result = CalculatePagesNumber(recordsNumber, recordsOnPage);
            return result;
        }

        public void EditAdvertisementById(Advertisement advertisement)
        {
            UnitOfWork.AdvertisementManager.EditAdvertisementById(advertisement);
        }

        public IList<Advertisement> GetAdvertisementsForCurrentUser(string userId, int pageNumber, int recordsOnPage)
        {
            return UnitOfWork.AdvertisementManager.GetAdvertisementsForCurrentUser(userId, pageNumber, recordsOnPage);
        }

        public void RemoveAdvertisementById(int advertisementId)
        {
            UnitOfWork.AdvertisementManager.RemoveAdvertisementById(advertisementId);
        }

        public IList<Advertisement> GetAdvertisementsByPartialHeader(string searchWord, int pageNumber, int recordsOnPage)
        {
            return UnitOfWork.AdvertisementManager.GetAdvertisementsByPartialHeader(searchWord, pageNumber, recordsOnPage);
        }

        public int GetAdvertisementsTotalPagesNumberByPartialHeader(string searchword, int recordsOnPage)
        {
            var recordsNumber = UnitOfWork.AdvertisementManager.GetAdvertisementsRecordsNumberByPartialHeader(searchword, recordsOnPage);
            var result = CalculatePagesNumber(recordsNumber, recordsOnPage);
            return result;
        }

        public int GetAdvertisementsTotalPagesNumber(int recordsOnPage)
        {
            var recordsNumber = UnitOfWork.AdvertisementManager.GetAdvertisementsRecordsNumber(recordsOnPage);
            var result = CalculatePagesNumber(recordsNumber, recordsOnPage);
            return result;
        }

        public IList<Advertisement> GetAdvertisements(int pageNumber, int recordsOnPage)
        {
            return UnitOfWork.AdvertisementManager.GetAdvertisements(pageNumber, recordsOnPage);
        }

        private static int CalculatePagesNumber(int recordsNumber, int recordsOnPage)
        {
            var addition = 0;
            if (recordsNumber % recordsOnPage > 0)
            {
                addition = 1;
            }
            return (recordsNumber / recordsOnPage) + addition;
        }
    }
}
