using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Equinox.Domain.Core.Core.Messaging;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Equinox.Domain.Core.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}