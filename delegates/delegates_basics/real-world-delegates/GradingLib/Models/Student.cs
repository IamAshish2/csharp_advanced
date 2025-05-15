namespace GradingLib.Models
{
    public class Student
    {
        public required string StudentName { get; set; }
        public double Grade { get; set; }
        public Boolean PassOrFail { get; set; }
    }
}