namespace WebApi.ViewModels.Mapping;

/// <summary>
/// Mapper interface for publicly exposed models
/// </summary>
/// <typeparam name="TViewModel"></typeparam>
/// <typeparam name="TModel"></typeparam>
public interface IMapper<TViewModel,TModel>
    where TViewModel : class
    where TModel : class
{
    /// <summary>
    /// Map internal <typeparamref name="TModel"/> to public <typeparamref name="TViewModel"/>
    /// </summary>
    /// <param name="model"><see cref="TModel"/></param>
    /// <returns><see cref="TViewModel"/></returns>
    TViewModel MapToView(TModel model);

    /// <summary>
    /// Map internal <typeparamref name="TModel"/> array to public <typeparamref name="TViewModel"/>
    /// array.
    /// </summary>
    /// <param name="models"><see cref="IEnumerable{TModel}"/></param>
    /// <returns><see cref="IEnumerable{TViewModel}"/></returns>
    IEnumerable<TViewModel> MapToView(IEnumerable<TModel> models);

    /// <summary>
    /// Map public <typeparamref name="TViewModel"/> to internal <typeparamref name="TModel"/>
    /// </summary>
    /// <param name="viewModel"><see cref="TViewModel"/></param>
    /// <returns><see cref="TModel"/></returns>
    TModel MapToModel(TViewModel viewModel);
}