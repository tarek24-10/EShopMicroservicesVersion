using MediatR;

namespace BuldingBlocks.CQRS
{
    public interface ICommandHandler<in TCommand, TResponce> : IRequestHandler<TCommand, TResponce>
        where TCommand : ICommand<TResponce>
        where TResponce : notnull
    {
    }

    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Unit>
        where TCommand : ICommand
    {
    }
}
