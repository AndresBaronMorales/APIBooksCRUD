using APIBooksCRUD.DTOs;
using APIBooksCRUD.Models;
using APIBooksCRUD.Repository;
using AutoMapper;

namespace APIBooksCRUD.Services
{
    public class GenreService : ICommonService<GenreDto, GenreInsertDto, GenreUpdateDto>
    {
        private IRepository<Genre> _repositoryGenre;
        private IMapper _mapper;

        public List<string> Errors { get; }

        public GenreService(IRepository<Genre> repositoryGenre, IMapper mapper)
        {
            _repositoryGenre = repositoryGenre;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<GenreDto>> GetAll()
        {
            var genres = await _repositoryGenre.GetAll();
            return genres.Select(genre => _mapper.Map<GenreDto>(genre));
        }

        public async Task<GenreDto> GetById(int id)
        {
            var genre = await _repositoryGenre.GetById(id);
            return _mapper.Map<GenreDto>(genre);
        }

        public async Task<GenreDto> Add(GenreInsertDto insert)
        {
            var genre = _mapper.Map<Genre>(insert);
            await _repositoryGenre.Add(genre);
            await _repositoryGenre.Save();

            return _mapper.Map<GenreDto>(genre);
        }

        public async Task<GenreDto> Update(GenreUpdateDto update)
        {
            var genre = await _repositoryGenre.GetById(update.GenreId);
            if (genre != null)
            {
                genre = _mapper.Map<GenreUpdateDto, Genre>(update, genre);
                _repositoryGenre.Update(genre);
                await _repositoryGenre.Save();

                return _mapper.Map<GenreDto>(genre);
            }
            return null;
        }

        public async Task<GenreDto> Delete(int id)
        {
            var genre = await _repositoryGenre.GetById(id);
            if (genre != null)
            {
                var genreDto = _mapper.Map<GenreDto>(genre);

                _repositoryGenre.Delete(genre);
                await _repositoryGenre.Save();

                return genreDto;
            }
            return null;
        }

        public bool Validate(GenreInsertDto insert)
        {
            if (_repositoryGenre.Search(genre => genre.Name == insert.Name).Count() > 0)
            {
                Errors.Add("The name of the genre already exists");
                return false;
            }
            return true;
        }

        public bool Validate(GenreUpdateDto update)
        {
            if (_repositoryGenre.Search(genre => genre.Name == update.Name
            && update.GenreId != genre.GenreId).Count() > 0)
            {
                Errors.Add("The name of the genre already exists");
                return false;
            }
            return true;
        }
    }
}
