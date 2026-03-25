using CityBreaks.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Property = CityBreaks.Web.Models.Property;

namespace CityBreaks.Web.Pages.Cities;

public class CreateProperty : PageModel
{
    private readonly ICityService _service;

    public CreateProperty(ICityService service)
    {
        _service = service;
    }

    [BindProperty] public Property Property { get; set; }

    public List<City> Cities { get; set; }

    public async Task OnGet()
    {
        Cities = await _service.GetAllAsync();
    }

    public async Task<IActionResult> OnPost()
    {
        Cities = await _service.GetAllAsync();
        
        Property.City = Cities.FirstOrDefault(c => c.Id == Property.CityId);

        _service.AddProperty(Property);
        _service.SaveChanges();

        return RedirectToPage("Index");
    }
}