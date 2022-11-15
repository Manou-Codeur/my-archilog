using ArchiLibrary.controllers;
using ArchiLog.Data;
using ArchiLog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArchiLog.Controllers.V2
{

    [ApiVersion("2.0")]
    public class BrandsController : BaseController<ArchiLogDbContext, Brand>
    {
        public BrandsController(ArchiLogDbContext context, ILogger<BaseController<ArchiLogDbContext, Brand>> logger) : base(context, logger)
        {
        }

        [HttpGet()]
        override public async Task<IEnumerable<Brand>> GetAll()
        {
            return await _context.Brands.Where(x => x.Active).Skip(4).ToListAsync();
        }


    }
}