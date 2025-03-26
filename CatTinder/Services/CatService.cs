using CatTinder.Mapper;
using CatTinder.Models;
using CatTinder.Models.Dto;
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

        public async Task<IEnumerable<CatDto>> GetAllCatsAsync()
        {
            var cats = await _catRepository.GetAllCatsAsync();

            return cats.Select(CatMapper.ToDto);
        }

        public async Task<CatDto?> GetCatByIdAsync(int id)
        {
            var cat = await _catRepository.GetCatByIdAsync(id);
            if (cat == null) return null;

            return CatMapper.ToDto(cat);
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
