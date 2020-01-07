using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace TechJobs.Models
{
    class JobData
    {
        static List<Dictionary<string, string>> AllJobs = new List<Dictionary<string, string>>();
        static bool IsDataLoaded = false;

        public static List<Dictionary<string, string>> FindAll()
        {
            LoadData();

            // Bonus mission: return a copy
            return new List<Dictionary<string, string>>(AllJobs);
        }

        /*
         * Returns a list of dictionaries with all values contained in a given column,
         * without duplicates. 
         */
        public static List<string> FindAll(string column)
        {
            LoadData();

            List<string> values = new List<string>();

            foreach (Dictionary<string, string> job in AllJobs)
            {
                string aValue = job[column];

                if (!values.Contains(aValue))
                {
                    values.Add(aValue);
                }
            }

            // Bonus mission: sort results alphabetically
            values.Sort();
            return values;
        }

        /**
         * Search all columns for the given term 
         * 
         * Use Find by value or find by column and value to return your search info.
         */
        public static List<Dictionary<string, string>> FindByValue(string value)
        {
            // load data, if not already loaded
            LoadData();

            // create the jobs list of dictionaries 
            List<Dictionary<string, string>> jobs = new List<Dictionary<string, string>>();

            // iterate across the "rows" of dictionaries in AllJobs
            foreach (Dictionary<string, string> row in AllJobs)
            {

                // iterate across the keys in each row
                foreach (string key in row.Keys)
                {
                    // ??? create aValue that holds the key for each row
                    string aValue = row[key];

                    // if the key from the row contains the incoming value (both in all lowercase)
                    if (aValue.ToLower().Contains(value.ToLower()))
                    {
                        // add the whole row to the jobs list of dictionaries 
                        jobs.Add(row);

                        // Finding one field in a job that matches is sufficient
                        break;
                    }
                }
            }
            // return the list of jobs that match your search term 
            return jobs;
        }

        /**
         * Returns results of search the jobs data by key/value, using
         * inclusion of the search term.
         *
         * For example, searching for employer "Enterprise" will include results
         * with "Enterprise Holdings, Inc".
         */
        public static List<Dictionary<string, string>> FindByColumnAndValue(string column, string value)
        {
            // load data, if not already loaded
            LoadData();
            
            //create a List of dictionaries called jobs
            List<Dictionary<string, string>> jobs = new List<Dictionary<string, string>>();

            // iterate across the "row" of dictionaries in AllJobs
            foreach (Dictionary<string, string> row in AllJobs)
            {
                //creates aValue that holds the column for each row
                string aValue = row[column];

                // if the column aValue contains the incomming value (both in lowercase)
                if (aValue.ToLower().Contains(value.ToLower()))
                {
                    // add the whole row to the jobs list of dictionaries
                    jobs.Add(row);
                }
            }
            // return the list of jobs that match your search term  
            return jobs;
        }

        /*
         * Load and parse data from job_data.csv
         */
        private static void LoadData()
        {
            //* check if this has been done already using the IsDataLoaded method from above

            if (IsDataLoaded)
            {
                return;
            }
            //* create a new string list called rows
            List<string[]> rows = new List<string[]>();

            using (StreamReader reader = File.OpenText("Models/job_data.csv"))
            {
                while (reader.Peek() >= 0)
                {
                    //* use the StreamReader to read the csv file and convert each line to an array
                    string line = reader.ReadLine();
                    string[] rowArrray = CSVRowToStringArray(line);
                    if (rowArrray.Length > 0)
                    {
                        rows.Add(rowArrray);
                    }
                }
            }
            // * remove the headers from the csv 
            string[] headers = rows[0];
            rows.Remove(headers);

            // Parse each row array into a more friendly Dictionary
            // *for each array that we just made from the rows in the csv, create a dictionary that holds each one with the 
            // * header as its key
            foreach (string[] row in rows)
            {
                Dictionary<string, string> rowDict = new Dictionary<string, string>();

                for (int i = 0; i < headers.Length; i++)
                {
                    rowDict.Add(headers[i], row[i]);
                }
                AllJobs.Add(rowDict);
            }

            IsDataLoaded = true;
        }

        /*
         * Parse a single line of a CSV file into a string array
         * the fields will be split by a , and the words will be separated by \" 
         */
        private static string[] CSVRowToStringArray(string row, char fieldSeparator = ',', char stringSeparator = '\"')
        {
            bool isBetweenQuotes = false;
            StringBuilder valueBuilder = new StringBuilder();
            List<string> rowValues = new List<string>();

            // Loop through the row string one char at a time
            foreach (char c in row.ToCharArray())
            {
                // * if the character is a , and is between the quotes then it gets added to the string
                if ((c == fieldSeparator && !isBetweenQuotes))
                {
                    rowValues.Add(valueBuilder.ToString());
                    valueBuilder.Clear();
                }
                else
                {
                    if (c == stringSeparator)
                    {
                        isBetweenQuotes = !isBetweenQuotes;
                    }
                    else
                    {
                        valueBuilder.Append(c);
                    }
                }
            }

            // Add the final value
            rowValues.Add(valueBuilder.ToString());
            valueBuilder.Clear();

            return rowValues.ToArray();
        }
    }
}
