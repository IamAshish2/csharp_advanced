namespace interview_prep.Dto.UserDTO;

public class GetUserDetailsDTO
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
}