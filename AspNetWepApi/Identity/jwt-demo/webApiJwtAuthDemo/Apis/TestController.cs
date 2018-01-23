using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace webApiJwtAuthDemo
{
    public class TestController : ApiController
    {
        [HttpGet, ActionName("get")]
        public string Get()
        {
            return "success";
        }
    }
}
