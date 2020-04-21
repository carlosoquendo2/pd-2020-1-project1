using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeRecord.Common.Enums;

namespace TimeRecord.Web.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        [HttpGet]
        public List<String> GetExpenseTypes()
        {
            Array arr = Enum.GetValues(typeof(CurrencyType));
            List<string> lstCurrencies = new List<string>(arr.Length);
            for (int i = 0; i < arr.Length; i++)
            {
                lstCurrencies.Add(arr.GetValue(i).ToString());
            }
            return lstCurrencies;
        }
    }
}