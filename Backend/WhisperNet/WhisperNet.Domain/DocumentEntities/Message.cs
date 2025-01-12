using MongoDB.Bson;

namespace WhisperNet.Domain.DocumentEntities;

public class Message
{
    public required ObjectId Id { get; set; }
    public required string SenderId { get; set; }
    public required string Text { get; set; }
    public required DateTime CreateAt { get; set; }
    public DateTime UpdatedAt { get; set; } 
}