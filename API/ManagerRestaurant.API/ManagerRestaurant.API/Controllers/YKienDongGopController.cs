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
    public class YKienDongGopController : ControllerBase
    {
        private readonly DataContext _context;

        public YKienDongGopController(DataContext context)
        {
            _context = context;
        }

        // GET: api/YKienDongGop
        [HttpGet]
        public async Task<ActionResult<IEnumerable<YKienDongGop>>> GetYKienDongGop()
        {
            return await _context.YKienDongGop.ToListAsync();
        }

        // GET: api/YKienDongGop/5
        [HttpGet("{id}")]
        public async Task<ActionResult<YKienDongGop>> GetYKienDongGop(Guid id)
        {
            var yKienDongGop = await _context.YKienDongGop.FindAsync(id);

            if (yKienDongGop == null)
            {
                return NotFound();
            }

            return yKienDongGop;
        }

        // PUT: api/YKienDongGop/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutYKienDongGop(Guid id, YKienDongGop yKienDongGop)
        {
            if (id != yKienDongGop.Id)
            {
                return BadRequest();
            }

            _context.Entry(yKienDongGop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NoContent();
            }

            return NoContent();
        }

        // POST: api/YKienDongGop
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<YKienDongGop>> PostYKienDongGop(YKienDongGop yKienDongGop)
        {
            _context.YKienDongGop.Add(yKienDongGop);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetYKienDongGop", new { id = yKienDongGop.Id }, yKienDongGop);
        }

        // DELETE: api/YKienDongGop/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteYKienDongGop(Guid id)
        {
            var yKienDongGop = await _context.YKienDongGop.FindAsync(id);
            if (yKienDongGop == null)
            {
                return NotFound();
            }

            _context.YKienDongGop.Remove(yKienDongGop);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("filter")]
        public async Task<Responsive> GetFilterYKDG([FromQuery] string _filter)
        {
            try
            {

                var filter = JsonConvert.DeserializeObject<YKenFilter>(_filter);
                var query = from s in _context.YKienDongGop select s;
                if (filter.Id != Guid.Empty)
                {
                    query = query.Where((x) => x.Id == filter.Id);
                }
                if (filter.TextSearch != null && filter.TextSearch.Length > 0)
                {
                    query = query.Where((x) => x.NoiDung.Contains(filter.TextSearch.Trim()) || x.SoDienThoai.Contains(filter.TextSearch.Trim())||x.TenKH.Contains(filter.TextSearch.Trim()));
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

        class YKenFilter : BaseFilter
        { 
        }
    }
}
