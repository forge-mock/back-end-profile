namespace Profile.Application.DTOs;

public class PasswordUpdateDto
{
    public string UserEmail { get; set; } = string.Empty;

    public string OldPassword { get; set; } = string.Empty;

    public string NewPassword { get; set; } = string.Empty;
}