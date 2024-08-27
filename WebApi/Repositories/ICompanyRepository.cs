namespace WebApi.Repositories
{
    using Services.Company.Models;

    /// <summary>
    /// Company Repository
    /// </summary>
    internal interface ICompanyRepository
    {
        /// <summary>
        /// Get Company By ID
        /// </summary>
        /// <param name="id"><see cref="int"/></param>
        /// <returns><see cref="T:Task{CompanyModel?}"/></returns>
        Task<CompanyModel?> GetCompanyByIdAsync(int id);
        
        /// <summary>
        /// Check Company Exists
        /// </summary>
        /// <param name="id"><see cref="int"/></param>
        /// <returns><see cref="T:Task{bool}"/></returns>
        Task<bool> ExistsAsync(int id);
    }
}