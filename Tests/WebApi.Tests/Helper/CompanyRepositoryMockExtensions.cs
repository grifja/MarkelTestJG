using Moq;

namespace WebApi.Tests.Helper
{
    using WebApi.Repositories;
    using WebApi.Services.Company.Models;

    internal static class CompanyRepositoryMockExtensions
    {
        internal static Mock<ICompanyRepository> SetGetCompanyAsync(this Mock<ICompanyRepository> mock, int companyId,
            CompanyModel model)
        {
            if (mock == null)
            {
                throw new ArgumentNullException(nameof(mock));
            }

            mock.Setup(m => m.GetCompanyByIdAsync(companyId))
                .ReturnsAsync(model);

            return mock;
        }

        internal static Mock<ICompanyRepository> SetGetCompanyAsync<TException>(this Mock<ICompanyRepository> mock,
            int companyId, TException exception)
            where TException : Exception
        {
            if (mock == null)
            {
                throw new ArgumentNullException(nameof(mock));
            }

            mock.Setup(m => m.GetCompanyByIdAsync(companyId))
                .ThrowsAsync(exception);

            return mock;
        }
    }
}