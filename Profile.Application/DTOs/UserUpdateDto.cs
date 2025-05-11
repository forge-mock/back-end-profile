namespace Profile.Application.DTOs;

public class UserUpdateDto
{
    public string OldUserEmail { get; set; } = string.Empty;
    
    public string NewUserEmail { get; set; } = string.Empty;
    
    public string Username { get; set; } = string.Empty;
}