using APIBooksCRUD.DTOs;
using FluentValidation;

namespace APIBooksCRUD.Validators
{
    public class GenreInsertValidator : AbstractValidator<GenreInsertDto>
    {
        public GenreInsertValidator()
        {
            RuleFor(genre => genre.Name).NotEmpty().WithMessage("The name is required.");
            RuleFor(genre => genre.Name).Length(2, 100).WithMessage("The name must contain between 2 and 100 characters.");
            RuleFor(genre => genre.Description).NotEmpty().WithMessage("Description is required.");
        }
    }
}
