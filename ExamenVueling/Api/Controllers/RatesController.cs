using Domain.Helpers;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : BaseController
    {
        private readonly IRatesService _serviceRates;

        public RatesController(IRatesService serviceRates)
        {
            _serviceRates = serviceRates;
        }

        // GET api/values
        [HttpGet]
        [Route("", Name = "GetRates")]
        public async Task<IActionResult> Get()
        {
            var response = await _serviceRates.Get();
            if (response.ResponseH.Result != ResultTypeHelper.ResultMsg.OK)
            {
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(response.ResponseH.ErrText)
                };
                return BadRequest(message);
            }
            else
            {
                return Ok(response.DataH);
            }
        }
    }
}