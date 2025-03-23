using CatTinder.Models;
using CatTinder.Repositories.Interfaces;
using CatTinder.Services.Interfaces;

namespace CatTinder.Services
{
    public class CatService : ICatService
    {
        private readonly ICatRepository _catRepository;

        public CatService(ICatRepository catRepository)
        {
            _catRepository = catRepository;
        }

        public async Task<IEnumerable<Cat>> GetAllCatsAsync()
        {
            return await _catRepository.GetAllCatsAsync();
        }

        public async Task<Cat?> GetCatByIdAsync(int id)
        {
            return await _catRepository.GetCatByIdAsync(id);
        }

        public async Task AddCatAsync(Cat cat)
        {
             await _catRepository.AddCatAsync(cat);
        }

        public async Task UpdateCatAsync(Cat cat)
        {
            await _catRepository.UpdateCatAsync(cat);
        }

        public async Task DeleteCatAsync(int id)
        {
            await _catRepository.DeleteCatAsync(id);
        }
    }
}
