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
                Console.WriteLine("Welcome to Mr. Estephan's 'Coding Chemistry' Program!\n\nPlease enter the symbol of the element you would like to explore!");
                string userInput = Console.ReadLine() ?? "";

                // Get the element from the database.
                Element? Element = context.Elements.FromSqlRaw($"SELECT * FROM Elements WHERE Symbol = '{userInput}'").SingleOrDefault();
                
                //Print the information below, if the user inputs a null value. 
                while (Element == null)
                {
                    Console.WriteLine($"\nUhoh! {userInput} is not an element.  Double check your spelling, capitalization and try again!");
                   
                    Console.WriteLine("\nPlease enter the symbol of the element you would like to explore!");
                    userInput = Console.ReadLine() ?? "";
                    Element = context.Elements.FromSqlRaw($"SELECT * FROM Elements WHERE Symbol = '{userInput}'").SingleOrDefault();
                 
                }
                // Print information regarding the element to the console.
                if (Element != null)
                {
                    Console.WriteLine($"\nThe element you selected is {Element?.Name}. {Element?.Name} has an atomic number of {Element?.AtomicNumber}.  An atomic number of {Element?.AtomicNumber} means it contains {Element?.AtomicNumber} protons and electrons.  Protons are subatomic particles that are located in the nucleus of the atom and contain a positive(+) charge. Electrons are subatomic particles that orbit outside the nucleus and contain a negative(-) charge.  The number of protons in an element is what makes an element unique.  Only {Element?.Name} contains exactly {Element.AtomicNumber} protons.");
                    Console.WriteLine($"\n\n\nPress 1 to learn about the Atomic Mass of {Element?.Name}.");
                    Console.WriteLine($"Press 2 to learn about {Element?.Name}'s groupname and its location on the Periodic Table.");
                    Console.WriteLine($"Press 3 to learn about the electron configuration of {Element?.Name}.");
                    Console.WriteLine("\n Type 'exit' to quit.");
                    userInput = Console.ReadLine() ?? "";     
                }
               
                while (userInput.ToLower() != "exit")
                {
                    if (userInput == "1")
                    {
                        Console.WriteLine($"\n\nThe atomic mass for {Element.Name} is {Element.AtomicMass}amu.  The unit 'amu' stands for atomic mass units.  The atomic mass is the mass of all the protons and neutrons in that element.  Since the number of protons in {Element.Name} is {Element.AtomicNumber}, the number of neutrons is {Element.Neutrons}.");
                        Console.WriteLine($"\n\nPress 2 to learn about {Element?.Name}'s groupname and its location on the Periodic Table.");
                        Console.WriteLine($"Press 3 to learn about the electron configuration of {Element?.Name}");
                        //Console.WriteLine("Press 4 to explore another element.");
                        Console.WriteLine("Type 'exit' to quit.");
                    }
                    else if (userInput == "2")
                    {
                        Console.WriteLine($"\n\nThe location of {Element.Name} can be found in Group {Element.Group} and Period {Element.Period} on the Periodic Table. An element's group number is the numbered column its in on the Periodic Table, while the Period is the numbered row on the Periodic Table. Also, {Element.Name} is considered a {Element.GroupName}.");
                        Console.WriteLine($"\n\nPress 1 to learn about the Atomic mass of {Element?.Name}.");
                        Console.WriteLine($"Press 3 to learn about the electron configuration of {Element?.Name}");
                        //Console.WriteLine("Press 4 to explore another element.");
                        Console.WriteLine("Type 'exit' to quit.");
                    }
                    else if (userInput == "3")
                    {
                        Console.Write($"\n\nThe electron configuration for {Element?.Name} is {Element.ElectronConfiguration} and the abbreviated electron configuration for {Element?.Name} is {Element?.AbbreviatedElectronConfiguration}. Notice the element in brackets []. That is the noble gas closest to {Element.Name}.");
                        Console.WriteLine($"\n\n\nPress 1 to learn about the Atomic mass of {Element?.Name}.");
                        Console.WriteLine($"Press 2 to learn about {Element?.Name}'s groupname and its location on the Periodic Table.");
                        //Console.WriteLine("Press 4 to explore another element.");
                        Console.WriteLine("Type 'exit' to quit.");
                    }
                    //else if (userInput == "4")
                    //{
                    //    Console.WriteLine("\n\nPlease enter the symbol of the element you would like to explore!");
                    //    userInput = Console.ReadLine() ?? "";
                    //    Element = context.Elements.FromSqlRaw($"SELECT * FROM Elements WHERE Symbol = '{userInput}'").SingleOrDefault();
                    //}
                    userInput = Console.ReadLine();
                }
                if (userInput.ToLower() == "exit") ;
                {
                    Console.WriteLine("Thanks for learning Chemistry with Mr. Estephan today!  Keep it classy!");
                }
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