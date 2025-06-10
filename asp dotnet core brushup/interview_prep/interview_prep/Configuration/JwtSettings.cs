namespace interview_prep.Configuration;

public class JwtSettings
{
    public string? Key { get; set; }
    public string? Audience { get; set; }
    public string? Issuer { get; set; }
    public int ExpireMinutes { get; set; }
}