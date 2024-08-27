namespace WebApi.ViewModels.Mapping
{
    using Services.Claim.Models;

    /// <summary>
    /// Map Claim view and internal models
    /// </summary>
    /// <inheritdoc cref="IMapper{TViewModel,TModel}"/>
    public sealed class UpdateClaimMapper : IMapper<UpdateClaimViewModel, ClaimModel>
    {
        /// <inheritdoc cref="IMapper{TViewModel,TModel}.MapToView(TModel)"/>
        public UpdateClaimViewModel MapToView(ClaimModel model) => throw new NotSupportedException();

        /// <inheritdoc cref="IMapper{TViewModel,TModel}.MapToView(IEnumerable{TModel})"/>
        public IEnumerable<UpdateClaimViewModel> MapToView(IEnumerable<ClaimModel> models) => throw new NotSupportedException();

        /// <inheritdoc cref="IMapper{TViewModel,TModel}.MapToModel"/>
        public ClaimModel MapToModel(UpdateClaimViewModel viewModel)
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