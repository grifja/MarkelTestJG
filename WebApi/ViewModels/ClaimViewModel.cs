namespace WebApi.ViewModels;

public sealed class ClaimViewModel
{
    /// <summary>
    /// Claim reference Number
    /// </summary>
    /// <remarks>Alias for UCR</remarks>
    public string ClaimReferenceNumber { get; }
    
    public int CompanyId { get; }

    public DateTime ClaimDate { get; set; } = DateTime.UtcNow;

    public DateTime LossDate { get; set; }

    public string AssuredName { get; set; } = string.Empty;
    
    public decimal IncurredLoss { get; set; }

    public bool Closed { get; set; }

    public int ClaimAgeInDays => (DateTime.UtcNow.Date - ClaimDate.Date).Days;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="ucr"><see cref="string"/></param>
    /// <param name="companyId"><see cref="int"/></param>
    /// <exception cref="ArgumentNullException"> on <paramref name="ucr"/></exception>
    public ClaimViewModel(string ucr, int companyId) 
        : base()
    {
        ClaimReferenceNumber = 
            !string.IsNullOrWhiteSpace(ucr) 
                ? ucr :
                throw new ArgumentNullException(nameof(ucr));

        CompanyId = 
            companyId > 0
                ? companyId : 
                throw new ArgumentException($"Invalid {nameof(companyId)}", nameof(companyId));
    }

    /// <summary>
    /// Constructor. Used where no claim has been found
    /// </summary>
    internal ClaimViewModel()
    {
        ClaimReferenceNumber = string.Empty;
    }
}