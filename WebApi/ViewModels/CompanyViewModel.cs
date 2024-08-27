namespace WebApi.ViewModels;

public sealed class CompanyViewModel(int id)
{
    public int Id { get; } = id;
    public string Name { get; set; } = string.Empty;

    public string AddressLine1 { get; set; } = string.Empty;
    public string AddressLine2 { get; set; } = string.Empty;
    public string AddressLine3 { get; set; } = string.Empty;
    public string PostCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public DateTime? PolicyExpirationDate { get; set; }

    public bool Active => PolicyExpirationDate is not null && PolicyExpirationDate >= DateTime.UtcNow;
}