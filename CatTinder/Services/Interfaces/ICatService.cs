using CatTinder.Models;

namespace CatTinder.Services.Interfaces
{
    public interface ICatService
    {
        Task<IEnumerable<Cat>> GetAllCatsAsync();
        Task<Cat?> GetCatByIdAsync(int id);
        Task AddCatAsync(Cat cat);
        Task UpdateCatAsync(Cat cat);
        Task DeleteCatAsync(int id);
    }
}
