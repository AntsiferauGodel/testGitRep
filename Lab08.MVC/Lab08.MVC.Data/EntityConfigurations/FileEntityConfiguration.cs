using System.Data.Entity.ModelConfiguration;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Data.EntityConfigurations
{
    internal class FileEntityConfiguration : EntityTypeConfiguration<File>
    {
        private const int FileNameMaxSize = 255;
        private const int FileContentTypeMaxSize = 100;

        public FileEntityConfiguration()
        {
            this.Property(p => p.FileName).HasMaxLength(FileNameMaxSize);
            this.Property(p => p.ContentType).HasMaxLength(FileContentTypeMaxSize);
        }
    }
}
