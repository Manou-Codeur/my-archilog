using ArchiLibrary.controllers;
using ArchiLog.Data;
using ArchiLog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArchiLog.Controllers.V1
{
    [ApiVersion("1.0")]
    public class BrandsController : BaseController<ArchiLogDbContext, Brand>
    {
        public BrandsController(ArchiLogDbContext context) : base(context)
        {
        }

    }
}
