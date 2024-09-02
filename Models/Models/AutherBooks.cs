using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class AutherBook
    {

        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }
        public int AuthorId { get; set; }
        public int BookId { get; set; }


    }

    public class AutherBookConfigration : IEntityTypeConfiguration<AutherBook>
    {
        public void Configure(EntityTypeBuilder<AutherBook> builder)
        {
            builder.HasKey(x => new {x.AuthorId, x.BookId});

            builder.HasOne(a => a.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(a => a.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(a => a.Book)
                .WithMany(a => a.Authers)
                .HasForeignKey(a => a.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
