using Moq;
using Xunit;

namespace WebApi.Tests.ViewModels.Mapping
{
    using WebApi.Services.Company.Models;
    using WebApi.ViewModels;
    using WebApi.ViewModels.Mapping;

    public sealed class CompanyMapperTests
    {
        private readonly IMapper<CompanyViewModel, CompanyModel> _mapper = new CompanyMapper();

        [Fact]
        public void MapToView_EmptyModel_ReturnsDefaultViewModel()
        {
            // Arrange
            var companyModel = new CompanyModel();

            // Act
            var result = _mapper.MapToView(companyModel);

            // Assert
            Assert.IsAssignableFrom<CompanyViewModel>(result);

            Assert.Equal(0, result.Id);
            Assert.Empty(result.Name);
            Assert.Empty(result.AddressLine1);
            Assert.Empty(result.AddressLine2);
            Assert.Empty(result.AddressLine3);
            Assert.Empty(result.PostCode);
            Assert.Empty(result.Country);
            Assert.Null(result.PolicyExpirationDate);
        }

        [Fact]
        public void MapToView_PopulatedModel_ReturnsMappedViewModel()
        {
            // Arrange
            var companyModel = new CompanyModel
            {
                Id = 123,
                Name = "TestName_123",
                Address1 = "TestAddress1_123",
                Address2 = "TestAddress2_123",
                Address3 = "TestAddress3_123",
                PostCode = "TestPostCode_123",
                Country = "United Kingdom",
                PolicyExpirationDateTime = DateTime.UtcNow.AddDays(1)
            };

            // Act
            var result = _mapper.MapToView(companyModel);

            // Assert
            Assert.IsAssignableFrom<CompanyViewModel>(result);

            Assert.Equal(companyModel.Id, result.Id);
            Assert.Equal(companyModel.Name, result.Name);
            Assert.Equal(companyModel.Address1, result.AddressLine1);
            Assert.Equal(companyModel.Address2, result.AddressLine2);
            Assert.Equal(companyModel.Address3, result.AddressLine3);
            Assert.Equal(companyModel.PostCode, result.PostCode);
            Assert.Equal(companyModel.Country, result.Country);
            Assert.Equal(companyModel.PolicyExpirationDateTime, result.PolicyExpirationDate);
            Assert.True(result.Active);
        }

        [Fact]
        public void MapToModel_ThrowsNotSupportedException()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<NotSupportedException>(() => _mapper.MapToModel(It.IsAny<CompanyViewModel>()));
        }
    }
}