using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Production_Planning_System.Areas.Identity.Data;
using Production_Planning_System.Models;

namespace Production_Planning_System.Data;

public class Production_Planning_SystemContext : IdentityDbContext<Production_Planning_SystemUser>
{
    public Production_Planning_SystemContext(DbContextOptions<Production_Planning_SystemContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    internal Task UpdateAsync(Administration administration)
    {
        throw new NotImplementedException();
    }

    public DbSet<Production_Planning_System.Models.Administration> Administration { get; set; }

    public DbSet<Production_Planning_System.Models.Production> Production { get; set; }

    public DbSet<Production_Planning_System.Models.Sales> Sales { get; set; }
   
}
