using ArchiLibrary.controllers;
using ArchiLibrary.Params;
using ArchiLog.Data;
using ArchiLog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using System.Collections.Generic;

namespace ArchiLog.Controllers
{
    
    [Route("api/[controller]")]
    public class BrandsController : BaseController<ArchiLogDbContext, Brand>
    {
        public BrandsController(ArchiLogDbContext context):base(context)
        {
        }
    }
}
