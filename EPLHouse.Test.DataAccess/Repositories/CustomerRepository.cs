using System;
using System.Collections.Generic;
using System.Text;
using EPLHouse.Test.DataAccess.Models;
using System.Linq;
using EPLHouse.Test.DataAccess.Repositories;

namespace EPLHouse.Test.DataAccess.Models
{

    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {

        public CustomerRepository(AppDBContext dbContext)
        : base(dbContext)
        {

        }

        public Customer GetBestCustomer()
        {
            return GetAll()
                .OrderByDescending(c => c.FirstName)
                .FirstOrDefault();
        }
    }
}
