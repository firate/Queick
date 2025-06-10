using Microsoft.EntityFrameworkCore.Storage;
using Queick.Company.Application.Interfaces;

namespace Queick.Company.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    private bool _disposed = false;
    
    private readonly ApplicationDbContext _context;
    private readonly ICompanyRepository _companyRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public UnitOfWork(
        ApplicationDbContext context, 
        ICompanyRepository companyRepository, 
        IBranchRepository branchRepository, 
        IRoleRepository roleRepository, 
        IUserRepository userRepository, 
        IPermissionRepository permissionRepository, 
        IRefreshTokenRepository refreshTokenRepository)
    {
        _context = context;
        _companyRepository = companyRepository;
        _branchRepository = branchRepository;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _permissionRepository = permissionRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }
    
    public ICompanyRepository Companies => _companyRepository;
    public IBranchRepository Branches => _branchRepository;
    public IRoleRepository Roles => _roleRepository;
    public IPermissionRepository Permissions => _permissionRepository;
    public IUserRepository Users => _userRepository;
    public IRefreshTokenRepository RefreshTokens => _refreshTokenRepository;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            throw new InvalidOperationException("Transaction already started");
        }
            
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction to commit");
        }

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await _transaction.RollbackAsync(cancellationToken);
            throw;
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction to rollback");
        }

        try
        {
            await _transaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _transaction?.Dispose();
            _context.Dispose();
            _disposed = true;
        }
    }
}