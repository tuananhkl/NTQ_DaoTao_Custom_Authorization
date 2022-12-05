using Microsoft.EntityFrameworkCore;
namespace CustomAuthorization.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>().Property(x => x.DateOfBirth).HasColumnType("datetime");

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                UserName = "tuananh",
                Password = "Tuananh123.",
                DateOfBirth = new DateTime(1999, 08, 30),
                Address = "Hanoi",
                Email = "tuananh@gmail.com",
                Age = 23,
                Gender = true,
                Status = true,
                GroupId = 1
            });

        modelBuilder.Entity<Group>().HasData(
            new Group
            {
              Id  = 1,
              GroupName = "Admin"
            });

        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = 1,
                Controller = "User",
                Action = "Index"
            });

        modelBuilder.Entity<UserRole>().HasData(
            new UserRole
            {
                Id = 1,
                UserId = 1,
                RoleId = 1,
                Status = true
            }
            );
    }
}