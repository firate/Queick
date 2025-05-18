using Appointment.Entity;
using Appointment.Data;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Service;

public class CustomerService : ICustomerService
{
    private readonly AppointmentDbContext _context;
    private readonly AppointmentReadOnlyContext _readOnlyContext;

    public CustomerService(AppointmentDbContext context, AppointmentReadOnlyContext readOnlyContext)
    {
        _context = context;
        _readOnlyContext = readOnlyContext;
    }
    
    public async Task<Customer?> GetCustomerById(long id)
    {
        var customer = await _readOnlyContext.Customers.FirstOrDefaultAsync(x => x.Id == id);

        return customer;
    }
    
    public async Task<Customer?> GetCustomerById2(long id)
    {
        var customer = await _readOnlyContext.Customers.FirstOrDefaultAsync(x => x.Id == id);

        return customer;
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