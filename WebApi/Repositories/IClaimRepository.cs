namespace WebApi.Repositories
{
    using Services.Claim.Models;

    /// <summary>
    /// Claim Repository
    /// </summary>
    public interface IClaimRepository
    {
        /// <summary>
        /// Get Claim by Reference Number
        /// </summary>
        /// <param name="ucr"><see cref="string"/></param>
        /// <returns><see cref="T:Task{ClaimModel?}"/></returns>
        Task<ClaimModel?> GetClaimAsync(string ucr);

        /// <summary>
        /// Get Claims by Company ID
        /// </summary>
        /// <param name="companyId"><see cref="int"/></param>
        /// <returns><see cref="T:Task{IEnumerable{ClaimModel}}"/></returns>
        Task<IEnumerable<ClaimModel>> GetClaimsByCompanyAsync(int companyId);

        /// <summary>
        /// Update Claim
        /// </summary>
        /// <param name="updateModel"><see cref="ClaimModel"/></param>
        /// <returns><see cref="T:Task{bool}"/></returns>
        Task<bool> UpdateClaimAsync(ClaimModel updateModel);
    }
}