namespace WebApi.Services.Claim.Models;

public sealed class ClaimModel
{
    public string UCR { get; set; } = string.Empty;

    public int CompanyId { get; set; }

    public DateTime ClaimDate { get; set; }

    public DateTime LossDate { get; set; }

    public string AssuredName { get; set; } = string.Empty;

    public decimal IncurredLoss { get; set; }

    public bool Closed { get; set; }
}