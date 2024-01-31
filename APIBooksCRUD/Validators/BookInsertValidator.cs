using APIBooksCRUD.DTOs;
using FluentValidation;

namespace APIBooksCRUD.Validators
{
    public class BookInsertValidator : AbstractValidator<BookInsertDto>
    {
        public BookInsertValidator()
        {
            RuleFor(book => book.GenreId).GreaterThan(0).WithMessage("The genre id is required.");
            RuleFor(book => book.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(book => book.Title).Length(2, 150).WithMessage("The title must contain between 2 and 150 characters.");
            RuleFor(book => book.Author).NotEmpty().WithMessage("The author is required.");
            RuleFor(book => book.Author).Length(2, 150).WithMessage("The author must contain between 2 and 100 characters.");
            RuleFor(book => book.PublicationDate).NotEmpty().WithMessage("Publication date is required.");
            RuleFor(book => book.PageCount).GreaterThan(0).WithMessage("The number of pages is required.");
            RuleFor(book => book.Synopsis).NotEmpty().WithMessage("Synopsis is required.");
            RuleFor(book => book.Value).GreaterThan(0).WithMessage("The value is required.");
        }
    }
}
