using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithTextFiles
{
    class Program
    {

        static string ReadSetting(string fileName, string searchString) // Read entire file to console
        {
            string setting = null;

            StreamReader file = null;
            try
            {

                file = new StreamReader(fileName);
                int lineCount = 0;
                using (file)
                {
                    Console.WriteLine("File: " + fileName + "\n\n");

                    // first count the number of lines in the file
 
                    string lines = file.ReadLine();
                    //Console.WriteLine(lines);

                    while (lines != null)
                    {
                        lines = file.ReadLine();
                       // Console.WriteLine(lines);
                        lineCount++;
                    }

                    Console.WriteLine("Line count = " + lineCount + "\n\n");
                }

                // now create an array of strings, one string per line
                file = new StreamReader(fileName);
                using (file)
                {
                    string[] line = new string[lineCount];

                    char[] trimChars = new char[] {'<','>',' ',',','.','?'};

                    //and populate array with lines from file
                    for (int i = 0; i < lineCount; i++)
                    {
                        line[i] = file.ReadLine();
                        Console.WriteLine("Line {0}:         {1}", (i + 1), line[i]);
                        if (line[i].IndexOf(searchString) != -1)
                        {
                            setting = line[i].Replace(searchString, "");
                            setting = setting.Trim(trimChars);
                        }
                    }

                }    
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
            }
            catch (IOException)
            {
                Console.WriteLine("Could not access file!");
            }
        return setting;
        }


        static void WriteSetting(string filename, string setting, string newValue)
        {
            int lineOfSetting = -1;
            int lineNum = 1;

            try
            {
                StreamReader fileRead = new StreamReader(filename);
                using (fileRead)
                {
                    string lineRead = fileRead.ReadLine();

                    while (lineRead != null)
                    {
                        int index = lineRead.IndexOf(setting);
                        if (index > -1)
                        {
                            lineOfSetting = lineNum; // this is the number of the line which we need to change
                        }
                        lineNum++;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("unable to read file during settings change.");
            }


            try
            { 
                StreamWriter fileWrite = new StreamWriter(filename);
                using (fileWrite)
                {
                    fileWrite.WriteLine(newValue);
                    Console.WriteLine(newValue + "written");
                }
                    
        
            }
            catch (Exception)
            {
                Console.WriteLine("unable to write to file.");
            }
        }



        static void Main()
        {

            /*  This section allows user to enter filename (path is always .\config\)
             
            Console.Write("Enter filename: ");
            string file1 = ".\\config\\" + Console.ReadLine();
            Console.WriteLine("Filename = " + file1); */

            string file1 = @".\config\config.txt";
            string dirName = "config";

            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
                Console.WriteLine("Directory \"{0}\" created.", dirName);
            }


            //Define settings variables
            string setting1 = ReadSetting(file1, "setting1");
            string setting2 = ReadSetting(file1, "setting2");
            string lengthStr = ReadSetting(file1, "length");
            int length = Int32.Parse(lengthStr);
            string heightStr = ReadSetting(file1, "height");
            int height = int.Parse(heightStr);


            try
            {
                setting1 = ReadSetting(file1, "setting1");
                setting2 = ReadSetting(file1, "setting2");
                lengthStr = ReadSetting(file1, "length");
                length = Int32.Parse(lengthStr);
                heightStr = ReadSetting(file1, "height");
                height = int.Parse(heightStr);

                Console.WriteLine("Setting 1: " + setting1);
                Console.WriteLine("Setting 2: " + setting2);
                Console.WriteLine("Length: " + length);
                Console.WriteLine("Height: " + height);


            }
            catch(Exception)
            {
                Console.WriteLine("Problem encountered");
            }

            // Now confirm that values from config file are used by app


            Console.WriteLine("area = " + (length * height));


            // Now write to file

            WriteSetting(file1, setting2, "boo");

            Console.WriteLine("\n\nDo more stuff here...\n\n");
        }
    }
}
