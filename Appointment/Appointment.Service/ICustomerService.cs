using Appointment.Entity;

namespace Service;

public interface ICustomerService
{
    Task<Customer?> GetCustomerById(long id);
    Task CreateCustomer(Customer customer);
    
    Task UpdateCustomer(Customer customer);
    
    Task<bool> DeleteCustomer(long id);
    
    Task<PaginationResult<Customer>> GetCustomers();
}