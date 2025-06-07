namespace Queick.Company.Domain;

public class Address : IEntity, ISoftDeleteEntity, IAuditableEntity
{
    private DateTimeOffset _updated;
    public long Id { get; set; }
    public AddressLocationType AddressLocationType { get; set; }
    public AddressFunctionType AddressFunctionType { get; set; }

    public string CountryCode { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string StateProvince { get; set; }
    public string CountyDistrict { get; set; }
    public string City { get; set; }
    public string Neighborhood { get; set; }
    public string PostalCode { get; set; }
    public string BuildingNumber { get; set; }  // binanın/apartmanın kapı numarası
    public string ApartmentNo { get; set; } // daire numarası
    public string FloorNo { get; set; } // kat no
    public string DoorNo { get; set; } // kapı numarası
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }

    // Name alanını, AddressTitle olarak değerlendiriyorum, o yüzden kaldırdım.
    public string AddressTitle { get; set; }    // Ev Adresi, İş Adresi
    public string ContactPerson { get; set; }   // Teslim Alacak Kişi // Amazon'da benzer bir yapı var.
    public string ContactPhone { get; set; }    // Teslim Alacak Kişinin Telefonu   // Amazon'da benzer bir yapı var.
    public string DeliveryNotes { get; set; }   // Teslimat için Notlar
    
    public bool IsPrimary { get; set; }
    
    // TODO: Nasıl verify edilebilir, bakılmalı.
    // Oluşturulduktan sonra Sms veya Email onayı olabilir.
    public bool IsVerified { get; set; }    

    public long? BranchId { get; set; }
    public Branch? Branch { get; set; }

    public long? CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public long? EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    public long? LocationId { get; set; }
    public Location? Location { get; set; }
    
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string DeletedBy { get; set; } = "System";
}