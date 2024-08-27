namespace WebApi.Services;

internal sealed class ResponseBuilder : IResponseBuilder
{
    public IModelResponse<TModel> GetResponse<TModel>(int httpStatusCode, TModel? model = null, string? message = null)
        where TModel : class
    {
        return new ModelResponse<TModel>
        {
            Result = model,
            StatusCode = httpStatusCode,
            Message = message ?? string.Empty
        };
    }
}