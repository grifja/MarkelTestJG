namespace WebApi.ViewModels;

public class UpdateClaimViewModel
{
    /// <summary>
    /// Claim reference Number
    /// </summary>
    /// <remarks>Alias for UCR</remarks>
    public string ClaimReferenceNumber { get; set; } = string.Empty;

    public int CompanyId { get; set; }

    public DateTime ClaimDate { get; set; } = DateTime.UtcNow;

    public DateTime LossDate { get; set; }

    public string AssuredName { get; set; } = string.Empty;

    public decimal IncurredLoss { get; set; }

    public bool Closed { get; set; }
}