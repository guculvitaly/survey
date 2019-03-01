using Microsoft.EntityFrameworkCore;
using WebProject.Models;

namespace WebProject.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() { }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { Database.EnsureCreated(); }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Question { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
                optionsBuilder.UseSqlServer(@"Server =.\SQLExpress; Database = SurveyDb; Trusted_Connection = True;");
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Question>().ToTable("Question");
            modelBuilder.Entity<Survey>().ToTable("Survey"); 
           
        }

    }
}
