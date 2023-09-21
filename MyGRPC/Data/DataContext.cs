using Microsoft.EntityFrameworkCore;

namespace MyGRPC.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> o) : base(o)
    {
        
    }

    public DbSet<User> Users { get; set; }
}