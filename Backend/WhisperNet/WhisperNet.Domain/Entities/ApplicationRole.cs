using Microsoft.AspNetCore.Identity;

namespace WhisperNet.Domain.Entities;

public class ApplicationRole : IdentityRole
{
    public string Description { get; set; }
}