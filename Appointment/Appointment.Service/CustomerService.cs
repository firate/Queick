using Appointment.Entity;

namespace Service;

public class CustomerService : ICustomerService
{


    public Customer GetCustomerById(long id)
    {
        throw new NotImplementedException();
    }

    public Task CreateCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCustomer(long id)
    {
        throw new NotImplementedException();
    }

    public Task<PaginationResult<Customer>> GetCustomers()
    {
        throw new NotImplementedException();
    }
}
