using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace CodingChemistry
{
    internal class Program
    { 
        static void Main(string[] args)
        {
            using PeriodicTableDataBase context = new PeriodicTableDataBase();
            context.Elements.ExecuteDelete(); 
            SeedTheDataBase();
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