using GradingLib.delegates;

namespace GradingLib.Models
{
    public class Exam
    {
        public List<Student> Students { get; set; } = new List<Student>();

        public double CalculateAverageGrade(TopGrade topGrade)
        {
            topGrade.Invoke(Students.Max(s => s.Grade));
            return Students.Sum(x => x.Grade) / Students.Count;
        }

        public void PassOrFail(PassOrFailDeleg check)
        {
            foreach (var student in Students)
            {
                student.PassOrFail = check.Invoke(student.Grade);
            }
        }

        public void PrintStudentsDetails()
        {
            foreach (var student in Students)
            {
                Console.WriteLine($"The grade obtained by {student.StudentName} is {student.Grade} and the student has {student.PassOrFail} ");
            }
        }
    }
}