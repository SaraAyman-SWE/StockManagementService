using FluentValidation;
using StockExchangeService.Models.Dtos;

namespace StockExchangeService.Validators
{
    public class OrderValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderValidator()
        {
            RequiredChecks();
            FormatChecks();
            
            void RequiredChecks()
            {
                RuleFor(prop => prop.Type)
                    .NotEmpty()
                    .WithMessage("Order Type is required");

                RuleFor(prop => prop.StockSymbol)
                    .NotEmpty()
                    .WithMessage("Stock Symbol is required");
            }
            void FormatChecks()
            {
                RuleFor(prop => prop.Type)
                    .Must(prop => prop.ToLower() == "buy" || prop.ToLower() == "sell")
                    .WithMessage("Order Type must be either Buy or Sell");

                RuleFor(prop => prop.Quantity)
                    .Must(prop => prop > 0)
                    .WithMessage("Quantity must be greater than 0");
            }
        }
    }
}
