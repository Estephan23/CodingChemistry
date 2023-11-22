using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Xml.Linq;

namespace CodingChemistry
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using PeriodicTableDataBase context = new PeriodicTableDataBase();
            context.Elements.ExecuteDelete();
            SeedTheDataBase();
            {
                // Get user input on which element they want to see.
                Console.WriteLine("Hello, welcome to Mr. Estephan's 'Coding Chemistry' Program!\n\nPlease enter the symbol of the element you would like to explore!");
                string userInput = Console.ReadLine() ?? "";

                // Get the element from the database.
                Element? Element = context.Elements.FromSqlRaw($"SELECT * FROM Elements WHERE Symbol = '{userInput}'").SingleOrDefault();
                Element E = Element;

                while (E == null)
                {
                    Console.WriteLine("\nUhoh! " + "'" + userInput + "'" + " is not an element.  Double check your spelling, capitalization and try again!");
                   
                    Console.WriteLine("\nPlease enter the symbol of the element you would like to explore!");
                    userInput = Console.ReadLine() ?? "";
                    Element = context.Elements.FromSqlRaw($"SELECT * FROM Elements WHERE Symbol = '{userInput}'").SingleOrDefault();
                    E = Element;
                }
                // Print information regarding the element to the console.
                if (E != null)
                {
                    Console.WriteLine("\nThe element you selected is " + E?.Name + ". " + E?.Name + " has an atomic number of " + E?.AtomicNumber + ".  An atomic number of " + E?.AtomicNumber + " means it contains " + E?.AtomicNumber + " protons.  Protons are subatomic particles that are located in the nucleus of  atom and contain a positive(+) charge.  The number of protons in an element is what makes an element unique.  Only " + E?.Name + " contains exactly " + E.AtomicNumber + " protons.");
                    Console.WriteLine("\n\n\nPress 1 to learn about the Atomic Mass of " + E?.Name + " .");
                    Console.WriteLine("Press 2 to learn about the location of " + E?.Name + " and the type of element it is.");
                    Console.WriteLine("Press 3 to learn about the electron configuration of " + E?.Name);
                    userInput = Console.ReadLine() ?? "";
                    //Console.WriteLine("\nPress 'exit' to quit.");
                }
           
                while (userInput.ToLower() != "exit")
                {
                    if (userInput == "1")
                    {
                        
                        Console.WriteLine("The atomic mass for " + E.Name + " is " + E.AtomicMass);
                        Console.WriteLine("The atomic mass of an element is in amu, atomic mass units.  The atomic mass consists of the mass of all the protons and neutrons in that element.");
                        Console.WriteLine("The atomic mass tells us the number of protons and neutrons in an element. In this case, since " + E.Name + " has " + E.AtomicNumber + " protons, that means if you subtract the " + E.AtomicNumber + " from, the " + E.AtomicMass + " you will get the number of neturons, after you round to the nearest whole number.");
                    }
                    else if (userInput == "2")
                    {
                        Console.WriteLine("Press 1 to learn about the Atomic mass of " + E?.Name + " .");
                        Console.WriteLine("Press 2 to learn about the location of " + E?.Name + " and the type of element it is.");
                        Console.WriteLine("Press 3 to learn about the electron configuration of " + E?.Name);
                    }
                    else if (userInput == "3")
                    {
                        Console.WriteLine("Press 1 to learn about the Atomic mass of " + E?.Name + " .");
                        Console.WriteLine("Press 2 to learn about the location of " + E?.Name + " and the type of element it is.");
                        Console.WriteLine("Press 3 to learn about the electron configuration of " + E?.Name);
                    }
                    
                    userInput = Console.ReadLine();
                }
                Console.WriteLine("\nPress 'exit' to quit.");

            }
        }
        private static void SeedTheDataBase()
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