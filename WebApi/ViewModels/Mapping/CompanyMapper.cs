namespace WebApi.ViewModels.Mapping
{
    using Services.Company.Models;

    /// <summary>
    /// Map Company view and internal models
    /// </summary>
    /// <inheritdoc cref="IMapper{TViewModel,TModel}"/>
    public sealed class CompanyMapper : IMapper<CompanyViewModel, CompanyModel>
    {
        /// <inheritdoc cref="IMapper{TViewModel,TModel}.MapToView(TModel)"/>
        public CompanyViewModel MapToView(CompanyModel model)
        {
            var viewModel = new CompanyViewModel(model.Id)
            {
                Name = model.Name,
                AddressLine1 = model.Address1 ?? string.Empty,
                AddressLine2 = model.Address2 ?? string.Empty,
                AddressLine3 = model.Address3 ?? string.Empty,
                PostCode = model.PostCode ?? string.Empty,
                Country = model.Country,
                PolicyExpirationDate = model.PolicyExpirationDateTime
            };

            return viewModel;
        }

        /// <inheritdoc cref="IMapper{TViewModel,TModel}.MapToView(IEnumerable{TModel})"/>
        /// <exception cref="NotSupportedException"></exception>
        public IEnumerable<CompanyViewModel> MapToView(IEnumerable<CompanyModel> models) => throw new NotSupportedException();

        /// <inheritdoc cref="IMapper{TViewModel,TModel}.MapToModel"/>
        /// <exception cref="NotSupportedException"></exception>
        public CompanyModel MapToModel(CompanyViewModel viewModel) => throw new NotSupportedException();
    }
}