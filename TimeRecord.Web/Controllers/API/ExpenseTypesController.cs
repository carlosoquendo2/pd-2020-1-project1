using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeRecord.Web.Data;
using TimeRecord.Web.Data.Entities;

namespace TimeRecord.Web.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public ExpenseTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ExpenseTypes
        [HttpGet]
        public IEnumerable<ExpenseTypeEntity> GetExpenseTypes()
        {
            return _context.ExpenseTypes.Where(e => e.Active == true);
        }
    }
}