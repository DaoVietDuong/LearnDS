using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using D.MoveOn.Common.Mvc;

namespace D.MoveOn.Order.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly IServiceInfor _serviceInfor;

        public DemoController(IServiceInfor serviceInfor)
        {
            this._serviceInfor = serviceInfor;
        }

        [HttpGet("GetFake/{id}")]
        public IActionResult GetFake(int id) {

            var resultFake = new List<string>(){
                "data1a and" + id.ToString(),
                "data2  and" + _serviceInfor.Id
            };

            return Ok(resultFake);
        }
    }
}