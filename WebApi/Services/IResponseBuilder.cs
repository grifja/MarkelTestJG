namespace WebApi.Services
{
    public interface IResponseBuilder
    {
        IModelResponse<TModel> GetResponse<TModel>(int httpStatusCode, TModel? model = null, string? message = null)
            where TModel : class;
    }
}