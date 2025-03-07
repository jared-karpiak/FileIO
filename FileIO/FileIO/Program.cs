using System.IO;

namespace FileIO
{
    internal class Program
    {
        static void Main(string[] args)
        {
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

        }
    }
}
