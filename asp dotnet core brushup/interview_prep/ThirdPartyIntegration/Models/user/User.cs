using interview_prep.Models.application;
using interview_prep.Models.job;
using Microsoft.AspNetCore.Identity;

namespace interview_prep.Models.user;

public class User : IdentityUser
{
    // admin can post many jobs
    public ICollection<Job> PostedJobs = new List<Job>();
    
    // user can apply to many jobs
    public ICollection<Application> Applications = new List<Application>();
    
}

// properties provided by IdentityUser class
// AccessFailedCount	
// Claims	
// ConcurrencyStamp	
// Email	
// EmailConfirmed	
// Id	
// LockoutEnd	
// Logins	
// NormalizedEmail	
// NormalizedUserName	
// PasswordHash	
// PhoneNumber	
// PhoneNumberConfirmed	
// Roles	
// SecurityStamp	
// TwoFactorEnabled	
// UserName	
