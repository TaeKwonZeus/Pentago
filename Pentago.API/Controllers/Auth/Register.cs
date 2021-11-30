using Microsoft.AspNetCore.Mvc;

namespace Pentago.API.Controllers.Auth
{
    [Route("[controller]")]
    public class Register : Controller
    {
        [HttpPost]
        public void Post()
        {
            HttpContext.Response.StatusCode = 200;
        }
    }
}