using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Models
{
  public class Author
  {
        public int ID { get; set; }
        public string WebSite { get; set;}
        public string UserId { get; set;}
        public virtual User User { get; set;}
        public virtual ICollection<AutherBook> Books { get; set;}
  }
    public class AuthorConfigration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(x => x.ID);

            builder.HasOne(a => a.User)
                .WithOne(u => u.Author)
                .HasForeignKey<Author>(a => a.UserId);
        }
    }
}