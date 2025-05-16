namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string userName) : ICommand<DeleteBasketResult>;

    public record DeleteBasketResult(bool isSuccess);

    public class DeleteBAsketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBAsketCommandValidator()
        {
            RuleFor(x => x.userName).NotEmpty().WithMessage("UserName is required");
        }
    }

    public class DeleteBasketHandler(IBasketRepository repository)
        : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            await repository.DeleteBasket(command.userName, cancellationToken);

            return new DeleteBasketResult(true);
        }
    }
}
