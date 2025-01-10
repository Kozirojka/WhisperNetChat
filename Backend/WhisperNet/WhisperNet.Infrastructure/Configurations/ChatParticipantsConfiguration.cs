using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhisperNet.Domain.Entities;

namespace WhisperNet.Infrastructure.Configurations;

public class ChatParticipantsConfiguration : IEntityTypeConfiguration<ChatParticipants>
{
    public void Configure(EntityTypeBuilder<ChatParticipants> builder)
    {
        builder.HasKey(cp => cp.Id);

        builder
            .HasOne(cp => cp.ChatRoom)
            .WithMany()
            .HasForeignKey(cp => cp.ChatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(cp => cp.User)
            .WithMany()
            .HasForeignKey(cp => cp.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}