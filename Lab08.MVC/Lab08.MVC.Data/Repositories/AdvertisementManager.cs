using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using Lab08.MVC.Data.DbContext;
using Lab08.MVC.Data.Interfaces;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Data.Repositories
{
    public class AdvertisementManager : IAdvertisementManager
    {
        public FleaMarketContext Context { get; set; }

        public AdvertisementManager(FleaMarketContext context)
        {
            Context = context;
        }

        public int GetAdvertisementsRecordsNumber(int recordsOnPage)
        {
            return Context.Advertisements.Count();
        }

        public int GetAdvertisementsRecordsNumberForCurrentUser(int recordsOnPage, string userId)
        {
            return Context.Advertisements.Count(p => p.CreatorUserId == userId);
        }

        public IList<Advertisement> GetAdvertisements(int pageNumber, int recordsOnPage)
        {
            return Context.Advertisements.OrderBy(p => p.AdvertisementId)
                .Skip(pageNumber * recordsOnPage).Take(recordsOnPage).ToList();
        }

        public IList<Advertisement> GetAdvertisements()
        {
            return Context.Advertisements.ToList();
        }

        public Advertisement GetAdvertisementById(int id)
        {
            return Context.Advertisements.Include(path => path.Files).FirstOrDefault(p => p.AdvertisementId == id);
        }

        public void Create(Advertisement advertisement)
        {
            Context.Advertisements.Add(advertisement);
            Context.SaveChanges();
        }
        public void EditAdvertisementById(Advertisement advertisement)
        {
            foreach (var item in advertisement.Files)
            {
                Context.Files.Add(item);
            }
            var original = Context.Advertisements.Find(advertisement.AdvertisementId);
            Context.Entry(original).CurrentValues.SetValues(advertisement);
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public IList<Advertisement> GetAdvertisementsForCurrentUser(string userId, int pageNumber, int recordsOnPage)
        {
            return Context.Advertisements.Where(x => x.CreatorUserId == userId)
                .OrderBy(x => x.AdvertisementId).Skip(pageNumber * recordsOnPage).Take(recordsOnPage).ToList();
        }

        public void RemoveAdvertisementById(int advertisementId)
        {
            var target = Context.Advertisements.Find(advertisementId);
            Context.Advertisements.Remove(target);
            Context.SaveChanges();
        }

        public IList<Advertisement> GetAdvertisementsByPartialHeader(string searchWord, int pageNumber, int recordsOnPage)
        {
            return Context.Advertisements.Where(x => x.Header.Contains(searchWord))
                .OrderBy(x => x.AdvertisementId).Skip(pageNumber * recordsOnPage).Take(recordsOnPage).ToList();
        }

        public int GetAdvertisementsRecordsNumberByPartialHeader(string searchword, int recordsOnPage)
        {
            return Context.Advertisements.Count(s => s.Header.Contains(searchword));
        }
    }
}
