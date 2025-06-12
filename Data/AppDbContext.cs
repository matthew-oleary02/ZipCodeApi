using Microsoft.EntityFrameworkCore;
using ZipCodeApi.Models;

namespace ZipCodeApi.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor to pass DB options
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Represents the ZipCodes table in the database
        public DbSet<ZipCode> ZipCodes { get; set; }
    }
}