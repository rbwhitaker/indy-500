using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indy500
{
    static class GameSettings
    {
        // Window
        public static bool IsFullScreen { get; private set; }
        public static int Width { get; private set; }
        public static int Height { get; private set; }

        //Debug
        public static bool Debug { get; private set; }

        //Gameplay
        public static string Mode { get; set; }
        public static int Laps { get; set; }
        public static string Map { get; set; }

        //Mapping
        public static Keys LeftKey { get; set; }
        public static Keys RightKey { get; set; }
        public static Keys Accelerate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public static void Initialize()
        {
            ParseConfigFile();
        }
        
        private static void ParseConfigFile()
        {
            string currentConfigTag = String.Empty;
            try
            {
                using (StreamReader streamReader = new StreamReader("config.txt"))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string currentLine = streamReader.ReadLine();
                        if (currentLine.StartsWith("["))
                        {
                            currentConfigTag = currentLine;
                            currentLine = streamReader.ReadLine();
                        }
                        if (currentLine == " ")
                        {
                            currentConfigTag = String.Empty;
                            continue;
                        }

                        if (currentConfigTag == "[Window]")
                        {
                            //This is the same currentLine(string) but split, 0 index is the name and 1 is the value.
                            var splitCurrentLine = currentLine.Split('=');

                            if (splitCurrentLine[0] == "FullScreen")
                                IsFullScreen = Boolean.Parse(splitCurrentLine[1]);
                            else if (splitCurrentLine[0] == "Width")
                                Width = Int32.Parse(splitCurrentLine[1]);
                            else if (splitCurrentLine[0] == "Height")
                                Height = Int32.Parse(splitCurrentLine[1]);
                        }
                        else if (currentConfigTag == "[Debug]")
                        {
                            var splitCurrentLine = currentLine.Split('=');
                            if (splitCurrentLine[0] == "Enabled")
                                Debug = Boolean.Parse(splitCurrentLine[1]);
                        }
                        else if (currentConfigTag == "[Gameplay]")
                        {
                            // To be implemented
                        }
                        else if(currentConfigTag == "[Mapping]")
                        {
                            var splitCurrentLine = currentLine.Split('=');
                            if (splitCurrentLine[0] == "Left")
                                LeftKey = (Keys)Enum.Parse(typeof(Keys), splitCurrentLine[1]);
                            if (splitCurrentLine[0] == "Right")
                                RightKey = (Keys)Enum.Parse(typeof(Keys), splitCurrentLine[1]);
                            if (splitCurrentLine[0] == "Accelerate")
                                Accelerate = (Keys)Enum.Parse(typeof(Keys), splitCurrentLine[1]);

                        }
                    }
                }
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine("An exception of file not found has occured: " + e.Message);
            }
        }

    }
}
