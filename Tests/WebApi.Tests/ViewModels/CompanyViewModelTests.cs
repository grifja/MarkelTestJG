using Xunit;

namespace WebApi.Tests.ViewModels
{
    using WebApi.ViewModels;

    public sealed class CompanyViewModelTests
    {
        [Fact]
        public void PolicyExpirationDateIsNull_ActivePropertyReturnsFalse()
        {
            // Arrange
            var viewModel = new CompanyViewModel(1);
            
            // Act
            // Assert
            Assert.Null(viewModel.PolicyExpirationDate);
            Assert.False(viewModel.Active);
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData(1, true)]
        public void PolicyExpirationDateIsNotNull_ActivePropertyReturnsBoolBasedOnDateTime(int daysToAdd, bool expectedActive)
        {
            // Arrange
            var dateTime = DateTime.UtcNow.AddDays(daysToAdd);

            var viewModel = new CompanyViewModel(1)
            {
                PolicyExpirationDate = dateTime
            };

            // Act
            // Assert
            Assert.IsAssignableFrom<DateTime>(viewModel.PolicyExpirationDate);
            Assert.Equal(dateTime, viewModel.PolicyExpirationDate);
            Assert.Equal(expectedActive, viewModel.Active);
        }
    }
}
