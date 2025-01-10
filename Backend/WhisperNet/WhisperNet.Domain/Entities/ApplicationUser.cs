using Microsoft.AspNetCore.Identity;

namespace WhisperNet.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string  FirstName { get; set; }
    public string  LastName { get; set; }
    
   // public ICollection<ChatParticipants> UserChats { get; set; } = new List<ChatParticipants>();
}