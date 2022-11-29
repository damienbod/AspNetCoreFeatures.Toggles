﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreFeatures.Toggles.Server.Controllers;

[ValidateAntiForgeryToken]
[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/[controller]")]
public class FeatureXApiController : ControllerBase
{
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new List<string> { "some data", "more data", "loads of data" };
    }
}
