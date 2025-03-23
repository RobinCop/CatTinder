using CatTinder.Models;
using CatTinder.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatTinder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatsController : ControllerBase
    {
        private readonly ICatService _catService;
        private readonly ILogger<CatsController> _logger;

        public CatsController(ICatService catService, ILogger<CatsController> logger)
        {
            _catService = catService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCats()
        {
            _logger.LogInformation("GetAllCats endpoint was called.");

            try
            {
                var cats = await _catService.GetAllCatsAsync();
                _logger.LogInformation("Retrieved {CatCount} cats from service.", cats.Count());
                return Ok(cats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving cats.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatById(int id)
        {
            _logger.LogInformation("GetCatById endpoint was called with ID: {CatId}", id);

            try
            {
                var cat = await _catService.GetCatByIdAsync(id);
                if (cat == null)
                {
                    _logger.LogWarning("Cat with ID: {CatId} not found.", id);
                    return NotFound();
                }
                return Ok(cat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the cat with ID: {CatId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCat([FromBody] Cat cat)
        {
            _logger.LogInformation("AddCat endpoint was called");

            if (cat == null)
                return BadRequest("Cat parameter is null");

            try
            {
                await _catService.AddCatAsync(cat);
                return CreatedAtAction(nameof(GetCatById), new { id = cat.Id }, cat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while adding a cat");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCat(int id, [FromBody] Cat cat)
        {
            _logger.LogInformation("UpdateCat endpoint was called with ID: {id}", id);

            if (cat == null)
                return BadRequest("Cat parameter is null");

            try
            {
                if (id != cat.Id) return BadRequest("Cat id does not match url id");
                await _catService.UpdateCatAsync(cat);
                return Ok(cat);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occured while updating a cat with ID: {id}",id);
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCat(int id)
        {
            _logger.LogInformation("DeleteCat endpoint was called with ID: {ID}", id);
            try
            {
                await _catService.DeleteCatAsync(id);
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occured while deleting a cat with ID: {ID}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
