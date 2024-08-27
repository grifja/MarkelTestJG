using Moq;
using Xunit;

namespace WebApi.Tests.ViewModels.Mapping
{
    using WebApi.Services.Claim.Models;
    using WebApi.ViewModels;
    using WebApi.ViewModels.Mapping;

    public sealed class ClaimMapperTests
    {
        private readonly IMapper<ClaimViewModel, ClaimModel> _mapper = new ClaimMapper();

        [Fact]
        public void MapToView_EmptyModel_ReturnsDefaultViewModel()
        {
            // Arrange
            var claimModel = new ClaimModel();

            // Act
            var result = _mapper.MapToView(claimModel);

            // Assert
            Assert.IsAssignableFrom<ClaimViewModel>(result);

            Assert.Empty(result.ClaimReferenceNumber);
            Assert.Equal(0, result.CompanyId);
        }

        [Fact]
        public void MapToView_PopulatedModel_ReturnsMappedViewModel()
        {
            // Arrange
            var claimModel = new ClaimModel
            {
                UCR = "RefNo_123",
                CompanyId = 123,
                ClaimDate = DateTime.UtcNow.Date.AddMonths(-1),
                LossDate = DateTime.UtcNow.Date.AddMonths(-1),
                AssuredName = "TestAssuredName_123",
                IncurredLoss = 11950.47m,
                Closed = false
            };

            // Act
            var result = _mapper.MapToView(claimModel);

            // Assert
            Assert.IsAssignableFrom<ClaimViewModel>(result);

            Assert.Equal(claimModel.UCR, result.ClaimReferenceNumber);
            Assert.Equal(claimModel.CompanyId, result.CompanyId);
            Assert.Equal(claimModel.ClaimDate, result.ClaimDate);
            Assert.Equal(claimModel.LossDate, result.LossDate);
            Assert.Equal(claimModel.AssuredName, result.AssuredName);
            Assert.Equal(claimModel.IncurredLoss, result.IncurredLoss);
            Assert.Equal(claimModel.Closed, result.Closed);
        }

        [Fact]
        public void MapToModel_PopulatedViewModel_ReturnsMappedModel()
        {
            // Arrange
            var viewModel = new ClaimViewModel(ucr: "RefNo_123", companyId: 123)
            {
                ClaimDate = DateTime.UtcNow.Date.AddMonths(-1),
                LossDate = DateTime.UtcNow.Date.AddMonths(-1),
                AssuredName = "TestAssuredName_123",
                IncurredLoss = 11950.47m,
                Closed = false
            };

            // Act
            var result = _mapper.MapToModel(viewModel);

            // Assert
            Assert.IsAssignableFrom<ClaimModel>(result);

            Assert.Equal(viewModel.ClaimReferenceNumber, result.UCR);
            Assert.Equal(viewModel.CompanyId, result.CompanyId);
            Assert.Equal(viewModel.ClaimDate, result.ClaimDate);
            Assert.Equal(viewModel.LossDate, result.LossDate);
            Assert.Equal(viewModel.AssuredName, result.AssuredName);
            Assert.Equal(viewModel.IncurredLoss, result.IncurredLoss);
            Assert.Equal(viewModel.Closed, result.Closed);
        }
    }
}