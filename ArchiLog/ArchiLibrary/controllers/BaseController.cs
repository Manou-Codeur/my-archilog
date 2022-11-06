using ArchiLibrary.Pagination;
using ArchiLibrary.Data;
using ArchiLibrary.Models;
using ArchiLibrary.Params;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiLibrary.controllers
{
    [ApiController]
    public abstract class BaseController<TContext, TModel> : ControllerBase where TContext : BaseDbContext where TModel : BaseModel
    {
        protected readonly TContext _context;

        public BaseController(TContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TModel>>> GetAll([FromQuery] BaseParams param)
        {
            const int maxRange = 3;

            var query = await _context.Set<TModel>().Where(x => x.Active).ToListAsync();

            if (param.Range == null)
            {
                return query;
            }

            //if param exists
            string[] table = param.Range.Split('-');
            int start = int.Parse(table[0]);
            int end = int.Parse(table[1]);

            if (start == 0) start = 1;

            int toTake = (end - start) + 1;
            int toSkip = start - 1;

            var unit = Request.Path.ToString().Split('/')[2];

            if (toTake > maxRange)
            {
                return BadRequest($"Max range is {maxRange} {unit}, but you asked for {toTake}.");
            }

            var obj = new PaginationUtil(query.Count, toTake, start, end, unit);

            //http headers
            Response.Headers.Add("Content-Range", param.Range + "/" + query.Count);
            Response.Headers.Add("Accept-Range", unit + " " + maxRange);
            Response.Headers.Add("Links", $"next: {obj.next}, prev: {obj.prev}, first: {obj.first}, last: {obj.last}");


            return query.Skip(toSkip).Take(toTake).ToList();
        }

        [HttpGet("{id}")]// /api/{item}/3
        public async Task<ActionResult<TModel>> GetById([FromRoute] int id)
        {
            var item = await _context.Set<TModel>().SingleOrDefaultAsync(x => x.ID == id);
            if (item == null || !item.Active)
                return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<IActionResult> PostItem([FromBody] TModel item)
        {
            item.Active = true;
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = item.ID }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TModel>> PutItem([FromRoute] int id, [FromBody] TModel item)
        {
            if (id != item.ID)
                return BadRequest();
            if (!ItemExists(id))
                return NotFound();

            //_context.Entry(item).State = EntityState.Modified;
            _context.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TModel>> DeleteItem([FromRoute] int id)
        {
            var item = await _context.Set<TModel>().FindAsync(id);
            if (item == null)
                return BadRequest();
            //_context.Entry(item).State = EntityState.Deleted;
            _context.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        private bool ItemExists(int id)
        {
            return _context.Set<TModel>().Any(x => x.ID == id);
        }
    }
}
