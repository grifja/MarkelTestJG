namespace WebApi.Services.Company
{
    using Models;
    using Repositories;

    /// <inheritdoc cref="ICompanyService"/>
    /// <param name="logger"><see cref="ILogger{CompanyService}"/></param>
    /// <param name="responseBuilder"><see cref="IResponseBuilder"/></param>
    /// <param name="companyRepository"><see cref="ICompanyRepository"/></param>
    /// <exception cref="ArgumentNullException"></exception>
    internal sealed class CompanyService(
        ILogger<CompanyService> logger,
        IResponseBuilder responseBuilder,
        ICompanyRepository companyRepository) : ICompanyService
    {
        private readonly ILogger<CompanyService> _logger = 
            logger ?? throw new ArgumentNullException(nameof(logger));

        private readonly IResponseBuilder _responseBuilder =
            responseBuilder ?? throw new ArgumentNullException(nameof(responseBuilder));

        private readonly ICompanyRepository _companyRepository =
            companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));


        /// <inheritdoc cref="ICompanyService.GetCompanyAsync"/>
        public async Task<IModelResponse<CompanyModel>> GetCompanyAsync(int id)
        {
            if (id < 1)
            {
                _logger.LogInformation($"Company ID is not valid: {id}");

                return _responseBuilder.GetResponse<CompanyModel>(
                    StatusCodes.Status422UnprocessableEntity, message: $"Invalid Company ID: {id}");
            }

            try
            {
                var model = await _companyRepository.GetCompanyByIdAsync(id);

                return _responseBuilder.GetResponse(StatusCodes.Status200OK, model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred during retrieval of company details by id: {id}");

                return _responseBuilder.GetResponse<CompanyModel>(
                    StatusCodes.Status500InternalServerError, message: ex.Message);
            }
        }
    }
}