namespace WebApi.Services.Company.Models;

/// <summary>
/// Company Model
/// </summary>
public sealed class CompanyModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? Address3 { get; set; }
    public string? PostCode { get; set; }
    public string Country { get; set; } = string.Empty;

    public DateTime? PolicyExpirationDateTime { get; set; }
}