using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public static class OriginalTextFileProcessor
    {
        public static List<Person> LoadPeople(string filePath)
        {
            List<Person> output = new List<Person>();
            Person p;
            var lines = File.ReadAllLines(filePath).ToList();

            // Remove the header row
            lines.RemoveAt(0);

            foreach (var line in lines)
            {
                var vals = line.Split(',');
                p = new Person();

                p.FirstName = vals[0];
                p.IsAlive = bool.Parse(vals[1]);
                p.LastName = vals[2];
                
                output.Add(p);
            }

            return output;
        }

        public static void SavePeople(List<Person> people, string filePath)
        {
            List<string> lines = new List<string>();

            // Add a header row for the csv file
            lines.Add("FirstName,IsAlive,LastName");

            foreach (var p in people)
            {
                lines.Add($"{ p.FirstName },{ p.IsAlive },{ p.LastName }");
            }

            System.IO.File.WriteAllLines(filePath, lines);
        }


        public static void SaveLogEntry(List<LogEntry> logs, string filePath)
        {
            List<string> lines = new List<string>();

            // add the header
            lines.Add("Error Code, Message, TimeOfEvent");

            // loop through the list and add it to the line
            foreach (var log in logs)
            {
                lines.Add($"{log.ErrorCode},{log.Message},{log.TimeOfEvent}");
            }

            // save the list to a csv file using File class
            File.WriteAllLines (filePath, lines);
        }


        public static List<LogEntry> ReadAllLogEntries(string filePath)
        {
            List<LogEntry> logEntries = new List<LogEntry>();
            LogEntry l;

            // read the file from the filepath and convert it to list
           List<string> lines = File.ReadAllLines(filePath).ToList();

            // remove the header
            lines.RemoveAt(0);

            // loop through the content of the file and save it to logEntries
            foreach (string line in lines)
            {
                l = new LogEntry();
                string [] values = line.Split(',');

                l.ErrorCode = int.Parse(values[0]);
                l.Message = values[1];
                l.TimeOfEvent = DateTime.Parse(values[2]);

                logEntries.Add(l);
            }
            
            return logEntries;
        }
    }
}
