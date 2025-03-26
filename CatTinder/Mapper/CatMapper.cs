using CatTinder.Models.Dto;
using CatTinder.Models;

namespace CatTinder.Mapper
{
    public static class CatMapper
    {
        public static CatDto ToDto(Cat cat)
        {
            return new CatDto
            {
                Id = cat.Id,
                Name = cat.Name,
                Description = cat.Description,
                ImageBase64 = cat.Image != null ? Convert.ToBase64String(cat.Image) : null
            };
        }
    }
}
