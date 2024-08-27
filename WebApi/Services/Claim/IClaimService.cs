namespace WebApi.Services.Claim
{
    using Models;

    /// <summary>
    /// Claim Service
    /// </summary>
    public interface IClaimService
    {
        /// <summary>
        /// Get Claim
        /// </summary>
        /// <param name="claimReferenceNumber"><see cref="string"/></param>
        /// <returns><see cref="T:Task{IModelResponse{ClaimModel}}"/></returns>
        Task<IModelResponse<ClaimModel>> GetClaimAsync(string claimReferenceNumber);

        /// <summary>
        /// Get Claims by Company ID
        /// </summary>
        /// <param name="companyId"><see cref="int"/></param>
        /// <returns><see cref="T:Task{IModelResponse{IEnumerable{ClaimModel}}}"/></returns>
        Task<IModelResponse<IEnumerable<ClaimModel>>> GetClaimsAsync(int companyId);

        /// <summary>
        /// Update Claim
        /// </summary>
        /// <param name="updateModel"><see cref="ClaimModel"/></param>
        /// <returns><see cref="T:Task{IModelResponse{ClaimModel}}"/></returns>
        Task<IModelResponse<ClaimModel>> UpdateClaimAsync(ClaimModel updateModel);
    }
}