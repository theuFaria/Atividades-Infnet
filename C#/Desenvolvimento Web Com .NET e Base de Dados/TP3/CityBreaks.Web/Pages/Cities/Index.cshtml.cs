using CityBreaks.Web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Web.Pages.Cities
{
    public class IndexModel : PageModel
    {
        private readonly ICityService _cityService;

        public IndexModel(ICityService cityService)
        {
            _cityService = cityService;
        }

        public List<City> Cities { get; set; }

        public async Task OnGetAsync()
        {
            Cities = await _cityService.GetAllAsync();
        }
    }
}