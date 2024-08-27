using Xunit;

namespace WebApi.Tests.ViewModels
{
    using WebApi.ViewModels;

    public sealed class ClaimViewModelTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void EmptyUcrArgumentProvided_ThrowsArgumentNullException(string? ucr)
        {
            // Arrange
            const int companyId = 123;

            // Act
            // Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new ClaimViewModel(ucr!, companyId));

            Assert.Equal(nameof(ucr), exception.ParamName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void InvalidCompanyIdProvided_ThrowsArgumentException(int companyId)
        {
            // Arrange
            const string ucr = "RandomUcrValue";

            // Act
            // Assert
            var exception = Assert.Throws<ArgumentException>(() => new ClaimViewModel(ucr, companyId));

            Assert.Equal(nameof(companyId), exception.ParamName);
        }

        [Fact]
        public void ConstructorArgumentsProvided_ReturnsViewModel()
        {
            // Arrange
            const string ucr = "RandomUcrValue";
            const int companyId = 123;

            // Act
            var viewModel = new ClaimViewModel(ucr, companyId);

            // Assert
            Assert.Equal(ucr, viewModel.ClaimReferenceNumber);
            Assert.Equal(companyId, viewModel.CompanyId);
        }

        [Fact]
        public void NoClaimDateProvided_DefaultUTCNowValueAssigned()
        {
            // Arrange
            const string ucr = "RandomUcrValue";
            const int companyId = 123;

            var date = DateTime.UtcNow.Date;

            // Act
            var viewModel = new ClaimViewModel(ucr, companyId);

            // Assert
            Assert.IsAssignableFrom<DateTime>(viewModel.ClaimDate);
            Assert.Equal(date, viewModel.ClaimDate.Date);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-200)]
        public void ClaimAgeInDays_CalculatedFromClaimDateToUtcNowDate(int daysToAdd)
        {
            // Arrange
            const string ucr = "RandomUcrValue";
            const int companyId = 123;

            var date = DateTime.UtcNow.Date.AddDays(daysToAdd);
            
            // Act
            var viewModel = new ClaimViewModel(ucr, companyId)
            {
                ClaimDate = date
            };

            var expectedClaimAgeInDays = (DateTime.UtcNow.Date - viewModel.ClaimDate).Days;

            // Assert
            Assert.Equal(expectedClaimAgeInDays, viewModel.ClaimAgeInDays);
        }
    }
}
