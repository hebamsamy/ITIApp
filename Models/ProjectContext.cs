using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProjectContext :DbContext
    {
        public ProjectContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            //for database
            optionsBuilder.UseSqlServer("Data source = DESKTOP-0KJMNFC; Initial catalog = Project; Integrated security= true; trustservercertificate = true; MultipleActiveResultSets=true ");
            //enable lazy loading
            optionsBuilder.UseLazyLoadingProxies();
            ////other options
            //optionsBuilder.LogTo(log=>Debug.WriteLine(log));

            base.OnConfiguring(optionsBuilder);
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
