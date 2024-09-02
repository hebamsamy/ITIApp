using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Models
{
  public class Subject
  {
        public int ID { get; set; }
        public string Description {get; set;}
        public string Name {get; set;}
        public virtual ICollection<Book> Books {get; set;}

  }

    public class SubjectConfigration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            ///
        }
    }
}