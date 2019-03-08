using Microsoft.EntityFrameworkCore;
using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Data
{
    public class DataContext : DbContext
    {
       public DataContext(DbContextOptions<DataContext> options) : base (options) {}

       public DbSet<Value> Values { get; set; }
    }
}