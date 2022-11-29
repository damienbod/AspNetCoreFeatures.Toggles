using AspNetCoreFeatures.Toggles.Shared;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;

namespace AspNetCoreFeatures.Toggles.Server.Controllers;

[ValidateAntiForgeryToken]
[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/[controller]")]
public class FeatureXApiController : ControllerBase
{
    private IFeatureManager _featureManager;

    public FeatureXApiController(IFeatureManager featureManager)
    {
        _featureManager = featureManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var featureX = await _featureManager.IsEnabledAsync(Features.FEATUREX);

        if(featureX)
        {
            return Ok(new List<string> { "some data", "more data", "loads of data" });
        }

        return NotFound();
    }
}
