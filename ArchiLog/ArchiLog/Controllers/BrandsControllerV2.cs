using ArchiLibrary.controllers;
using ArchiLog.Data;
using ArchiLog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArchiLog.Controllers
{
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiVersion("2.0")]
    public class BrandsControllerV2 : BaseController<ArchiLogDbContext, Brand>
    {
        public BrandsControllerV2(ArchiLogDbContext context) : base(context)
        {
        }

        [HttpGet()]
        public async Task<IEnumerable<Brand>> GetAll()
        {
            return await _context.Brands.Where(x => x.Active).Skip(1).ToListAsync();
        }


    }
}