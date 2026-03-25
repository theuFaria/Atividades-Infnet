using CityBreaks.Web.Data;
using CityBreaks.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Web.Pages.Cities;

public class EditProperty : PageModel
{
    private ICityService _service;

    public EditProperty(ICityService service)
    {
        _service = service;
    }

    [BindProperty] public City City { get; set; }

    public async Task OnGet(int id)
    {
        City = await _service.GetByIdAsync(id);
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        //Pego novamente as propriedades
        var cityToUpdate = await _service.GetByIdAsync(id);

        foreach (var p in cityToUpdate.Properties)
        {
            var prefix = $"City.Properties[{cityToUpdate.Properties.IndexOf(p)}]";

            await TryUpdateModelAsync(
                p,
                prefix,
                p => p.Name,
                p => p.PricePerNight
            );
        }

        await _service.SaveChanges();

        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToPage("Index");
    }
}