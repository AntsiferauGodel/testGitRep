using System.Web;
using Lab08.MVC.Business.Models;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Web.Converters
{
    public static class FileConverter
    {
        public static File ConvertPostedFileBaseToFile(HttpPostedFileBase fileBase)
        {
            byte[] content;
            using (var reader = new System.IO.BinaryReader(fileBase.InputStream))
            {
                content = reader.ReadBytes(fileBase.ContentLength);
            }
            return new File
            {
                FileName = System.IO.Path.GetFileName(fileBase.FileName),
                FileType = FileType.ItemPicture,
                ContentType = fileBase.ContentType,
                Content = content
            };
        }
        public static File ConvertPostedFileBaseToFileWithId(HttpPostedFileBase fileBase, int advertisementId)
        {
            byte[] content;
            using (var reader = new System.IO.BinaryReader(fileBase.InputStream))
            {
                content = reader.ReadBytes(fileBase.ContentLength);
            }
            return new File
            {
                FileName = System.IO.Path.GetFileName(fileBase.FileName),
                FileType = FileType.ItemPicture,
                ContentType = fileBase.ContentType,
                Content = content,
                AdvertisementId = advertisementId
            };
        }

        public static PictureView ConvertFileToPictureView(File file)
        {
            return new PictureView
            {
                FileId = file.FileId,
                Content = file.Content
            };
        }
    }
}
