using Microsoft.EntityFrameworkCore;

namespace SensoStatWeb.Models.Entities
{
    public class SensoStatDbContext: DbContext
    {
        public DbSet<Administrator>? Administrators { get; set; }
        public DbSet<Answer>? Answers { get; set; }
        public DbSet<Instruction>? Instructions { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Question>? Questions { get; set; }
        public DbSet<Survey>? Surveys { get; set; }
        public DbSet<SurveyInstruction>? SurveyInstructions { get; set; }
        public DbSet<SurveyState>? SurveyStates { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<UserProduct>? UserProducts { get; set; }


        public SensoStatDbContext(DbContextOptions<SensoStatDbContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SurveyInstruction>().HasKey(si => new { si.SurveyId, si.InstructionId });
            modelBuilder.Entity<UserProduct>().HasKey(us => new { us.UserId, us.ProductId });
        }

    }
}
