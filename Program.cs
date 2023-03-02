﻿using System.Diagnostics;
using System.IO;
using System.Net.Sockets;

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
            string filename = "list.lis";
            PrintWelcome();
            
            string command;
            do
            {
                Console.Write(": ");
                command = Console.ReadLine();
                if (command == "quit")
                {
                    Console.WriteLine("Good bye!");
                }
                else if (command == "help")
                {
                    WriteTheHelp();
                }
                else if (command == "load")
                {
                    NotYetImplemented("load");
                    string loadPath;
                    //ImportLinksFromFile(loadPath); //Isak
                }
                else if (command == "open")
                {
                    NotYetImplemented("open");
                    string openName;
                    //OpenWeblink(openName); //Alex
                }
                else if (command == "list")
                {
                    NotYetImplemented("list");
                    //ListLinks(); //Sebastian
                }
                else if (command == "add")
                {
                    NotYetImplemented("add");
                    string addName, addUrl, addInfo;
                    //AddLink(addName, addUrl, addInfo); //Sebastian
                }
                else if (command == "save")
                {
                    NotYetImplemented("save");
                    string saveFile;
                    //SaveWebLinks(saveFile); //Isak
                }
                else
                {
                    Console.WriteLine($"Unknown command '{command}'");
                }
            } while (command != "quit");
        }

        private static void NotYetImplemented(string command)
        {
            Console.WriteLine($"Sorry: '{command}' is not yet implemented");
        }

        private static void PrintWelcome()
        {
            Console.WriteLine("Hello and welcome to the ... program ...");
            Console.WriteLine("that does ... something.");
            Console.WriteLine("Write 'help' for help!");
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
            //code...
        }
        public static void ListLinks()
        //Lists all weblinks currently loaded into weblinks array.
        {
            //code...
        }
        public static void OpenWeblink(string Link)
        //Opens a link from the weblinks array in native browser
        {
            //code...
        }
        public static void AddLink(string name, string url, string info)
        //Add a weblink to the array from console.
        {
            //code...
        }
        public static void SaveWebLinks(string path)
        //Save the current weblinks array to file
        {
            //code...
        }
    }
}