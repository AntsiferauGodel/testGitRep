namespace Lab08.MVC.Domain
{
    public class File
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public FileType FileType { get; set; }
        public int AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }
    }
}
