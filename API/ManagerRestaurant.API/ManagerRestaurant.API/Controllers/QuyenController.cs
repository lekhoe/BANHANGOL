using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infratructure;
using ManagerRestaurant.API.Infratructure.Datatables;
using ManagerRestaurant.API.Models;
using Newtonsoft.Json;

namespace ManagerRestaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuyenController : ControllerBase
    {
        private readonly DataContext _context;

        public QuyenController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Quyen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quyen>>> GetQuyen()
        {
            return await _context.Quyen.ToListAsync();
        }

        // GET: api/Quyen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quyen>> GetQuyen(Guid id)
        {
            var quyen = await _context.Quyen.FindAsync(id);

            if (quyen == null)
            {
                return NotFound();
            }

            return quyen;
        }

        // PUT: api/Quyen/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<Responsive> PutQuyen(Guid id, QuyenUpdateModel item)
        {
            var res = new Responsive();
            if (id != item.Id)
            {
                res.Code = 204;
                res.Mess = "Invalid data";
                return res;
            }
            try
            {
                var quyen = _context.Quyen.Find(id);
                if (quyen != null)
                {
                    quyen.Code = item.Code;
                    quyen.Name = item.Name;
                    quyen.LastModifiedByUserId = item.LastModifiedByUserId;
                    quyen.LastModifiedByUserName = item.LastModifiedByUserName;
                    await _context.SaveChangesAsync();
                    res.Code = 200;
                    res.Mess = "Update success";
                    return res;
                }
                else
                {
                    res.Code = 204;
                    res.Mess = "Item not exist";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Code = 500;
                res.Mess = ex.InnerException.Message;
                return res;
            }
        }

        // POST: api/Quyen
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<Responsive> PostQuyen(QuyenCreateModel item)
        {

            try
            {
                //conver
                var quyen = new Quyen();
                quyen.Code = item.Code;
                quyen.Name = item.Name;
                quyen.CreatedByUserId = item.CreatedByUserId;
                quyen.CreatedByUserName = item.CreatedByUserName;
                quyen.CreatedOnDate = item.CreatedOnDate;

                _context.Quyen.Add(quyen);
                await _context.SaveChangesAsync();

                return new Responsive(200, "Thêm mới thành công", quyen);
            }
            catch (Exception ex)
            {
                return new Responsive(500, ex.InnerException.Message, null);
            }
        }

        // DELETE: api/Quyen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuyen(Guid id)
        {
            var quyen = await _context.Quyen.FindAsync(id);
            if (quyen == null)
            {
                return NotFound();
            }

            _context.Quyen.Remove(quyen);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // POST: api/DoAn
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpGet("filter")]
        public async Task<Responsive> GetFilterDoAn([FromQuery] string _filter)
        {
            try
            {

                var filter = JsonConvert.DeserializeObject<QuyenFilter>(_filter);
                var query = from s in _context.Quyen select s;
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
        class QuyenFilter : BaseFilter
        { 
        }
    }
}
