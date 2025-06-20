﻿namespace interview_prep.Dto.UserDTO;

public class CreateUserDTO
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public required string Password { get; set; }
}