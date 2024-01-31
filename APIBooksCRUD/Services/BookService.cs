using APIBooksCRUD.DTOs;
using APIBooksCRUD.Models;
using APIBooksCRUD.Repository;
using AutoMapper;

namespace APIBooksCRUD.Services
{
    public class BookService : ICommonService<BookDto, BookInsertDto, BookUpdateDto>
    {
        private IRepository<Book> _repositoryBook;
        private IMapper _mapper;

        public List<string> Errors { get; }

        public BookService(IRepository<Book> repositoryBook, IMapper mapper)
        {
            _repositoryBook = repositoryBook;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<BookDto>> GetAll()
        {
            var books = await _repositoryBook.GetAll();
            return books.Select(book => _mapper.Map<BookDto>(book));
        }

        public async Task<BookDto> GetById(int id)
        {
            var book = await _repositoryBook.GetById(id);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> Add(BookInsertDto insert)
        {
            var book = _mapper.Map<Book>(insert);
            await _repositoryBook.Add(book);
            await _repositoryBook.Save();

            var bookAdd = await _repositoryBook.GetById(book.BookId);

            return _mapper.Map<BookDto>(bookAdd);
        }

        public async Task<BookDto> Update(BookUpdateDto update)
        {
            var book = await _repositoryBook.GetById(update.BookId);
            if (book != null)
            {
                book = _mapper.Map<BookUpdateDto, Book>(update, book);
                _repositoryBook.Update(book);
                await _repositoryBook.Save();

                var bookUpdate = await _repositoryBook.GetById(book.BookId);
                return _mapper.Map<BookDto>(bookUpdate);
            }
            return null;
        }

        public async Task<BookDto> Delete(int id)
        {
            var book = await _repositoryBook.GetById(id);
            if (book != null)
            {
                var bookDto = _mapper.Map<BookDto>(book);

                _repositoryBook.Delete(book);
                _repositoryBook.Save();

                return bookDto;
            }
            return null;
        }

        public bool Validate(BookInsertDto insert)
        {
            if (_repositoryBook.Search(book => book.Title == insert.Title).Count() > 0)
            {
                Errors.Add("The title of the book already exists.");
                return false;
            }
            return true;
        }

        public bool Validate(BookUpdateDto update)
        {
            if (_repositoryBook.Search(book => book.Title == update.Title && book.BookId != update.BookId).Count() > 0)
            {
                Errors.Add("The title of the book already exists.");
                return false;
            }
            return true;
        }
    }
}
