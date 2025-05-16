using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {

            DemonstrateTextFileStorage();
            
            Console.WriteLine();
            Console.Write("Press enter to shut down...");
            Console.ReadLine();
        }

        // OriginalTextFileProcessor is the class that handles the read and write to the csv files
        private static void DemonstrateTextFileStorage()
        {
            List<Person> people = new List<Person>();
            List<LogEntry> logs = new List<LogEntry>();

            string peopleFile = "C:\\Users\\ashish\\Desktop\\1_c# advanced\\generics/peopleFile.csv";
            string logFile = "C:\\Users\\ashish\\Desktop\\1_c# advanced\\generics/logFile.csv";


            PopulateLists(people, logs);

            /* OLD WAY OF DOING THINGS without generics
            OriginalTextFileProcessor.SavePeople(people, peopleFile);
            Console.WriteLine("Processor");

            var newPeople = OriginalTextFileProcessor.LoadPeople(peopleFile);

            foreach (var p in newPeople)
            {
                Console.WriteLine($"{ p.FirstName } { p.LastName } (IsAlive = { p.IsAlive })");
            }


            Console.WriteLine();


            // for log entry
            OriginalTextFileProcessor.SaveLogEntry(logs, logFile);
            
            var logEntries = OriginalTextFileProcessor.ReadAllLogEntries(logFile);

            foreach (var l in logEntries)
            {
                Console.WriteLine($"{l.ErrorCode} {l.Message}  {l.TimeOfEvent.ToShortDateString()}");
            }
            */


            // With Generics
            GenericTextFileProcessor.SaveToFile<Person>(people,peopleFile);
            var peoples = GenericTextFileProcessor.LoadFromTextFile<Person>(peopleFile);

            foreach (var p in people)
            {
                Console.WriteLine($"{p.FirstName} {p.LastName} (IsAlive = {p.IsAlive})");
            }

            Console.WriteLine();

            GenericTextFileProcessor.SaveToFile<LogEntry>(logs, logFile);
            var output = GenericTextFileProcessor.LoadFromTextFile<LogEntry>(logFile);

            foreach (var o in output)
            {
                Console.WriteLine($"{o.ErrorCode} {o.Message} (IsAlive = {o.TimeOfEvent.ToShortDateString()})");
            }
        }

        private static void PopulateLists(List<Person> people, List<LogEntry> logs)
        {
            people.Add(new Person { FirstName = "Tim", LastName = "Corey" });
            people.Add(new Person { FirstName = "Sue", LastName = "Storm", IsAlive = false });
            people.Add(new Person { FirstName = "Greg", LastName = "Olsen" });
            people.Add(new Person { FirstName = "Ashish", LastName = "Karki" });
            people.Add(new Person { FirstName = "Riya", LastName = "Khadka" });

            logs.Add(new LogEntry { Message = "I blew up", ErrorCode = 9999 });
            logs.Add(new LogEntry { Message = "I'm too awesome", ErrorCode = 1337 });
            //logs.Add(new LogEntry { Message = "I was tired", ErrorCode = 2222 });
        }
    }
}
