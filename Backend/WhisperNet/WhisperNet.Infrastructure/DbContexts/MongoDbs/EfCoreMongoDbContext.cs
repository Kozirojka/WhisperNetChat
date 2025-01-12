using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using WhisperNet.Domain.DocumentEntities;

namespace WhisperNet.Infrastructure.DbContexts.MongoDbs;

public class EfCoreMongoDbContext(DbContextOptions options) : DbContext(options)
{
    
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        
        // todo: що це таке? property 
        modelBuilder.Entity<Message>()
            .ToCollection("messages")
            .Property(m => m.CreateAt).HasConversion<string>();
    }
}