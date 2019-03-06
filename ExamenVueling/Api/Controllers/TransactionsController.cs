using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Helpers;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : BaseController
    {
        private readonly ITransactionsService _serviceTransaction;

        public TransactionsController(ITransactionsService serviceTransaction)
        {
            _serviceTransaction = serviceTransaction;
        }

        //DEVUELVE TODAS LAS TRANSACCIONES
        [HttpGet]
        [Route("", Name = "GetTrans")]
        public async Task<IActionResult> Get()
        {
            var response = await _serviceTransaction.Get();
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