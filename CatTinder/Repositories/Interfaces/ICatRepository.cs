using CatTinder.Models;

namespace CatTinder.Repositories.Interfaces
{
    public interface ICatRepository
    {
        Task<IEnumerable<Cat>> GetAllCatsAsync();
        Task<Cat?> GetCatByIdAsync(int id);
        Task AddCatAsync(Cat cat);
        Task UpdateCatAsync(Cat cat);
        Task DeleteCatAsync(int id);
    }
}
