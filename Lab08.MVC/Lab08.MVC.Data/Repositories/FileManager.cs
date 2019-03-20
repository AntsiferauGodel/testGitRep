using Lab08.MVC.Data.DbContext;
using Lab08.MVC.Data.Interfaces;

namespace Lab08.MVC.Data.Repositories
{
    public class FileManager : IFileManager
    {
        public FleaMarketContext Context { get; set; }

        public FileManager(FleaMarketContext context)
        {
            Context = context;
        }

        public void RemoveFileById(int fileId)
        {
            var target = Context.Files.Find(fileId);
            Context.Files.Remove(target);
            Context.SaveChanges();
        }
    }
}
