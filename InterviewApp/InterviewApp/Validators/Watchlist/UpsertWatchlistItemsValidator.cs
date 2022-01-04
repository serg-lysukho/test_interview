using FluentValidation;
using InterviewApp.ViewModels.Watchlist;

namespace InterviewApp.Validators.Watchlist
{
    public class UpsertWatchlistItemsValidator : AbstractValidator<UpsertWatchlistItemsViewModel>
    {
        public UpsertWatchlistItemsValidator()
        {
            RuleFor(w => w.UserId)
                .GreaterThan(0).WithMessage("User Id must be greater than 0");

            RuleFor(w => w.FilmsId)
                .NotEmpty().WithMessage("There are no films id");

            RuleForEach(w => w.FilmsId)
                .NotNull()
                .MinimumLength(6).WithMessage("Film Id can't be less than 6 symbols")
                .Matches(@"^[t]{2}\d+$").WithMessage("Film Id doesn't match the standard template");
        }
    }
}
