﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SQLitePCL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CodingChemistry
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using PeriodicTableDataBase context = new PeriodicTableDataBase();
            //context.Elements.ExecuteDelete();
            //SeedTheDataBase();
            {
                // Get user input on which element they want to see.
                Console.WriteLine("Welcome to Mr. Estephan's 'Coding Chemistry' Program!\n\nPlease enter the symbol of the element you would like to explore!");
                string userInput = Console.ReadLine() ?? "";

                //Feature Requirement #1 Query your database using araw SQL query, not EF

                // Get the element from the database. 
                Element? element = context.Elements.FromSqlRaw($"SELECT * FROM Elements WHERE Symbol = '{userInput}'").SingleOrDefault();
                
                //Print the information below, if the user inputs a null value. 
                while (element == null)
                {
                    Console.WriteLine($"\nUhoh! {userInput} is not an element.  Double check your spelling, capitalization and try again!");
                   
                    Console.WriteLine("\nPlease enter the symbol of the element you would like to explore!");
                    userInput = Console.ReadLine() ?? "";
                    element = context.Elements.FromSqlRaw($"SELECT * FROM Elements WHERE Symbol = '{userInput}'").SingleOrDefault();
                 
                }
                // Print information regarding the element to the console.
                if (element != null)
                {
                    Console.WriteLine($"\nThe element you selected is {element?.Name}. {element?.Name} has an atomic number of {element?.AtomicNumber}.  An atomic number of {element?.AtomicNumber} means it contains {element?.AtomicNumber} protons and electrons.  Protons are subatomic particles that are located in the nucleus of the atom and contain a positive(+) charge. Electrons are subatomic particles that orbit outside the nucleus and contain a negative(-) charge.  The number of protons in an element is what makes an element unique.  Only {element?.Name} contains exactly {element.AtomicNumber} protons.");
                    Console.WriteLine($"\n\n\nPress 1 to learn about the Atomic Mass of {element?.Name}.");
                    Console.WriteLine($"Press 2 to learn about {element?.Name}'s groupname and its location on the Periodic Table.");
                    Console.WriteLine($"Press 3 to learn about the electron configuration of {element?.Name}.");
                    Console.WriteLine("Press 4 to explore another element.");
                    Console.WriteLine("\n Type 'exit' to quit.");
                    userInput = Console.ReadLine() ?? "";     
                }
               
                while (userInput.ToLower() != "exit")
                {
                    if (userInput == "1")
                    {
                        PrintUserInputOption1(element);
                        
                    }
                    else if (userInput == "2")
                    {
                        Console.WriteLine($"\n\nThe location of {element.Name} can be found in Group {element.Group} and Period {element.Period} on the Periodic Table. An element's group number is the numbered column its in on the Periodic Table, while the Period is the numbered row on the Periodic Table. Also, {element.Name} is considered a {element.GroupName}.");
                        Console.WriteLine($"\nPress 1 to learn about the Atomic mass of {element?.Name}.");
                        Console.WriteLine($"Press 2 to learn about {element?.Name}'s groupname and its location on the Periodic Table.");
                        Console.WriteLine($"Press 3 to learn about the electron configuration of {element?.Name}");
                        Console.WriteLine("Press 4 to explore another element.");
                        Console.WriteLine("Type 'exit' to quit.");
                    }
                    else if (userInput == "3")
                    {
                        var elementString = element.AbbreviatedElectronConfiguration;
                        var regexMatch = Regex.Match(elementString, @"\[(.*)\].*");
                        if (regexMatch.Groups.Count == 2)
                        {
                            Console.WriteLine($"Element symbol is {regexMatch.Groups[1].Value}");
                        }
                        else
                        {
                            Console.WriteLine("Element symbol not found");
                        }
                        Console.Write($"\n\nThe electron configuration for {element?.Name} is {element.ElectronConfiguration} and the abbreviated electron configuration for {element?.Name} is {element?.AbbreviatedElectronConfiguration}. Notice the element in brackets [] is {regexMatch.Groups[1].Value}. That is the noble gas closest to {element.Name}.");
                        Console.WriteLine($"\n\nPress 1 to learn about the Atomic mass of {element?.Name}.");
                        Console.WriteLine($"Press 2 to learn about {element?.Name}'s groupname and its location on the Periodic Table.");
                        Console.WriteLine($"Press 3 to learn about the electron configuration of {element?.Name}.");
                        Console.WriteLine("Press 4 to explore another element.");
                        Console.WriteLine("Type 'exit' to quit.");
                    }
                    else if (userInput == "4")
                    {
                        Console.WriteLine("\n\nPlease enter the symbol of the element you would like to explore!");
                        userInput = Console.ReadLine() ?? "";
                        element = context.Elements.FromSqlRaw($"SELECT * FROM Elements WHERE Symbol = '{userInput}'").SingleOrDefault();
                        Console.WriteLine($"\n\nNow lets learn about the element {element.Name}! {element?.Name} has an atomic number of {element?.AtomicNumber}.  An atomic number of {element?.AtomicNumber} means it contains {element?.AtomicNumber} protons and electrons.  Protons are subatomic particles that are located in the nucleus of the atom and contain a positive(+) charge. Electrons are subatomic particles that orbit outside the nucleus and contain a negative(-) charge.  The number of protons in an element is what makes an element unique.  Only {element?.Name} contains exactly {element.AtomicNumber} protons.\");\r\n");
                        Console.WriteLine($"Press 1 to learn about the Atomic Mass of {element?.Name}.");
                        Console.WriteLine($"Press 2 to learn about {element?.Name}'s groupname and its location on the Periodic Table.");
                        Console.WriteLine($"Press 3 to learn about the electron configuration of {element?.Name}.");
                        Console.WriteLine("Press 4 to explore another element.");
                        Console.WriteLine("Type 'exit' to quit.");

                    }
                    userInput = Console.ReadLine();
                }
                if (userInput.ToLower() == "exit") ;
                {
                    Console.WriteLine("Thanks for learning Chemistry with Mr. Estephan today!  Keep it classy!");
                }
            }
        }
        private static void SeedTheDataBase()  //Feature Requirement #2 Create a dictionary or list,populate it with severalvalues, retrieve at least onevalue, and use it in yourprogram
        {
            //create the destination/recipient list of elements.
            List<Element> listOfElements = new List<Element>();
            //create the database context. 
            using PeriodicTableDataBase context = new PeriodicTableDataBase();
            //convert JSON into list above.
            string json = File.ReadAllText(@"c:\users\estep\OneDrive\desktop\PeriodicTableJSON.json");
            listOfElements = JsonConvert.DeserializeObject<List<Element>>(json);
            //add the list to the context (database in ef core).
            context.AddRange(listOfElements);
            //save changes to the context. 
            context.SaveChanges();
        }
        private static void PrintUserInputOption1(Element element)
        {
            Console.WriteLine($"\n\nThe atomic mass for {element.Name} is {element.AtomicMass}amu.  The unit 'amu' stands for atomic mass units.  The atomic mass is the mass of all the protons and neutrons in that element.  Since the number of protons in {element.Name} is {element.AtomicNumber}, the number of neutrons is {element.CalculateNeutrons(element.AtomicMass, element.AtomicNumber)}.");
            Console.WriteLine($"\nPress 1 to learn about the Atomic Mass of {element?.Name}.");
            Console.WriteLine($"Press 2 to learn about {element?.Name}'s groupname and its location on the Periodic Table.");
            Console.WriteLine($"Press 3 to learn about the electron configuration of {element?.Name}");
            Console.WriteLine("Press 4 to explore another element.");
            Console.WriteLine("Type 'exit' to quit.");
        }
    }


    public class PeriodicTableDataBase : DbContext
    {
        public DbSet<Element> Elements { get; set; }
        public string DbPath { get; set; }
        public PeriodicTableDataBase()
        {
            var folder = Environment.SpecialFolder.Desktop;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "PeriodicTableDataBase.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }


}