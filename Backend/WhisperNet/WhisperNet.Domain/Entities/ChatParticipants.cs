using WhisperNet.Domain.Enums;

namespace WhisperNet.Domain.Entities;


/// <summary>
/// Кожен коритувач Application User буде мати Один 
/// </summary>
public class ChatParticipants 
{
    public int Id { get; set; }
    public int ChatId { get; set; }
    public string UserId { get; set; }
    
    public ChatRoom? ChatRoom { get; set; }    
    public ApplicationUser? User { get; set; }
    public ChatRoomRoles ChatRoomRole { get; set; }
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
}

