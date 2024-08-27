namespace WebApi.Services;

public interface IModelResponse<TModel> where TModel : class
{
    TModel? Result { get; set; }
    int StatusCode { get; set; }
    string Message { get; set; }
}