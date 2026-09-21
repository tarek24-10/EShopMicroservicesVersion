using MediatR;

namespace BuldingBlocks.CQRS
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }

    public interface ICommand : IRequest<Unit>
    {
    }
}
