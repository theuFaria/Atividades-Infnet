using CityBreaks.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Web.Pages.Cities;

public class CityDetailsModel : PageModel
{
    private readonly ICityService _cityService;

    public CityDetailsModel(ICityService cityService)
    {
        _cityService = cityService;
    }

    public City? City { get; set; }
    public bool IsFound { get; set; } = true;

    public async Task<IActionResult> OnGetAsync(string? name)
    {
        IsFound = true;
        if (string.IsNullOrWhiteSpace(name))
        {
            return Page();
        }

        City = await _cityService.GetByNameAsync(name);

        if (City == null)
        {  
            IsFound = false;
        }
        
        return Page();
    }
}