using EPLHouse.Test.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EPLHouse.Test.DataAccess.Repositories
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Customer GetBestCustomer();
    }
}
