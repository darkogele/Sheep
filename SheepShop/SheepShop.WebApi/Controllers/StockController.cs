using Newtonsoft.Json;
using SheepShop.Logic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SheepShop.WebApi.Controllers
{
    public class StockController : ApiController
    {
        //SHEEP-2
        [HttpGet]
        [Route("sheep-shop/stock/{time}")]
        public HttpResponseMessage Stock(int time)
        {
            var logic = new SheepLogic();


            var content = new ObjectContent<StockInfo>(logic.GetStockInfo(ConfigurationManager.AppSettings["stockFileLocation"].ToString(), time), Configuration.Formatters.JsonFormatter);

            return new HttpResponseMessage()
            {
                Content = content
            };
        }

        [HttpGet]
        [Route("sheep-shop/herd/{time}")]
        public HttpResponseMessage Herd(int time)
        {
            var logic = new SheepLogic();

            var content = new ObjectContent<object>(logic.GetHerdLastShavedInfo(time), new System.Net.Http.Formatting.JsonMediaTypeFormatter() { SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings { Formatting = Newtonsoft.Json.Formatting.Indented } });


            return new HttpResponseMessage()
            {
                Content = content
            };
        }
    }
}
