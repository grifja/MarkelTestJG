namespace WebApi.Repositories
{
    using Services.Claim.Models;

    /// <inheritdoc cref="IClaimRepository"/>
    /// <param name="claimDbContext"><see cref="ClaimDbContext"/></param>
    internal sealed class ClaimRepository(ClaimDbContext claimDbContext) : IClaimRepository
    {
        private readonly ClaimDbContext _claimDbContext = 
            claimDbContext ?? throw new ArgumentNullException(nameof(claimDbContext));

        /// <inheritdoc cref="IClaimRepository.GetClaimAsync"/>
        public async Task<ClaimModel?> GetClaimAsync(string ucr)
        {
            // Task.FromResult here is just to force async behaviour here as we should be expecting
            // the repository to conduct its operations asynchronously with a legitimate DbContext.
            return await Task.FromResult(
                _claimDbContext.Claim.FirstOrDefault(c => c.UCR.Equals(ucr, StringComparison.InvariantCultureIgnoreCase)));
        }

        /// <inheritdoc cref="IClaimRepository.GetClaimsByCompanyAsync"/>
        public async Task<IEnumerable<ClaimModel>> GetClaimsByCompanyAsync(int companyId)
        {
            // Task.FromResult here is just to force async behaviour here as we should be expecting
            // the repository to conduct its operations asynchronously with a legitimate DbContext.
            return await Task.FromResult(
                _claimDbContext.Claim.Where(c => c.CompanyId == companyId));
        }

        /// <inheritdoc cref="IClaimRepository.UpdateClaimAsync"/>
        /// <remarks>
        /// This doesn't work correctly; the data changes are not persisted to the Singleton dummy
        /// Db Context. I should probably have implemented an in-memory EFCore database repository
        /// </remarks>
        public async Task<bool> UpdateClaimAsync(ClaimModel updateModel)
        {
            var index =
                _claimDbContext.Claim.FindIndex(
                    c => c.UCR.Equals(updateModel.UCR, StringComparison.InvariantCultureIgnoreCase));

            if (index == -1)
            {
                // This shouldn't happen as we should never allow poor data get this far; Coulda/Shoulda/Woulda ...
                return false;
            }

            _claimDbContext.Claim.RemoveAt(index);
            _claimDbContext.Claim.Insert(index, updateModel);

            // Task.FromResult here is just to force async behaviour here as we should be expecting
            // the repository to conduct its operations asynchronously with a legitimate DbContext.
            return await Task.FromResult(true);
        }
    }

    internal sealed class ClaimDbContext
    {
        public List<ClaimModel> Claim =>
        [
            ..new[]
            {
                new ClaimModel
                {
                    UCR = "Ref_1_0001",
                    CompanyId = 1,
                    ClaimDate = DateTime.UtcNow.AddYears(-2),
                    LossDate = DateTime.UtcNow.AddYears(-2),
                    AssuredName = "Test_AssuredName_1",
                    IncurredLoss = 9566.99m,
                    Closed = true
                },
                new ClaimModel
                {
                    UCR = "Ref_2_0001",
                    CompanyId = 2,
                    ClaimDate = DateTime.UtcNow.AddMonths(-1),
                    LossDate = DateTime.UtcNow.AddMonths(-1),
                    AssuredName = "Test_AssuredName_2",
                    IncurredLoss = 14766.13m,
                    Closed = false
                },
                new ClaimModel
                {
                    UCR = "Ref_1_0002",
                    CompanyId = 1,
                    ClaimDate = DateTime.UtcNow.AddDays(-1),
                    LossDate = DateTime.UtcNow.AddDays(-2),
                    AssuredName = "Test_AssuredName_1",
                    IncurredLoss = 23000.0m,
                    Closed = false
                },
            }
        ];
    }
}