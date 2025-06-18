using Queick.Company.Application.Common;

namespace Queick.Company.Application.DTOs;

public class BranchAddressSearchDto: BaseSearchRequestDto
{
    public Guid BranchId { get; set; }
    public int AddressLocationType { get; set; }
    public int AddressFunctionType { get; set; }

    public string CountryCode { get; set; }
    public string StateProvince { get; set; }   // İl
    public string CountyDistrict { get; set; }  // İlçe
    
    public string PostalCode { get; set; }
    
    public string AddressTitle { get; set; } // Ev Adresi, İş Adresi
   
}