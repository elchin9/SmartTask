using System.Threading.Tasks;

namespace SmartSolution.SharedKernel.Infrastructure.IntegrationEvents
{
    public interface IEventBus
    {
        Task PublishAsync(string topicName, IntegrationEvent integrationEvent);
    }
}
