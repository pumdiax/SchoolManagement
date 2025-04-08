using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models;

namespace SchoolManagement.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    
    public DbSet<UserDetail> UserDetail { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the one-to-many relationship between Role and UserDetail
        modelBuilder.Entity<UserDetail>()
            .HasOne(ud => ud.Role)
            .WithMany(r => r.UserDetail)
            .HasForeignKey(ud => ud.RoleId);
    }
}
