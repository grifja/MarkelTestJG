namespace WebApi.Repositories
{
    using Services.Company.Models;

    /// <inheritdoc cref="ICompanyRepository"/>>
    /// <param name="dbContext"><see cref="CompanyDbContext"/></param>
    /// <exception cref="ArgumentNullException"></exception>
    internal sealed class CompanyRepository(CompanyDbContext dbContext) : ICompanyRepository
    {
        private readonly CompanyDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        /// <inheritdoc cref="ICompanyRepository.GetCompanyByIdAsync"/>
        public async Task<CompanyModel?> GetCompanyByIdAsync(int id)
        {
            if (id > 100) // Dummy path to force an internal error for demo purposes
            {
                throw new InvalidOperationException("Dummy internal server error has occurred");
            }

            // Task.FromResult here is just to force async behaviour here as we should be expecting
            // the repository to conduct its operations asynchronously with a legitimate DbContext.
            return await Task.FromResult(
                _dbContext.Company.FirstOrDefault(c => c.Id == id));
        }

        /// <inheritdoc cref="ICompanyRepository.ExistsAsync"/>
        public async Task<bool> ExistsAsync(int id)
        {
            // Task.FromResult here is just to force async behaviour here as we should be expecting
            // the repository to conduct its operations asynchronously with a legitimate DbContext.
            return await Task.FromResult(
                _dbContext.Company.Any(c => c.Id == id));
        }
    }


    internal sealed class CompanyDbContext
    {
        public List<CompanyModel> Company =>
        [
            ..new[]
            {
                new CompanyModel
                {
                    Id = 1,
                    Name = "TestName_1"
                },
                new CompanyModel
                {
                    Id = 2,
                    Name = "TestName_2"
                }
            }
        ];
    }
}
