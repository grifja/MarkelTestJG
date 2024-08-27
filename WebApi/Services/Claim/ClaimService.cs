using System.Net;

namespace WebApi.Services.Claim
{
    using Models;
    using Repositories;
    using System.ComponentModel.Design;

    /// <inheritdoc cref="IClaimService"/>
    /// <param name="logger"><see cref="ILogger{ClaimService}"/></param>
    /// <param name="responseBuilder"><see cref="IResponseBuilder"/></param>
    /// <param name="claimRepository"><see cref="IClaimRepository"/></param>
    /// <param name="companyRepository"><see cref="ICompanyRepository"/></param>
    /// <exception cref="ArgumentNullException"></exception>
    internal sealed class ClaimService(
        ILogger<ClaimService> logger,
        IResponseBuilder responseBuilder,
        IClaimRepository claimRepository,
        ICompanyRepository companyRepository) : IClaimService
    {
        private readonly ILogger<ClaimService> _logger = 
            logger ?? throw new ArgumentNullException(nameof(logger));

        private readonly IResponseBuilder _responseBuilder =
            responseBuilder ?? throw new ArgumentNullException(nameof(responseBuilder));

        private readonly IClaimRepository _claimRepository =
            claimRepository ?? throw new ArgumentNullException(nameof(claimRepository));

        private readonly ICompanyRepository _companyRepository =
            companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));

        /// <inheritdoc cref="IClaimService.GetClaimAsync"/>
        public async Task<IModelResponse<ClaimModel>> GetClaimAsync(string claimReferenceNumber)
        {
            if (string.IsNullOrWhiteSpace(claimReferenceNumber))
            {
                _logger.LogInformation("Claim Reference Number is not valid");

                return _responseBuilder.GetResponse<ClaimModel>(
                    StatusCodes.Status422UnprocessableEntity, message: "Invalid Claim Reference Number provided");
            }

            try
            {
                var model = await _claimRepository.GetClaimAsync(claimReferenceNumber);

                return _responseBuilder.GetResponse(StatusCodes.Status200OK, model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, 
                    $"Error occurred during retrieval of claim details for reference number: {claimReferenceNumber}");

                return _responseBuilder.GetResponse<ClaimModel>(
                    StatusCodes.Status500InternalServerError, message: ex.Message);
            }
        }

        /// <inheritdoc cref="IClaimService.GetClaimsAsync"/>
        public async Task<IModelResponse<IEnumerable<ClaimModel>>> GetClaimsAsync(int companyId)
        {
            if (companyId < 1)
            {
                _logger.LogInformation("Company Id is not valid");

                return _responseBuilder.GetResponse<IEnumerable<ClaimModel>>(
                    StatusCodes.Status422UnprocessableEntity,
                    message: "Invalid Company Id provided for claims retrieval");
            }

            try
            {

                if (!await _companyRepository.ExistsAsync(companyId))
                {
                    _logger.LogInformation($"Company not found: {companyId}");

                    return _responseBuilder.GetResponse<IEnumerable<ClaimModel>>(
                        StatusCodes.Status422UnprocessableEntity, 
                        message: $"No matching Company Id provided for claims retrieval: {companyId}");
                }

                var models = await _claimRepository.GetClaimsByCompanyAsync(companyId);

                return _responseBuilder.GetResponse(StatusCodes.Status200OK, models);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    $"Error occurred during retrieval of claim details by company Id: {companyId}");

                return _responseBuilder.GetResponse<IEnumerable<ClaimModel>>(
                    StatusCodes.Status500InternalServerError, message: ex.Message);
            }
        }

        /// <inheritdoc cref="IClaimService.UpdateClaimAsync"/>
        public async Task<IModelResponse<ClaimModel>> UpdateClaimAsync(ClaimModel updateModel)
        {
            if (string.IsNullOrWhiteSpace(updateModel.UCR))
            {
                _logger.LogInformation("No Claim Reference Number present. Update cancelled");

                _responseBuilder.GetResponse(
                    StatusCodes.Status422UnprocessableEntity, updateModel,
                    "Claim Reference Number must be provided");
            }

            if (updateModel.CompanyId < 1)
            {
                _logger.LogInformation($"Company not found: {updateModel.CompanyId}");

                _responseBuilder.GetResponse(
                    StatusCodes.Status422UnprocessableEntity, updateModel, "Invalid Company ID");
            }
            
            try
            {
                if (!await _companyRepository.ExistsAsync(updateModel.CompanyId))
                {
                    _logger.LogInformation("Invalid Company ID. Update cancelled");

                    _responseBuilder.GetResponse(
                        StatusCodes.Status422UnprocessableEntity, updateModel, 
                        $"No matching Company Id provided for claim update: {updateModel.CompanyId}");
                }
                
                return await _claimRepository.UpdateClaimAsync(updateModel)
                    ? _responseBuilder.GetResponse(StatusCodes.Status200OK, updateModel)
                    : _responseBuilder.GetResponse(StatusCodes.Status404NotFound, updateModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    $"Error occurred during update of Claim (Ref.No: {updateModel.UCR})");

                return _responseBuilder.GetResponse<ClaimModel>(
                    StatusCodes.Status500InternalServerError, message: ex.Message);
            }
        }
    }
}