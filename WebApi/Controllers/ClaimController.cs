using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    using Services.Claim;
    using Services.Claim.Models;
    using ViewModels;
    using ViewModels.Mapping;

    [ApiController]
    [Route("api/[controller]")]
    public class ClaimController(
        IClaimService claimService,
        IMapper<ClaimViewModel,ClaimModel> claimMapper,
        IMapper<UpdateClaimViewModel,ClaimModel> updateClaimMapper) : ControllerBase
    {
        private readonly IClaimService _claimService =
            claimService ?? throw new ArgumentNullException(nameof(claimService));

        private readonly IMapper<ClaimViewModel, ClaimModel> _claimMapper =
            claimMapper ?? throw new ArgumentNullException(nameof(claimMapper));

        private readonly IMapper<UpdateClaimViewModel, ClaimModel> _updateClaimMapper =
            updateClaimMapper ?? throw new ArgumentNullException(nameof(updateClaimMapper));


        [HttpGet("{referenceNumber}")]
        public async Task<ActionResult> Get(string referenceNumber)
        {
            var internalResponse = await _claimService.GetClaimAsync(referenceNumber);
            
            return new JsonResult(_claimMapper.MapToView(internalResponse.Result ?? new ClaimModel()))
            {
                StatusCode = internalResponse.StatusCode
            };
        }

        [HttpGet("list/{companyId:int}")]
        public async Task<ActionResult> Get(int companyId)
        {
            var internalResponse = await _claimService.GetClaimsAsync(companyId);

            return new JsonResult(_claimMapper.MapToView(internalResponse.Result ?? Array.Empty<ClaimModel>()))
            {
                StatusCode = internalResponse.StatusCode
            };
        }
        
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateClaimViewModel viewModel)
        {
            var updateModel = _updateClaimMapper.MapToModel(viewModel);

            var internalResponse = await _claimService.UpdateClaimAsync(updateModel);

            return new JsonResult(_claimMapper.MapToView(internalResponse.Result ?? new ClaimModel()))
            {
                StatusCode = internalResponse.StatusCode
            };
        }
    }
}
