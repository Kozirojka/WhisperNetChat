using WhisperNet.Domain.Entities;
using WhisperNet.Infrastructure.Configurations;


using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WhisperNet.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


    public DbSet<ChatRoom> ChatRooms { get; set; }
    public DbSet<ChatParticipants> ChatParticipants { get; set; }
    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ChatParticipantsConfiguration());




        builder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "9e4f49fe-0786-44c6-9061-53d2aa84fab3",
                Name = "User",
                NormalizedName = "USER"
            },
            new IdentityRole
            {
                Id =  "b8d5e64c-2dd3-4237-9778-ca0f84eac96e",
                Name = "Admin",
                NormalizedName = "ADMIN"
            }
        );  


    }
}