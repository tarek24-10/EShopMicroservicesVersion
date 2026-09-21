using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuldingBlocks.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse>(ILogger<LoggingBehaviour<TRequest, TResponse>> logger) 
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[Start] Handle request={RequestName} - Response={Response} - RequestData: {RequestData}"
                , typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();
            timer.Stop();

            var timeTaken = timer.Elapsed;

            if (timeTaken.Seconds > 3)
            {
                logger.LogWarning("[Performance] the request {RequestName} took {TimeTaken} s"
                    , typeof(TRequest).Name, timeTaken.Seconds);
            }

            logger.LogInformation("[End] Handled request {RequestName} with Response {Response}"
                , typeof(TRequest).Name, typeof(TResponse).Name);

            return response;
        }
    }
}
