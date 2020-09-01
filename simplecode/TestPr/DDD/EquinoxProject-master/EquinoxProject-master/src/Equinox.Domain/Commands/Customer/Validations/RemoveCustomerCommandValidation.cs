using Equinox.Domain.Commands.Customer.Validations;

namespace Equinox.Domain.Commands.Customer.Validations
{
    public class RemoveCustomerCommandValidation : CustomerValidation<RemoveCustomerCommand>
    {
        public RemoveCustomerCommandValidation()
        {
            ValidateId();
        }
    }
}