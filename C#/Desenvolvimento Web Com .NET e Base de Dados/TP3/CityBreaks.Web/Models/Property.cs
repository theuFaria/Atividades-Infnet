using System.ComponentModel.DataAnnotations;

namespace CityBreaks.Web.Models;

public class Property
{
    public Property()
    {
    }

    public Property(string name, decimal pricePerNight, int cityId, City city)
    {
        Name = name;
        PricePerNight = pricePerNight;
        CityId = cityId;
        City = city;
    }

    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter a name.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Must be between 3 and 50 characters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Please enter a value.")]
    public decimal PricePerNight { get; set; }

    public int CityId { get; set; }
    public City City { get; set; }
    
    public DateTime? DeletedAt { get; set; }
}