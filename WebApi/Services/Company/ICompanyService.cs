namespace WebApi.Services.Company
{
    using Models;

    /// <summary>
    /// Company Service
    /// </summary>
    public interface ICompanyService
    {
        /// <summary>
        /// Get Company by ID
        /// </summary>
        /// <param name="id"><see cref="int"/></param>
        /// <returns><see cref="T:Task{IModelResponse{CompanyModel}}"/></returns>
        Task<IModelResponse<CompanyModel>> GetCompanyAsync(int id);
    }
}