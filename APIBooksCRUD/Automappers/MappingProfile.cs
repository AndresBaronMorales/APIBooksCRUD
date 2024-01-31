using APIBooksCRUD.DTOs;
using APIBooksCRUD.Models;
using AutoMapper;

namespace APIBooksCRUD.Automappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Genre
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreInsertDto, Genre>();
            CreateMap<GenreUpdateDto, Genre>();

            //Book
            CreateMap<Book, BookDto>();
            CreateMap<BookInsertDto, Book>();
            CreateMap<BookUpdateDto, Book>();
        }
    }
}
