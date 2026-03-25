using CityBreaks.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Web.Pages.Cities;

public class FilterProperties : PageModel
{
    private readonly ICityService _cityService;

    public FilterProperties(ICityService cityService)
    {
        _cityService = cityService;
    }
    
    public class InputModel
    {
        public Decimal? MaxPrice { get; set; }
        public Decimal? MinPrice { get; set; }
        public string CityName { get; set; }
        public string PropertyName { get; set; }
    }
    
    [BindProperty]
    public InputModel Input { get; set; }
    public List<Property> Properties { get; set; }
    
    public void OnGet()
    {
       
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Properties = await _cityService.GetFilteredAsync(Input.MinPrice, Input.MaxPrice,  Input.CityName, Input.PropertyName);
        
        return Page();
    }
}