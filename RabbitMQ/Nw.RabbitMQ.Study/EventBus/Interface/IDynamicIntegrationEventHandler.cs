using System.Threading.Tasks;

namespace EventBus.Interface
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
