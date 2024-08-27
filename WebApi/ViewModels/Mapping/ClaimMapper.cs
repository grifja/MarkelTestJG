namespace WebApi.ViewModels.Mapping
{
    using Services.Claim.Models;

    /// <summary>
    /// Map Claim view and internal models
    /// </summary>
    /// <inheritdoc cref="IMapper{TViewModel,TModel}"/>
    public sealed class ClaimMapper : IMapper<ClaimViewModel, ClaimModel>
    {
        /// <inheritdoc cref="IMapper{TViewModel,TModel}.MapToView(TModel)"/>
        public ClaimViewModel MapToView(ClaimModel model)
        {
            if (string.IsNullOrWhiteSpace(model.UCR) || model.CompanyId == 0)
            {
                return new ClaimViewModel();
            }

            var viewModel = new ClaimViewModel(model.UCR, model.CompanyId)
            {
                ClaimDate = model.ClaimDate,
                LossDate = model.LossDate,
                AssuredName = model.AssuredName,
                IncurredLoss = model.IncurredLoss,
                Closed = model.Closed
            };

            return viewModel;
        }

        /// <inheritdoc cref="IMapper{TViewModel,TModel}.MapToView(IEnumerable{TModel})"/>
        public IEnumerable<ClaimViewModel> MapToView(IEnumerable<ClaimModel> models)
        {
            var claimModels = models.ToArray();

            return claimModels.Length == 0 
                ? Array.Empty<ClaimViewModel>() : claimModels.Select(MapToView);
        }

        /// <inheritdoc cref="IMapper{TViewModel,TModel}.MapToModel"/>
        public ClaimModel MapToModel(ClaimViewModel viewModel)
        {
            var model = new ClaimModel
            {
                UCR = viewModel.ClaimReferenceNumber,
                CompanyId = viewModel.CompanyId,
                ClaimDate = viewModel.ClaimDate,
                LossDate = viewModel.LossDate,
                AssuredName = viewModel.AssuredName,
                IncurredLoss = viewModel.IncurredLoss,
                Closed = viewModel.Closed
            };

            return model;
        }
    }
}