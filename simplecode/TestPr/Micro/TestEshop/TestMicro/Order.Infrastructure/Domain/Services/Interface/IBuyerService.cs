using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Order.Infrastructure.Domain.AggregatesModel.Entities;
using Order.Infrastructure.Domain.Services.Base;

namespace Order.Infrastructure.Domain.Services.Interface
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Buyer Aggregate
    public interface IBuyerService : IRepository<Buyer>
    {
        Buyer Add(Buyer buyer);
        Buyer Update(Buyer buyer);
        Task<Buyer> FindAsync(string buyerIdentityGuid);
        Task<Buyer> FindByIdAsync(string id);
    }
}
