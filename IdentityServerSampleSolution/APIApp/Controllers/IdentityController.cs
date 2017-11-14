using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using IdentityServer4.AccessTokenValidation;

namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    public class IdentityController : Controller
    {
        // GET api/values
        [HttpGet]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        public IActionResult Get()
        {
           // var caller = User as ClaimsPrincipal;
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
