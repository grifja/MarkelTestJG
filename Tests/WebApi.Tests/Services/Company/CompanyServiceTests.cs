using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Xunit;
using Moq;

namespace WebApi.Tests.Services.Company
{
    using WebApi.Repositories;
    using WebApi.Services;
    using WebApi.Services.Company;
    using WebApi.Services.Company.Models;
    using Helper;
    
    public sealed class CompanyServiceTests : IDisposable
    {
        private readonly Mock<ILogger<CompanyService>> _logger;
        private readonly Mock<IResponseBuilder> _responseBuilder;
        private readonly Mock<ICompanyRepository> _companyRepository;

        private readonly ICompanyService _companyService;

        public CompanyServiceTests()
        {
            _logger = new Mock<ILogger<CompanyService>>(MockBehavior.Default);
            _responseBuilder = new Mock<IResponseBuilder>(MockBehavior.Default);
            _companyRepository = new Mock<ICompanyRepository>(MockBehavior.Default);

            _companyService = 
                new CompanyService(_logger.Object, _responseBuilder.Object, _companyRepository.Object);
        }

        public void Dispose()
        {
            _responseBuilder.VerifyNoOtherCalls();
            _companyRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public void Ctor_NullLoggerArgument_ThrowsArgumentNullException()
        {
            // Arrange
            const string expectedParameter = "logger";

            // Act
            // Assert
            var exception =
                Assert.Throws<ArgumentNullException>(
                    () => new CompanyService(null!, _responseBuilder.Object, _companyRepository.Object));

            Assert.Equal(expectedParameter, exception.ParamName);
        }

        [Fact]
        public void Ctor_NullResponseBuilderArgument_ThrowsArgumentNullException()
        {
            // Arrange
            const string expectedParameter = "responseBuilder";

            // Act
            // Assert
            var exception =
                Assert.Throws<ArgumentNullException>(
                    () => new CompanyService(_logger.Object, null!, _companyRepository.Object));

            Assert.Equal(expectedParameter, exception.ParamName);
        }

        [Fact]
        public void Ctor_NullCompanyRepositoryArgument_ThrowsArgumentNullException()
        {
            // Arrange
            const string expectedParameter = "companyRepository";

            // Act
            // Assert
            var exception =
                Assert.Throws<ArgumentNullException>(
                    () => new CompanyService(_logger.Object, _responseBuilder.Object, null!));

            Assert.Equal(expectedParameter, exception.ParamName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task GetCompanyAsync_InvalidId_ReturnsResponseContainingNullModelWithHttpStatus422(int companyId)
        {
            // Arrange
            var expectedResponseMessage = $"Invalid Company ID: {companyId}";

            _responseBuilder
                .SetResponse<CompanyModel>(
                    StatusCodes.Status422UnprocessableEntity, message: expectedResponseMessage);

            // Act
            var response = await _companyService.GetCompanyAsync(companyId);

            // Assert
            Assert.IsAssignableFrom<IModelResponse<CompanyModel>>(response);
            Assert.Null(response.Result);
            Assert.Equal(StatusCodes.Status422UnprocessableEntity, response.StatusCode);
            Assert.Equal(expectedResponseMessage, response.Message);
            
            _responseBuilder.Verify(
                r => r.GetResponse<CompanyModel>(
                    StatusCodes.Status422UnprocessableEntity, null, expectedResponseMessage),
                Times.Once);
        }

        [Fact]
        public async Task GetCompanyAsync_MatchFound_ReturnsResponseContainingModelWithHttpStatus200()
        {
            // Arrange
            const int companyId = 10;

            var companyModel = new CompanyModel
            {
                Id = companyId,
                Name = "TestName_10",
                Country = "United Kingdom",
                PolicyExpirationDateTime = DateTime.UtcNow.AddMonths(1)
            };

            _companyRepository.SetGetCompanyAsync(companyId, companyModel);
            _responseBuilder.SetResponse(StatusCodes.Status200OK, companyModel);

            // Act
            var response = await _companyService.GetCompanyAsync(companyId);

            // Assert
            Assert.IsAssignableFrom<IModelResponse<CompanyModel>>(response);
            Assert.Equal(companyModel, response.Result);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Empty(response.Message);

            _companyRepository.Verify(cr => cr.GetCompanyByIdAsync(companyId), Times.Once);

            _responseBuilder.Verify(
                r => r.GetResponse(StatusCodes.Status200OK, companyModel, null),
                Times.Once);
        }

        [Fact]
        public async Task GetCompanyAsync_InternalServerErrorOccurs_ReturnsResponseContainingNullModelWithHttpStatus500()
        {
            // Arrange
            const int companyId = 123;
            var expectedResponseMessage = $"Dummy Exception: {companyId}";

            _companyRepository.SetGetCompanyAsync(companyId, new InvalidOperationException(expectedResponseMessage));
            _responseBuilder.SetResponse<CompanyModel>(StatusCodes.Status500InternalServerError, message: expectedResponseMessage);

            // Act
            var response = await _companyService.GetCompanyAsync(companyId);

            // Assert
            Assert.IsAssignableFrom<IModelResponse<CompanyModel>>(response);
            Assert.Null(response.Result);
            Assert.Equal(StatusCodes.Status500InternalServerError, response.StatusCode);
            Assert.Equal(expectedResponseMessage, response.Message);

            _companyRepository.Verify(cr => cr.GetCompanyByIdAsync(companyId), Times.Once);

            _responseBuilder.Verify(
                r => r.GetResponse<CompanyModel>(
                    StatusCodes.Status500InternalServerError, null, expectedResponseMessage),
                Times.Once);
        }
    }
}