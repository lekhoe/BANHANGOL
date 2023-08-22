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
    public class TheLoaiMonAnController : ControllerBase
    {
        private readonly DataContext _context;

        public TheLoaiMonAnController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TheLoaiDoAn
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TheLoaiDoAn>>> GetTheLoaiMonAn()
        {
            return await _context.TheLoaiDoAn.ToListAsync();
        }

        // GET: api/TheLoaiDoAn/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TheLoaiDoAn>> GetTheLoaiMonAn(Guid id)
        {
            var TheLoaiDoAn = await _context.TheLoaiDoAn.FindAsync(id);

            if (TheLoaiDoAn == null)
            {
                return NotFound();
            }

            return TheLoaiDoAn;
        }

        // PUT: api/TheLoaiDoAn/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTheLoaiMonAn(Guid id, TheLoaiDoAn TheLoaiDoAn)
        {
            if (id != TheLoaiDoAn.Id)
            {
                return BadRequest();
            }

            _context.Entry(TheLoaiDoAn).State = EntityState.Modified;

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

        // POST: api/TheLoaiDoAn
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TheLoaiDoAn>> PostTheLoaiMonAn(TheLoaiDoAn TheLoaiDoAn)
        {
            _context.TheLoaiDoAn.Add(TheLoaiDoAn);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTheLoaiMonAn", new { id = TheLoaiDoAn.Id }, TheLoaiDoAn);
        }

        // DELETE: api/TheLoaiDoAn/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTheLoaiMonAn(Guid id)
        {
            var TheLoaiDoAn = await _context.TheLoaiDoAn.FindAsync(id);
            if (TheLoaiDoAn == null)
            {
                return NotFound();
            }

            _context.TheLoaiDoAn.Remove(TheLoaiDoAn);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("filter")]
        public async Task<Responsive> GetFilterTLDoAn([FromQuery] string _filter)
        {
            try
            {

                var filter = JsonConvert.DeserializeObject<TheloaiDoAwnFilter>(_filter);
                var query = from s in _context.TheLoaiDoAn select s;
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

        class TheloaiDoAwnFilter : BaseFilter
        {
        }
    }
}
