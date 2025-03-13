namespace FileIO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Reading and Writing Basic Text
            // if we don't specify the directory of a file, the default directory is used,
            // 
            string classListFileName = "classlist.txt";
            string numbersFileName = "numbers.txt";
            StreamReader reader;
            // File.Exists will take a file path as an argument and return a bool if
            // the file exists. 
            if (File.Exists(classListFileName))
            {
                // put stream reading and writing operations in a try/catch case there are problems 
                try
                {
                    // create a new StreamReader object passing in the file path as an argument. 
                    reader = new StreamReader(classListFileName);

                    // The EndOfStream property on the StreamReader object will return a bool
                    // indicating if we have reached the end of a stream of data.
                    
                    // Since we may not know how large the file is, we will use a while loop
                    while (!reader.EndOfStream)
                    {
                        // reader.ReadLine() will read a single line of a file.
                        // Once we finish reading the last line of a file, EndOfStream will
                        // become 'true' and the loop will terminate.

                        string line = reader.ReadLine();
                        Console.WriteLine(line);
                    }

                    //We must always close the stream after we are done using it
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"There was a problem: {e.Message}");
                }
            }

            try
            {
                // We can simplify the use of a Stream class by using the "using" keyword.
                // If we put the Initialization of the StreamWriter class inside of the "using" statement
                // then the scope of that object will be contained to the "using" block.
                // In this way we do not have to use the Close() method explicitly since it will be done once
                // the using block is done executing.

                // StreamWriter will allow us to write to a file. If the file does not exist it will be created
                                
                using (StreamWriter writer = new StreamWriter(numbersFileName))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        writer.WriteLine(i + 1);
                    }
                }

                // When we create a StreamWriter, by default the cursor starts at the very beginning of the file,
                // meaning anything we write will overwrite the existing text.

                using (StreamWriter writer = new StreamWriter(numbersFileName))
                {
                    for (int i = 5; i < 10; i++)
                    {
                        writer.WriteLine(i + 1);
                    }
                }

                // If we want to append to the file (add to it) instead of overwriting it, we can add pass a second
                // argument (a boolean) telling the stream writer to append. (a value of 'true' means 'append')

                using (StreamWriter writer = new StreamWriter(numbersFileName, true))
                {
                    for (int i = 10; i < 15; i++)
                    {
                        writer.WriteLine(i + 1);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"There was a problem: {e.Message}");
            }
            #endregion

            #region Reading Comma Separated Values (CSV)

            // Comma separated values are a series of values that are separated by, you guessed it, commas
            // CSVs are a very common way of storing simple pieces of data in a text file. 

            // Imagine we have a text file that holds the first name, last name, and student ID of a group of students.
            // A single student record would be stored on a single line in the text file, like this:
            // <firstName>,<lastName>,<studentID>
            
            // The second record would be stored in exactly the same way on the second line, then the third record, and so on.
            
            // Two approaches will be taken to demonstrate this example

            string studentsFile = "students.csv";

            if (File.Exists(studentsFile))
            {
                // File.ReadAllLines will return an array of strings
                // each item in the array is one line in the text file
                // if we know it is a CSV 

                string[] students = File.ReadAllLines(studentsFile);

                // loop through each string in the array
                for (int i = 0; i < students.Length; i++)
                {
                    // Split() takes in a char as a delimiter and returns 
                    // an array of string.
                    // The array is made up of each string that was separated
                    // by the specified delimiter

                    // Example, if we have the following string:
                    // string myString = jared,karpiak,123
                    // and use myString.Split(',');
                    // we will have an array of strings with 3 items.
                    // The first string will be "jared"
                    // The second string will be "karpiak"
                    // The third string will be "123"

                    string[] record = students[i].Split(',');

                    for (int j = 0; j < record.Length; j++)
                    {
                        // With string interpolation we can use an integer after
                        // our string to add padding. This integer represents
                        // the total width of the string. If the string length
                        // is smaller than the integer, then whitespace will be
                        // added. A negative number represents padding on the right
                        // A positive integer padding on the left.
                        Console.Write($"{record[j],-20}");
                    }
                    
                    // Often times a CSV will have a header row (names of the column)
                    // This will be the first row in a CSV file.
                    // We can add some additional text styling based on this

                    if (i == 0)
                    { 
                        // Create a newline character as a string
                        // We can then use the PadRight string method
                        // to add 60 dashes.
                        Console.WriteLine("\n".PadRight(60, '-'));
                    }

                    Console.WriteLine();
                }
            }
            #endregion

            #region Writing Comma Separated Values (CSV)

            // To create a Comma Separated Value file, we can follow
            // the same method as with writing a text file, using
            // the StreamWriter class

            string actorCsvFileName = "actors.csv";
            string actors = "FirstName,LastName,Ability;" +
                "Keanu,Reeves,Meh;" +
                "Awkwa,Fina,Questionable;" +
                "Cillian,Murphy,Oscar Worthy;" +
                "Michelle,Yeoh,Excellent";

            using (StreamWriter sw = new StreamWriter(actorCsvFileName))
            {
                // create an array to store each full record
                // Split() can take any character as a delimiter
                string[] actorRecords = actors.Split(';');

                // the foreach loop can be applied to a collection, like
                // an array or a list.

                // It will iterate through each item in the collection
                // and create a temporary variable for it that can only
                // be used inside of the foreach loop (the scope of the
                // variable is in the foreach).
                foreach (string record in actorRecords)
                {
                    // write each record to the file (including the commas
                    // since we are creating a csv file)
                    sw.WriteLine(record);
                }
            }

            #endregion
        }
    }
}
