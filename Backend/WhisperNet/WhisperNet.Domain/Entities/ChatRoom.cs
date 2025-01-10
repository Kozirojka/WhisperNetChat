namespace WhisperNet.Domain.Entities;

public class ChatRoom
{
    public required string Id { get; set; }
    public ICollection<ChatParticipants> UserChats { get; set; } = new List<ChatParticipants>();
    
    
    public bool IsPrivate { get; set; }
    
    
    
    //does people can be added in this chatRoom
    public bool IsShared { get; set; }
    
    public DateTime Created { get; set; }
    
    
}