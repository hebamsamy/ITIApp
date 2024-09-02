using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
  public class Book
  {
        public int ID { get; set; }
        public string Isbn {get; set;}
        public string Notes {get; set;}
        public int PageCount {get; set;}
        public decimal Price {get; set;}
        public DateTime PublicationDate {get; set;}
        public string Summary {get; set;}
        public string Title {get; set;}
        public int SubjectId { get; set;}
        public virtual Subject Subject {get; set;}
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<AutherBook> Authers { get; set; }
        public virtual ICollection<BookAttachment> Attachments { get; set; }
    }

    public class BookConfigration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            //builder.ToTable("book","HR");
            builder.ToTable("book");

            builder.HasKey(x => x.ID);

            builder.Property(b => b.ID).ValueGeneratedOnAdd();

            builder.HasOne(b => b.Subject)
                .WithMany(s => s.Books)
                .HasForeignKey(b => b.SubjectId);

            builder.HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId);

            //builder.Property(b => b.Isbn).HasDefaultValueSql("");
            builder.Property(b => b.Isbn).HasColumnType("nvarchar(15)");

        }
    }
}