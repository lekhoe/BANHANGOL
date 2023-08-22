using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infratructure;
using Infratructure.Datatables;
using ManagerRestaurant.API.Models;
using Newtonsoft.Json;

namespace ManagerRestaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhuVucController : ControllerBase
    {
        private readonly DataContext _context;

        public KhuVucController(DataContext context)
        {
            _context = context;
        }

        // GET: api/KhuVuc
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhuVuc>>> GetKhuVuc()
        {
            return await _context.KhuVuc.ToListAsync();
        }

        // GET: api/KhuVuc/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KhuVuc>> GetKhuVuc(Guid id)
        {
            var khuVuc = await _context.KhuVuc.FindAsync(id);

            if (khuVuc == null)
            {
                return NotFound();
            }

            return khuVuc;
        }

        // PUT: api/KhuVuc/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhuVuc(Guid id, KhuVuc khuVuc)
        {
            if (id != khuVuc.Id)
            {
                return BadRequest();
            }

            _context.Entry(khuVuc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhuVucExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/KhuVuc
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KhuVuc>> PostKhuVuc(KhuVuc khuVuc)
        {
            _context.KhuVuc.Add(khuVuc);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKhuVuc", new { id = khuVuc.Id }, khuVuc);
        }

        // DELETE: api/KhuVuc/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhuVuc(Guid id)
        {
            var khuVuc = await _context.KhuVuc.FindAsync(id);
            if (khuVuc == null)
            {
                return NotFound();
            }

            _context.KhuVuc.Remove(khuVuc);
            await _context.SaveChangesAsync();

            return NoContent();
        } 
        [HttpGet("filter")]
        public async Task<Responsive> GetFilterKhuVuc([FromQuery] string _filter)
        {
            try
            {

                var filter = JsonConvert.DeserializeObject<KhuVucFilter>(_filter);
                var query = from s in _context.KhuVuc select s;
                if (filter.Id != Guid.Empty)
                {
                    query = query.Where((x) => x.Id == filter.Id);
                }
                if (filter.TextSearch != null && filter.TextSearch.Length > 0)
                {
                    query = query.Where((x) => x.Name.Contains(filter.TextSearch));
                }

                if (filter.PageNumber > 0 && filter.PageSize > 0)
                {
                    query = query.Skip(filter.PageSize * (filter.PageNumber - 1)).Take(filter.PageSize);
                }

                var data = await query.ToListAsync();

                var mes = "";
                if (data.Count == 0)
                {
                    mes = "Not data";
                }
                else
                {
                    mes = "Get success";
                }

                var res = new Responsive(200, mes, data);
                return res;
            }
            catch (Exception err)
            {
                var res = new Responsive(500, err.Message, err.ToString());
                return res;
            }
        }

        private bool KhuVucExists(Guid id)
        {
            return _context.KhuVuc.Any(e => e.Id == id);
        }

        class KhuVucFilter : BaseFilter
        { 
        }
    }
}
