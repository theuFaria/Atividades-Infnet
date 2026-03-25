using CityBreaks.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace CityBreaks.Web.Models;

public class CityService : ICityService
{
    private readonly CityBreaksContext _context;

    public CityService(CityBreaksContext context)
    {
        _context = context;
    }

    public async Task<List<City>> GetAllAsync()
    {
        return await _context.Cities
            .Include(c => c.Country)
            .Include(c => c.Properties.Where(p => p.DeletedAt == null))
            .ToListAsync();
    }

    public async Task<City> GetByIdAsync(int id)
    {
        return await _context.Cities
                   .Include(c => c.Country)
                   .Include(c => c.Properties.Where(p => p.DeletedAt == null))
                   .FirstOrDefaultAsync(c => c.Id == id) ??
               throw new InvalidOperationException($"City with ID {id} not found");

        //OBS: propriedades com DeleteAt preenchidos foram "Deletadas" e por isso não devem ser buscadas no banco.
    }

    public async Task<City> GetByNameAsync(string name)
    {
        return await _context.Cities.AsNoTracking()
            .Include(c => c.Country)
            .Include(c => c.Properties.Where(p => p.DeletedAt == null))
            .FirstOrDefaultAsync(c =>
                EF.Functions.Collate(c.Name, "NOCASE") == name);
    }

    public async Task<List<Property>> GetFilteredAsync(decimal? minPrice, decimal? maxPrice, string cityName,
        string propertyName)
    {
        var query = _context.Properety
            .Include(p => p.City)
            .Where(p => p.DeletedAt == null).AsQueryable();

        if (minPrice.HasValue) query = query.Where(p => p.PricePerNight >= minPrice.Value);
        if (maxPrice.HasValue) query = query.Where(p => p.PricePerNight <= maxPrice.Value);
        if (!string.IsNullOrEmpty(propertyName)) query = query.Where(p => p.Name.Contains(propertyName));
        if (!string.IsNullOrEmpty(cityName)) query = query.Where(p => p.City.Name.Contains(cityName));

        return await query.ToListAsync();
    }

    public async Task AddProperty(Property property)
    {
        await _context.Properety.AddAsync(property);
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

    public Task DeleteAsync(int id)
    {
        Property p = _context.Properety.FirstOrDefault(p => p.Id == id);
        p.DeletedAt = DateTime.Now; //Deleta
        return _context.SaveChangesAsync();
    }
}