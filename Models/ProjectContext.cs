using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProjectContext : IdentityDbContext<User>
    {
        public ProjectContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new BookConfigration());
            modelBuilder.ApplyConfiguration(new PublisherConfigration());
            modelBuilder.ApplyConfiguration(new AuthorConfigration());
            modelBuilder.ApplyConfiguration(new AutherBookConfigration());
            modelBuilder.ApplyConfiguration(new SubjectConfigration());

            base.OnModelCreating(modelBuilder);

        }


        //Tables
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author>  Authors { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<AutherBook> AutherBook { get; set; }


    }
}
