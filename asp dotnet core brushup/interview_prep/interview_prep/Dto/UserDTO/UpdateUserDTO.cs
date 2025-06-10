namespace interview_prep.Dto.UserDTO;

public class UpdateUserDTO
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
}