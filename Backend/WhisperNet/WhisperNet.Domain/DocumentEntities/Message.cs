using MongoDB.Bson;

namespace WhisperNet.Domain.DocumentEntities;

public class Message
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string? SenderId { get; set; }
    public required string Text { get; set; }
    public required DateTime CreateAt { get; set; }
    public DateTime? UpdatedAt { get; set; } 
}