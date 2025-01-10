using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WhisperNet.Domain;
using WhisperNet.Domain.Entities;
using WhisperNet.Infrastructure.Configurations;

namespace WhisperNet.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    
    
    public DbSet<ChatRoom> ChatRooms { get; set; }     
    public DbSet<ChatParticipants> ChatParticipants { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ChatParticipantsConfiguration());
        
    }
}