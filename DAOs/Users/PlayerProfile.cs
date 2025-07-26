using System;

namespace ChessClubKIU.DAOs.Users;

public class PlayerProfile
{
    public int ProfileId { get; set; }               // PK
    public int UserId { get; set; }           // FK to Users table
    
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    
    public int? FideRating { get; set; }
    public int? KIURating { get; set; }
    public string? Title { get; set; }        // e.g., GM, IM, FM

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}