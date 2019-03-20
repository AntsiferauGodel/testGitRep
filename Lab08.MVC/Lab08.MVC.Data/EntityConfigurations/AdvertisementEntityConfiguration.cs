using System.Data.Entity.ModelConfiguration;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Data.EntityConfigurations
{
    public class AdvertisementEntityConfiguration : EntityTypeConfiguration<Advertisement>
    {
        private const int MaxHeaderSize = 80;
        private const int MaxDescriptionSize = 500;
        public AdvertisementEntityConfiguration()
        {
            this.HasKey(k => k.AdvertisementId);
            this.HasRequired(r => r.CreatorUser).WithMany(m => m.Advertisements).HasForeignKey(r => r.CreatorUserId);
            this.Property(p => p.Header).HasMaxLength(MaxHeaderSize).IsRequired();
            this.Property(p => p.Description).HasMaxLength(MaxDescriptionSize).IsRequired();
            this.Property(p => p.Price).IsRequired();
        }
    }
}
