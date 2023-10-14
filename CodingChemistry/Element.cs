
using System.ComponentModel.DataAnnotations;

public class Element
{
    public string Name { get; set; }
    public float AtomicMass { get; set; }
    public string GroupName { get; set; }
    [Key]
    public int AtomicNumber { get; set; }
    public int Period { get; set; }
    public int Group { get; set; }
    public string Symbol { get; set; }
    public string ElectronConfiguration { get; set; }
    public string AbbreviatedElectronConfiguration { get; set; }
    public float? Electronegativity { get; set; }
   
}

