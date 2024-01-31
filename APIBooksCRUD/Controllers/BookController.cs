using APIBooksCRUD.DTOs;
using APIBooksCRUD.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace APIBooksCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private ICommonService<BookDto, BookInsertDto, BookUpdateDto> _bookService;
        private IValidator<BookInsertDto> _bookInsertValidator;
        private IValidator<BookUpdateDto> _bookUpdateValidator;

        public BookController([FromKeyedServices("bookService")] ICommonService<BookDto, BookInsertDto, BookUpdateDto> bookService, IValidator<BookInsertDto> bookInsertValidator, IValidator<BookUpdateDto> bookUpdateValidator)
        {
            _bookService = bookService;
            _bookInsertValidator = bookInsertValidator;
            _bookUpdateValidator = bookUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<BookDto>> GetAll() => await _bookService.GetAll();

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetById(int id)
        {
            var bookDto = await _bookService.GetById(id);
            return bookDto == null ? NotFound() : Ok(bookDto);
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> Add(BookInsertDto bookInsertDto)
        {
            var validation = await _bookInsertValidator.ValidateAsync(bookInsertDto);
            if (!validation.IsValid) return BadRequest(validation.Errors);

            if (!_bookService.Validate(bookInsertDto)) return BadRequest(_bookService.Errors);

            var bookDto = await _bookService.Add(bookInsertDto);
            return bookDto == null ? NotFound() : Ok(bookDto);
        }

        [HttpPut]
        public async Task<ActionResult<BookDto>> Update(BookUpdateDto bookUpdateDto)
        {
            var validation = await _bookUpdateValidator.ValidateAsync(bookUpdateDto);
            if (!validation.IsValid) return BadRequest(validation.Errors);

            if (!_bookService.Validate(bookUpdateDto)) return BadRequest(_bookService.Errors);

            var bookDto = await _bookService.Update(bookUpdateDto);
            return bookDto == null ? NotFound() : Ok(bookDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BookDto>> Delete(int id)
        {
            var bookDto = await _bookService.Delete(id);
            return bookDto == null ? NotFound() : Ok(bookDto);
        }
    }
}
