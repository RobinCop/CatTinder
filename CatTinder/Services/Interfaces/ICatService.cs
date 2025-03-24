using CatTinder.Models;
using CatTinder.Models.Dto;

namespace CatTinder.Services.Interfaces
{
    public interface ICatService
    {
        Task<IEnumerable<CatDto>> GetAllCatsAsync();
        Task<CatDto?> GetCatByIdAsync(int id);
        Task AddCatAsync(Cat cat);
        Task UpdateCatAsync(Cat cat);
        Task DeleteCatAsync(int id);
    }
}
