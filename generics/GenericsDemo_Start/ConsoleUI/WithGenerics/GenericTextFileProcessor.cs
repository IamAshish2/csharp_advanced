using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public static class GenericTextFileProcessor
    {
        /*
        public static List<T> LoadFromTextFile<T>(string filePath) where T : class, new()
        {
            var lines = System.IO.File.ReadAllLines(filePath).ToList();
            List<T> output = new List<T>();
            T entry = new T();
            var cols = entry.GetType().GetProperties();

            // Checks to be sure we have at least one header row and one data row
            if (lines.Count < 2)
            {
                throw new IndexOutOfRangeException("The file was either empty or missing.");
            }

            // Splits the header into one column header per entry
            var headers = lines[0].Split(',');

            // Removes the header row from the lines so we don't
            // have to worry about skipping over that first row.
            lines.RemoveAt(0);

            foreach (var row in lines)
            {
                entry = new T();

                // Splits the row into individual columns. Now the index
                // of this row matches the index of the header so the
                // FirstName column header lines up with the FirstName
                // value in this row.
                var vals = row.Split(',');

                // Loops through each header entry so we can compare that
                // against the list of columns from reflection. Once we get
                // the matching column, we can do the "SetValue" method to 
                // set the column value for our entry variable to the vals
                // item at the same index as this particular header.
                for (var i = 0; i < headers.Length; i++)
                {
                    foreach (var col in cols)
                    {
                        if (col.Name == headers[i])
                        {
                            col.SetValue(entry, Convert.ChangeType(vals[i], col.PropertyType));
                        }
                    }
                }

                output.Add(entry);
            }

            return output;
        }

        public static void SaveToTextFile<T>(List<T> data, string filePath) where T : class, new()
        {
            List<string> lines = new List<string>();
            StringBuilder line = new StringBuilder();

            if (data == null || data.Count == 0)
            {
                throw new ArgumentNullException("data", "You must populate the data parameter with at least one value.");
            }
            var cols = data[0].GetType().GetProperties();

            // Loops through each column and gets the name so it can comma 
            // separate it into the header row.
            foreach (var col in cols)
            {
                line.Append(col.Name);
                line.Append(",");
            }

            // Adds the column header entries to the first line (removing
            // the last comma from the end first).
            lines.Add(line.ToString().Substring(0, line.Length - 1));

            foreach (var row in data)
            {
                line = new StringBuilder();

                foreach (var col in cols)
                {
                    line.Append(col.GetValue(row));
                    line.Append(",");
                }

                // Adds the row to the set of lines (removing
                // the last comma from the end first).
                lines.Add(line.ToString().Substring(0, line.Length - 1));
            }

            System.IO.File.WriteAllLines(filePath, lines);
        }
        */
        
        public static List<T> LoadFromTextFile<T>(string filePath) where T : class, new()
        {
            var lines = File.ReadAllLines(filePath).ToList();
            List<T> output = new List<T>();

            if (lines.Count < 2)
            {
                throw new IndexOutOfRangeException("The file was empty or contained no data.");
            }

            // 
            T entry = new T();

            // uses reflection to get the properties of the type passed to the method
            var cols = entry.GetType().GetProperties();

            // get the header of the file
            var header = lines[0].Split(',');

            // remove the header row
            lines.RemoveAt(0);

            foreach (var row in lines)
            {
                entry = new T();

                // split the row of data by comma
                var values = row.Split(',');

                // loop through the header
                for (int i = 0; i < values.Length; i++ )
                {
                    foreach (var col in cols)
                    {
                        if (col.Name == header[i])
                        {
                            // set the value of the property at run time
                            col.SetValue(entry, Convert.ChangeType(values[i], col.PropertyType));
                        }
                    }

                }
                // now after the foreach loop the entry which is a instance of T() will have the values from the reflection
                output.Add(entry);
            }

            return output;
        }

        public static void SaveToFile<T>(List<T> values, string filePath) where T : class, new()
        {
            List<string> lines = new List<string>();
            StringBuilder line = new StringBuilder();

            
            if (values.Count < 2)
            {
                throw new IndexOutOfRangeException("The data was not found!");
            }

            T type = new T();
            // use reflection to get the properties of the type at runtime
            var properties =  type.GetType().GetProperties();

            foreach (var prop in properties)
            {
                line.Append(prop.Name);
                line.Append(',');
            }

            // add the header row to the final lines list 
            // substring is used to remove the comma at the end of the line variable
            lines.Add(line.ToString().Substring(0, line.Length - 1));

            // now loop through the values list
            foreach (var row in values)
            {
                line = new StringBuilder();

                foreach (var col in properties)
                {
                    line.Append(col.GetValue(row));
                    line.Append(",");
                }

                lines.Add(line.ToString().Substring(0, line.Length - 1));
            }

            File.WriteAllLines(filePath,lines);

        }
    }
}
