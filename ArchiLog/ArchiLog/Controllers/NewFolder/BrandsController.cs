using ArchiLibrary.controllers;
using ArchiLog.Data;
using ArchiLog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArchiLog.Controllers.V2
{
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiVersion("2.0")]
    public class BrandsController : BaseController<ArchiLogDbContext, Brand>
    {
        public BrandsController(ArchiLogDbContext context) : base(context)
        {
        }

        [HttpGet()]
        public async Task<IEnumerable<Brand>> GetAll()
        {
            return await _context.Brands.Where(x => x.Active).Skip(4).ToListAsync();
        }


    }
}