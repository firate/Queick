using Queick.Company.Domain.Common;
using Queick.Company.Domain.Events;
using Queick.Company.Domain.Exceptions;
using Queick.Company.Domain.ValueObjects;
using DomainEvent = Queick.Company.Domain.Common.DomainEvent;

namespace Queick.Company.Domain;

public class CompanyDomain : Entity, ISoftDeleteEntity, IActivatable, IAuditableEntity
{
    private readonly List<Branch> _branches = [];
    private readonly List<DomainEvent> _domainEvents = [];
    public CompanyName CompanyName { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public string DeletedBy { get; private set; }
    public string? CreatedBy { get; private set; }
    public string? UpdatedBy { get; private set; }
    public DateTimeOffset Created { get; private set; }
    public DateTimeOffset Updated { get; private set; }

    // for EF Core 
    private CompanyDomain()
    {
    }

    public CompanyDomain(CompanyName companyName,
        string description,
        string? createdBy,
        string? updatedBy)
    {
        if (description == null) throw new ArgumentNullException(nameof(description));

        CompanyName = companyName;
        Description = description;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        Created = DateTimeOffset.UtcNow;
        Updated = DateTimeOffset.UtcNow;
    }

    public IReadOnlyCollection<Branch> Branches => _branches.AsReadOnly();
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    // Business Methods
    public void Activate(string activatedBy)
    {
        if (string.IsNullOrWhiteSpace(activatedBy))
            throw new ArgumentException("ActivatedBy cannot be empty", nameof(activatedBy));

        if (IsDeleted)
            throw new CompanyAlreadyDeletedException();

        if (!IsActive)
        {
            IsActive = true;
            UpdatedBy = activatedBy;
            Updated = DateTimeOffset.UtcNow;

            AddDomainEvent(new CompanyActivatedEvent(Id, activatedBy));
        }
    }

    public void Deactivate(string deactivatedBy, string reason = "")
    {
        if (string.IsNullOrWhiteSpace(deactivatedBy))
        {
            throw new ArgumentException("DeactivatedBy cannot be empty", nameof(deactivatedBy));
        }

        if (IsDeleted)
        {
            throw new CompanyAlreadyDeletedException();
        }


        if (IsActive)
        {
            // Business rule: Cannot deactivate if there are active branches
            if (_branches.Any(b => b.IsActive))
            {
                throw new DomainException("Cannot deactivate company with active branches");
            }

            IsActive = false;
            UpdatedBy = deactivatedBy;
            Updated = DateTimeOffset.UtcNow;

            AddDomainEvent(new CompanyDeactivatedEvent(Id, deactivatedBy, reason));
        }
    }

    public void MarkAsDeleted(string deletedBy, string reason = "")
    {
        if (string.IsNullOrWhiteSpace(deletedBy))
        {
            throw new ArgumentException("DeletedBy cannot be empty", nameof(deletedBy));
        }

        if (IsDeleted)
        {
            // Already deleted
            return;
        }

        // Business rule: Must be inactive before deletion
        if (IsActive)
        {
            throw new DomainException("Company must be deactivated before deletion");
        }

        IsDeleted = true;
        DeletedAt = DateTimeOffset.UtcNow;
        DeletedBy = deletedBy;
        Updated = DateTimeOffset.UtcNow;
        UpdatedBy = deletedBy;

        AddDomainEvent(new EntitySoftDeletedEvent(deletedBy, reason));
        //AddDomainEvent(new CompanyDeletedEvent(Id, deletedBy, reason));
    }

    // recover from soft deletion
    public void Restore(string restoredBy)
    {
        if (string.IsNullOrWhiteSpace(restoredBy))
        {
            throw new ArgumentException("RestoredBy cannot be empty", nameof(restoredBy));
        }


        if (!IsDeleted)
        {
            return; // Not deleted
        }

        IsDeleted = false;
        DeletedAt = null;
        DeletedBy = null;
        Updated = DateTimeOffset.UtcNow;
        UpdatedBy = restoredBy;

        // Business rule: Restored companies should be inactive by default
        IsActive = false;
    }

    public void UpdateCompanyInfo(CompanyName newCompanyName, string newDescription, string updatedBy)
    {
        if (IsDeleted)
        {
            throw new CompanyAlreadyDeletedException();
        }

        if (string.IsNullOrWhiteSpace(updatedBy))
        {
            throw new ArgumentException("UpdatedBy cannot be empty", nameof(updatedBy));
        }

        // Business rule: Active companies must have description
        if (IsActive && string.IsNullOrWhiteSpace(newDescription))
        {
            throw new DomainException("Active company must have a description");
        }

        CompanyName = newCompanyName;
        Description = newDescription;
        UpdatedBy = updatedBy;
        Updated = DateTimeOffset.UtcNow;
    }

    public void AddBranch(Branch branch, string addedBy)
    {
        if (branch == null)
            throw new ArgumentNullException(nameof(branch));

        if (IsDeleted)
            throw new CompanyAlreadyDeletedException();

        if (!IsActive)
            throw new CompanyNotActiveException();

        if (string.IsNullOrWhiteSpace(addedBy))
            throw new ArgumentException("AddedBy cannot be empty", nameof(addedBy));

        // Business rule: Branch names must be unique within company
        if (_branches.Any(b => b.Name.Equals(branch.Name, StringComparison.OrdinalIgnoreCase) && !b.IsDeleted))
            throw new DomainException($"Branch with name '{branch.Name}' already exists");

        _branches.Add(branch);
        UpdatedBy = addedBy;
        Updated = DateTimeOffset.UtcNow;

        AddDomainEvent(new BranchAddedEvent(Id, branch.Id, addedBy));
    }

    public void RemoveBranch(Guid branchId, string removedBy, string reason = "")
    {
        if (IsDeleted)
            throw new CompanyAlreadyDeletedException();

        if (string.IsNullOrWhiteSpace(removedBy))
            throw new ArgumentException("RemovedBy cannot be empty", nameof(removedBy));

        var branch = _branches.FirstOrDefault(b => b.Id == branchId);
        if (branch == null)
            throw new DomainException("Branch not found");

        // Soft delete the branch instead of removing
        //branch.SoftDelete(removedBy, reason);
        UpdatedBy = removedBy;
        Updated = DateTimeOffset.UtcNow;
    }

    // Domain Event Management
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    private void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    // Query Methods
    public bool CanBeActivated() => !IsDeleted && !IsActive;
    public bool CanBeDeactivated() => !IsDeleted && IsActive && !_branches.Any(b => b.IsActive);
    public bool CanBeDeleted() => !IsDeleted && !IsActive;
    public int ActiveBranchCount => _branches.Count(b => b is { IsActive: true, IsDeleted: false });
}