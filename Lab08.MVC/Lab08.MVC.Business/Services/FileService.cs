using Lab08.MVC.Business.Interfaces;
using Lab08.MVC.Data.Interfaces;

namespace Lab08.MVC.Business.Services
{
    public class FileService : IFileService
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public FileService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public void RemoveFileById(int fileId)
        {
            UnitOfWork.FileManager.RemoveFileById(fileId);
        }
    }
}
