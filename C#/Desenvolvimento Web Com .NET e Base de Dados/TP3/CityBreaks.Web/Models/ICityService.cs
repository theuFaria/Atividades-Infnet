namespace CityBreaks.Web.Models;

public interface ICityService
{
    public Task<List<City>> GetAllAsync();

    public Task<City> GetByIdAsync(int id);
    public Task<City> GetByNameAsync(string name);
    public Task<List<Property>> GetFilteredAsync(decimal? minPrice, decimal? maxPrice, string cityName, string propertyName);
    public Task AddProperty(Property property);
    public Task SaveChanges();
    public Task DeleteAsync(int id);
    
}