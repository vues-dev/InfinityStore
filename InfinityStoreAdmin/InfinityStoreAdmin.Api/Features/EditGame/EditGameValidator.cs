using FluentValidation;

namespace InfinityStoreAdmin.Api.Features.EditGame;

public class EditGameValidator : AbstractValidator<EditGameCommand>
{
    public EditGameValidator()
    {
        // Id validation: not empty
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Id is required.");
        // Title validation: not empty and length constraints
        RuleFor(command => command.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(2, 100).WithMessage("Title must be between 2 and 100 characters.");
        // Description validation: not empty and perhaps a maximum length
        RuleFor(command => command.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description can't be more than 500 characters.");
        // ImagePath validation: not empty and must be a valid URL
        RuleFor(command => command.ImagePath)
            .NotEmpty().WithMessage("Image URL is required.");
        // Price validation: greater than or equal to zero
        RuleFor(command => command.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");
    }
}