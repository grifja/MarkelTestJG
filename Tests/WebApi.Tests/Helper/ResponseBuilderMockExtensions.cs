using Moq;

namespace WebApi.Tests.Helper
{
    using WebApi.Services;

    internal static class ResponseBuilderMockExtensions
    {
        internal static Mock<IResponseBuilder> SetResponse<TModel>(this Mock<IResponseBuilder> mock, int httpStatusCode,
            TModel? model = null, string? message = null)
            where TModel : class
        {
            if (mock == null)
            {
                throw new ArgumentNullException(nameof(mock));
            }

            mock
                .Setup(m => m.GetResponse(httpStatusCode, model, message))
                .Returns(
                    new ModelResponse<TModel>
                    {
                        Result = model,
                        StatusCode = httpStatusCode,
                        Message = message ?? string.Empty
                    });

            return mock;
        }
    }
}