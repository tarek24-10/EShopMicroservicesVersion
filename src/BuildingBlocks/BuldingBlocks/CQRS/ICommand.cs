using MediatR;

namespace BuldingBlocks.CQRS
{
    public interface ICommand<out TResponce> : IRequest<TResponce>
    {
    }

    public interface ICommand : IRequest<Unit>
    {
    }
}
