using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Models
{
  public class Author
  {
        public int ID { get; set; }
        public string Name {get; set;}
        public string WebSite { get; set;}
        public string PhoneNumber {get; set;} 
        public virtual ICollection<AutherBook> Books { get; set;}
  }
    public class AuthorConfigration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            
        }
    }
}