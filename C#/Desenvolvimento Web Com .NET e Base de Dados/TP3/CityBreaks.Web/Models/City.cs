using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CityBreaks.Web.Models;

public class City
{
    public int Id { get; set; }
    public String Name { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
    public List<Property> Properties { get; set; }
}