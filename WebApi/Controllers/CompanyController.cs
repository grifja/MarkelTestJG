using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    using Services.Company;
    using Services.Company.Models;
    using ViewModels;
    using ViewModels.Mapping;

    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController(
        ICompanyService companyService,
        IMapper<CompanyViewModel,CompanyModel> companyMapper) : ControllerBase
    {
        private readonly ICompanyService _companyService =
            companyService ?? throw new ArgumentNullException(nameof(companyService));

        private readonly IMapper<CompanyViewModel, CompanyModel> _companyMapper =
            companyMapper ?? throw new ArgumentNullException(nameof(companyMapper));


        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var internalResponse = await _companyService.GetCompanyAsync(id);
            
            return new JsonResult(_companyMapper.MapToView(internalResponse.Result ?? new CompanyModel()))
            {
                StatusCode = internalResponse.StatusCode
            };
        }
    }
}
