namespace Queick.Company.Application.DTOs;

public class BranchAddressCreationDto
{
    public long BranchId { get; set; }
    
    public bool IsPrimary { get; set; }

    public int AddressLocationType { get; set; }
    public int AddressFunctionType { get; set; }
    
    public string CountryCode { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string StateProvince { get; set; }
    public string CountyDistrict { get; set; }
    public string City { get; set; }
    public string Neighborhood { get; set; }
    public string PostalCode { get; set; }
    public string BuildingNumber { get; set; } // binanın/apartmanın kapı numarası
    public string ApartmentNo { get; set; } // daire numarası
    public string FloorNo { get; set; } // kat no
    public string DoorNo { get; set; } // kapı numarası
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }

    public string AddressTitle { get; set; } // Ev Adresi, İş Adresi
    public string ContactPerson { get; set; } // Teslim Alacak Kişi // Amazon'da benzer bir yapı var.
    public string ContactPhone { get; set; } // Teslim Alacak Kişinin Telefonu   // Amazon'da benzer bir yapı var.
    public string DeliveryNotes { get; set; } // Teslimat için Notlar
}