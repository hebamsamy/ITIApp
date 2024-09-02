using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class BookAttachment
    {
        public int ID { get; set; }
        public string Path { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
    public class BookAttachmentConfigration : IEntityTypeConfiguration<BookAttachment>
    {
        public void Configure(EntityTypeBuilder<BookAttachment> builder)
        {
            builder.ToTable(nameof(BookAttachment));
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Book)
                .WithMany(x=>x.Attachments)
                .HasForeignKey(x=>x.BookId)
                .OnDelete(DeleteBehavior.Cascade);  
        }
    }
}

