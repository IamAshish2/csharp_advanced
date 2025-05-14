using System.IO.Compression;
using GradingLib.delegates;
using GradingLib.Models;

namespace GradesApplication;
class Program
{
    static Exam exam = new Exam();

    static void Main(string[] args)
    {
        InitializeStudents();

        var deleg = new TopGrade(HighGrade);

        var result = exam.CalculateAverageGrade(deleg);
        Console.WriteLine($"The average grade for the students is {result}");


        // pass or fail
        var passOrFailDeleg = new PassOrFailDeleg(PassOrFail);
        exam.PassOrFail(passOrFailDeleg);

        exam.PrintStudentsDetails();
    }

    public static bool PassOrFail(double grade)
    {
        if (grade < 40) return false;
        if (grade >= 40 && grade <= 100) return true;
        return false;
    }

    public static void HighGrade(double grade)
    {
        Console.WriteLine($"The highest grade is {grade}.");
    }

    private static void InitializeStudents()
    {
        exam.Students.Add(new Student
        {
            StudentName = "Ashish",
            Grade = 90,
        });

        exam.Students.Add(new Student
        {
            StudentName = "Mausam",
            Grade = 20,
        });

        exam.Students.Add(new Student
        {
            StudentName = "Batman",
            Grade = 100,
        });

    }
}