using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerByIdAsync(string identificacion);
        Task CreateCustomerAsync(Customer customer);
    }
}
