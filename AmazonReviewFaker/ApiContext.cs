using AmazonReviewFaker.Models;
using Microsoft.EntityFrameworkCore;
namespace AmazonReviewFaker
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring
        (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ReviewDb");
        }
        public DbSet<AmazonFakeReview> Reviews { get; set; }
    }
}