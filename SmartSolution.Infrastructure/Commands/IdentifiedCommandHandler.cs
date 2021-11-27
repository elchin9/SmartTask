using MediatR;
using SmartSolution.Infrastructure.Idempotency;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSolution.Infrastructure.Commands
{
    public class IdentifiedCommandHandler<T, R> : IRequestHandler<IdentifiedCommand<T, R>, R>
         where T : IRequest<R>
    {
        private readonly IMediator _mediator;
        private readonly IRequestManager _requestManager;

        public IdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager)
        {
            _mediator = mediator;
            _requestManager = requestManager;
        }

        /// <summary>
        ///     This method handles the command. It just ensures that no other request exists with the same ID, and if this is the  
        ///     case
        ///     just enqueues the original inner command.
        /// </summary>
        /// <param name="request">IdentifiedCommand which contains both original command & request ID</param>
        /// <returns>Return value of inner command or default value if request same ID was found</returns>
        public async Task<R> Handle(IdentifiedCommand<T, R> request, CancellationToken cancellationToken)
        {
            var alreadyExists = await _requestManager.ExistAsync(request.Key);
            if (alreadyExists)
            {
                return CreateResultForDuplicateRequest();
            }

            await _requestManager.CreateRequestForCommandAsync<T>(request.Key);

            // Send the embeded business command to mediator so it runs its related CommandHandler 
            var result = await _mediator.Send(request.Command, cancellationToken);

            return result;
        }

        /// <summary>
        ///     Creates the result value to return if a previous request was found
        /// </summary>
        /// <returns></returns>
        protected virtual R CreateResultForDuplicateRequest()
        {
            return default(R);
        }
    }
}
