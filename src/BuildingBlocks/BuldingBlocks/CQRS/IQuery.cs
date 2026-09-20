using MediatR;

namespace BuldingBlocks.CQRS
{
    public interface IQuery<out TResponce> : IRequest<TResponce> 
        where TResponce : notnull
    {
    }
}
