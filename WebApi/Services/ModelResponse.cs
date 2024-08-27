namespace WebApi.Services;

public sealed class ModelResponse<TModel> : IModelResponse<TModel> where TModel : class
{
    public TModel? Result { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
}