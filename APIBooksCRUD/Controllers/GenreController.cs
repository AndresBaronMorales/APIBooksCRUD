using APIBooksCRUD.DTOs;
using APIBooksCRUD.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace APIBooksCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private ICommonService<GenreDto, GenreInsertDto, GenreUpdateDto> _genreService;
        private IValidator<GenreInsertDto> _genreInsertValidator;
        private IValidator<GenreUpdateDto> _genreUpdateValidator;

        public GenreController([FromKeyedServices("genreService")] ICommonService<GenreDto, GenreInsertDto, GenreUpdateDto> genreService, IValidator<GenreInsertDto> genreInsertValidator, IValidator<GenreUpdateDto> genreUpdateValidator)
        {
            _genreService = genreService;
            _genreInsertValidator = genreInsertValidator;
            _genreUpdateValidator = genreUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<GenreDto>> GetAll() => await _genreService.GetAll();

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> GetById(int id)
        {
            var genreDto = await _genreService.GetById(id);
            return genreDto == null ? NoContent() : Ok(genreDto);
        }

        [HttpPost]
        public async Task<ActionResult<GenreDto>> Add(GenreInsertDto genreInsertDto)
        {
            var validation = await _genreInsertValidator.ValidateAsync(genreInsertDto);
            if (!validation.IsValid) return BadRequest(validation.Errors);

            if (!_genreService.Validate(genreInsertDto)) return BadRequest(_genreService.Errors);

            var genreDto = await _genreService.Add(genreInsertDto);
            return CreatedAtAction(nameof(GetById), new { id = genreDto.GenreId }, genreDto);
        }

        [HttpPut]
        public async Task<ActionResult<GenreDto>> Update(GenreUpdateDto genreUpdate)
        {
            var validation = await _genreUpdateValidator.ValidateAsync(genreUpdate);
            if (!validation.IsValid) return BadRequest(validation.Errors);

            if (!_genreService.Validate(genreUpdate)) return BadRequest(_genreService.Errors);

            var genreDto = await _genreService.Update(genreUpdate);
            return genreDto == null ? NotFound() : Ok(genreDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GenreDto>> Delete(int id)
        {
            var genreDto = await _genreService.Delete(id);
            return genreDto == null ? NotFound() : Ok(genreDto);
        }
    }
}
