using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
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
            Program p = new Program();
            string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string filePath = Path.Combine(homeDirectory, "source", "repos", "WebLinks"/*, "Weblinks.txt"*/);

            PrintWelcome();

            string command;
            do
            {
                Write(": ");
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
                    //ImportLinksFromFile(loadPath); //Isak
                    p.ImportLinksFromFile(filePath, "weblinks.txt");                   
                }
                else if (command == "directory")
                {
                    p.ShowDirectory(filePath);
                }
                else if (command.Split(' ')[0] == "open")
                {
                    p.OpenWeblink(command); //Alex
                }
                else if (command == "list")
                {
                    p.ListLinks();
                    PrintContinue();

                }
                else if (command == "add")
                {
                    if (File.Exists("zenity.exe") || File.Exists("C:\\Windows\\zenity.exe") || File.Exists("C:\\Windows\\System32\\zenity.exe")) { p.AddLinksZenity(); continue; }
                    string addName, addUrl, addInfo;
                    Write("Add: Lägg till en länk i listan (namn, url, beskrivning)\nNamn: ");
                    addName = ReadLine();
                    Write("URL: ");
                    addUrl = ReadLine();
                    Write("Beskrivning: ");
                    addInfo = ReadLine();
                    p.AddLink(addName, addUrl, addInfo);
                    WriteLine("\n");
                    PrintContinue();
                }
                else if (command.Split(' ')[0] == "save")
                {
                    p.SaveWebLinks(filePath, command);
                }
                else if (command == "pwd")
                {
                    p.PresentWorkingDirectory(filePath);
                }
                else if (command.Split(' ')[0] == "cd")
                {
                    if (command.Split(' ').Length == 2) { filePath = p.ChangeDirectory(filePath, command.Split(' ')[1]); p.PresentWorkingDirectory(filePath); }
                    else WriteLine("Syntax for 'cd' : 'cd <directory>' to go to directory    'cd ..' to move up one directory");
                }
                else if (command.Split(' ')[0] == "mkdir")
                {
                    p.CreateDirectory(filePath, command);
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
            WriteLine("Hello and welcome to the WebLinks program!\n\n");
            WriteLine("Weblinks manages a library of links stored in a local file.\nFrom the terminal you can load the file, add new links, open links in your web browser and save the list to the file.");
            WriteLine("\nWrite 'help' for help!");
        }

        private static void PrintContinue()
        {
            WriteLine("\nNext command?");
        }

        private static void WriteTheHelp()
        {
            string[] hstr = {
                "  help            - display this help",
                "  load <file.txt> - load all links from <file.txt> file to the list",
                "  pwd             - show path to current working directory",
                "  cd <folder>     - change working directory to <folder>. Use cd '..' to go up one folder",
                "  mkdir <folder>  - create new folder <folder> in working directory",
                "  directory       - show all files in working directory",
                "  add             - manually enter data for a new link to the list",
                "  list            - display all currently loaded weblinks",
                "  open <n>        - open the link with number n (1,2.3....) from the list",
                "  save <file.txt> - save current list of weblinks to file",
                "  quit            - quit the program",
                ""
            };
            foreach (string h in hstr) Console.WriteLine(h);
        }

        public void ShowDirectory(string directory)
        {
            string[] files = Directory.GetFiles(directory);

            foreach (string file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
            }
        }

        public void ImportLinksFromFile(string path, string file)
        //ImportLinksFromFile - loads weblinks from a standardfile (ex. weblinks.lis)
        //Links consists of a name, description and URL
        {                      
            string filePath = Path.Combine(path, file);
            var rows = File.ReadAllLines(filePath);

            weblinks.AddRange(rows);
            weblinks = weblinks.Distinct().ToList();
        }
        public void ListLinks()
        //Lists all weblinks currently loaded into weblinks list.
        {
            WriteLine("\nWeblinks:\n\n");
            int i = 0;
            weblinks.ForEach(link =>
            {
                WriteLine(
                $"{++i}: " +
                $"Namn:        {link.Split(',')[0]}\n" +
                $"   Beskrivning: {link.Split(',')[1]}\n" +
                $"   URL          {link.Split(',')[2]}\n");
            }
            ) ;
        }
        /// <summary>
        /// Hittar länk i listan med index eller label
        /// </summary>
        /// <param name="Link"></param>
        public void OpenWeblink(string Link)
        //Opens a link from the weblinks array in native browser
        {
            string[] splString = Link.Split(' ');
            if (splString.Length == 1)
            {
                WriteLine("Ange Namn Eller Nummer för länken du vill öppna.");
                string rl = ReadLine();
                int index;
                if(int.TryParse(rl, out index))
                {
                    // Om Int hämta rad med index
                    string[] a = weblinks.ElementAt(index-1).Split(',');
                    BrowserProces(a[2].Trim());
                }
                else
                {
                    // Om sträng hitta index med contains
                    index = weblinks.FindIndex(a => a.Contains(rl));
                    string[] a = weblinks.ElementAt(index).Split(',');
                    BrowserProces(a[2].Trim());
                }
            }
            else if (splString.Length == 2)
            {
                WriteLine("Ange Namn Eller Nummer för länken du vill öppna.");
                string rl = splString[1];
                int index;
                if (int.TryParse(rl, out index))
                {
                    // Om Int hämta rad med index
                    string[] a = weblinks.ElementAt(index - 1).Split(',');
                    BrowserProces(a[2].Trim());
                }
                else
                {
                    // Om sträng hitta index med contains
                    index = weblinks.FindIndex(a => a.Contains(rl));
                    string[] a = weblinks.ElementAt(index).Split(',');
                    BrowserProces(a[2].Trim());
                }
            }
        }
        /// <summary>
        /// Öppnar default webbläsaren med vald länk
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
        /// <summary>
        /// Lägger till en rad till weblinks
        /// </summary>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <param name="info"></param>
        public void AddLink(string name, string url, string info)
        //Add a weblink to the array from console.
        {
            weblinks.Add($"{name},{info},{url}");
        }
        public void SaveWebLinks(string path, string command)
        //Save the current weblinks array to file
        {
            string file;
            if (command.Split(" ").Length != 1) file = command.Split(" ")[1];
            else { Console.Write("Add filename: "); file = Console.ReadLine(); }
            file = path + "\\" + file;
            if (File.Exists(file))
            {
                string input;
                do
                {
                    Console.Write("File already exists. Overwrite (y/n)?: ");
                    input = Console.ReadLine();
                } while (input != "y" && input != "n");
                if (input == "n") return;
            }
            File.Create(file).Dispose();
            StreamWriter writer = new StreamWriter(file);

            for (int i = 0; i < weblinks.Count; i++)
            {
                writer.WriteLine(weblinks[i]);
                //WriteLine("Writing: " + weblinks[i]);
            }
            writer.Close();
            WriteLine("File successfully saved");
            //Debug: Open the file:
            //Process.Start("explorer", path);

        }
        public void AddLinksZenity()
        {
            Process proc = new Process();
            //Start zenity form:
            proc.StartInfo.FileName = "zenity.exe";
            proc.StartInfo.Arguments = "--forms --add-entry=\"Name\" --add-entry=\"Info\" --add-entry=\"URL\"";
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            StreamReader reader = proc.StandardOutput;
            string input = reader.ReadToEnd().Replace('|',',');
            proc.WaitForExit();
            weblinks.Add(input);
        }
        public void PresentWorkingDirectory(string path)
        {
            Console.WriteLine(path);
        }
        public string ChangeDirectory(string path, string folder)
        {
            string newPath = "";
            if (folder == "..")
            {
                string[] splitPath = path.Split('\\');
                for (int i = 0; i < splitPath.Length - 1; i++) 
                {
                    newPath = Path.Combine(newPath, splitPath[i]);
                }
                return newPath;
            }
            newPath = Path.Combine(path, folder);
            if (Directory.Exists(newPath) && folder != ".") return newPath;
            WriteLine("Folder does not exist");
            return path;
        }
        public void CreateDirectory(string path, string folder)
        {
            if (folder.Split(' ').Length < 2) { WriteLine("Syntax error. Try: 'mkdir <new foldername>'"); return; }

            string name = folder.Split(' ')[1];
            string fullPath = Path.Combine(path, name);
            if (Directory.Exists(fullPath)) { WriteLine("Folder already exists."); return; }
            Directory.CreateDirectory(fullPath);
            return;
        }
    }
}