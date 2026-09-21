using MediatR;

namespace BuldingBlocks.CQRS
{
    public interface IQueryHandler<in TQuery, TResponce> : IRequestHandler<TQuery, TResponce>
        where TQuery : IQuery<TResponce>
        where TResponce : notnull
    {
    }
}
