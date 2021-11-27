using MediatR;
using SmartSolution.Infrastructure.Commands;
using System;
using System.Threading.Tasks;

namespace SmartSolutionCRM.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task<TResponse> ExecuteIdentifiedCommand<TCommand, TResponse>(this IMediator mediator, TCommand command, string requestId)
            where TCommand : IRequest<TResponse>
        {
            var commandResult = default(TResponse);

            //FIXME: temporary bypass requestId 
            if (true && Guid.TryParse(requestId, out var guid) && guid != Guid.Empty)
            {
                var identifiedCommand = new IdentifiedCommand<TCommand, TResponse>(command, guid);
                commandResult = await mediator.Send(identifiedCommand);
            }

            return commandResult;
        }
    }
}
