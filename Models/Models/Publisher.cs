using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{

  public class Publisher
  {
        public int ID { get; set; }
        public string WebSite {get; set;}
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Book> Books { get; set;}
  }
    public class PublisherConfigration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasOne(p => p.User)
                .WithOne(u => u.Publisher)
                .HasForeignKey<Publisher>(p => p.UserId);
        }
    }


}
