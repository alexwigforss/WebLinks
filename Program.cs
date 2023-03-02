using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using static System.Console;

namespace WebLinks
/*-------------------------- WebLinks.cs --------------------------
 * ----------------------------------------------------------------
 * ---------------- Console Application for weblinks---------------
 * -----------------     PLACEHOLDER HEADER -----------------------
 *  add: Project name, developers, date, purpose
 *  add: Format for weblinks save files and instructions!
 */
{
    internal class Program
    {
        public List<string> weblinks = new();
        static void Main(string[] args)
        {
            string path = ".\\files\\";
            System.IO.Directory.CreateDirectory(path);
            string filename = "Weblinks.txt";
            Program p = new Program();
            PrintWelcome();

            string command;
            do
            {
                Console.Write(": ");
                command = ReadLine();
                if (command == "quit")
                {
                    WriteLine("Good bye!");
                }
                else if (command == "help")
                {
                    WriteTheHelp();
                }
                else if (command == "load")
                {                    
                    string loadPath;
                    //ImportLinksFromFile(loadPath); //Isak
                    ImportLinksFromFile(command);
                }
                else if (command.Split()[0] == "open")
                {
                    // NotYetImplemented("open");
                    OpenWeblink(command); //Alex
                }
                else if (command == "list")
                {
                    p.ListLinks();
                    PrintContinue();
                }
                else if (command == "add")
                {
                    string addName, addUrl, addInfo;
                    Console.Write("Add: Lägg till en länk i listan (namn, url, beskrivning)\nNamn: ");
                    addName = Console.ReadLine();
                    Console.Write("URL: ");
                    addUrl = Console.ReadLine();
                    Console.Write("Beskrivning: ");
                    addInfo = Console.ReadLine();
                    p.AddLink(addName, addUrl, addInfo);
                    Console.WriteLine("\n");
                    PrintContinue();
                }
                else if (command == "save")
                {
                    NotYetImplemented("save");
                    string saveFile;
                    //SaveWebLinks(saveFile); //Isak
                }
                else
                {
                    WriteLine($"Unknown command '{command}'");
                }
            } while (command != "quit" && command != "exit");
        }

        private static void NotYetImplemented(string command)
        {
            WriteLine($"Sorry: '{command}' is not yet implemented");
        }

        private static void PrintWelcome()
        {
            WriteLine("Hello and welcome to the ... program ...");
            WriteLine("that does ... something.");
            WriteLine("Write 'help' for help!");
        }

        private static void PrintContinue()
        {
            WriteLine("\nNext command?");
        }

        private static void WriteTheHelp()
        {
            string[] hstr = {
                "help  - display this help",
                "load  - load all links from a file",
                "open  - open a specific link",
                "quit  - quit the program"
            };
            foreach (string h in hstr) Console.WriteLine(h);
        }
        public static void ImportLinksFromFile(string path)
        //ImportLinksFromFile - loads weblinks from a standardfile (ex. weblinks.lis)
        //Links consists of a name, description and URL
        {           
            string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string filePath = Path.Combine(homeDirectory, "source", "repos", "WebLinks", "Weblinks.txt");
            string text = File.ReadAllText(filePath);

            string[] rows = text.Split("\n");

            var n = 0;

            foreach (string row in rows)
            {
                n++;
                Console.WriteLine($"{n}, {row}");
            }           
        }
        public void ListLinks()
        //Lists all weblinks currently loaded into weblinks list.
        {
            Console.WriteLine("\nWeblinks:\n\n");
            int i = 0;
            weblinks.ForEach(link =>
            {
                Console.WriteLine(
                $"{++i}: " +
                $"Namn:        {link.Split(',')[0]}\n" +
                $"   Beskrivning: {link.Split(',')[1]}\n" +
                $"   URL          {link.Split(',')[2]}\n");
            });
        }
        public static void OpenWeblink(string Link)
        //Opens a link from the weblinks array in native browser
        {
            string[] splString = Link.Split(' ');
            if (splString.Length == 1 ) {
                WriteLine("Ange Länk och tryck på enter");
                //ReadLine();
                BrowserProces(@ReadLine());
                // Kommentar, här skulle man kunna checka så att det är en url som angivits
            }
            else if (splString.Length == 2)
            {
                WriteLine("Rätt mängd data för att utföra åtgärden");
                BrowserProces(splString[1]);
            }
            else if (splString.Length > 2)
            {
                WriteLine("För mkt data men vi kastar bort överflödet");
                BrowserProces(splString[1]);
            }
            //BrowserProces(@"http://google.com");
            // code...
        }
        /// <summary>
        /// Ska öppna default webbläsaren med vald länk
        /// </summary>
        public static void BrowserProces(string Link)
        {
            using (Process browspr = new Process())
            {
                browspr.StartInfo.FileName = Link;
                browspr.StartInfo.UseShellExecute = true;
                browspr.Start();
            }
        }

        public void AddLink(string name, string url, string info)
        //Add a weblink to the array from console.
        {
            weblinks.Add($"{name},{info},{url}");
        }
        public static void SaveWebLinks(string path)
        //Save the current weblinks array to file
        {
            //code...
        }
    }
}