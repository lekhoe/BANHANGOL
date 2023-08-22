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
    public class BarsController : ControllerBase
    {
        private readonly DataContext _context;

        public BarsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Bars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bar>>> GetBar()
        {
            return await _context.Bar.ToListAsync();
        }

        // GET: api/Bars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bar>> GetBar(Guid id)
        {
            var bar = await _context.Bar.FindAsync(id);

            if (bar == null)
            {
                return NotFound();
            }

            return bar;
        }

        // PUT: api/Bars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<Responsive> PutBar(Guid id, Bar item)
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
                var bar = _context.Bar.Find(id);
                if (bar != null)
                {
                    bar.MaMatHang = item.MaMatHang;
                    bar.TenMatHang = item.TenMatHang;
                    bar.NhomMatHang = item.NhomMatHang;
                    bar.SoLuong = item.SoLuong;
                    bar.DonVi = item.DonVi;
                    bar.DonGia = item.DonGia;
                    bar.ThanhTien = item.ThanhTien;
                    bar.CreatedByUserId = item.CreatedByUserId;
                    bar.CreatedByUserName = item.CreatedByUserName;
                    bar.CreatedOnDate = item.CreatedOnDate;
                }
                else
                {
                    res.Code = 204;
                    res.Mess = "Item not exist";
                    return res;
                }
                await _context.SaveChangesAsync();
                res.Code = 200;
                res.Mess = "Update success";
                return res;
            }
            catch (Exception ex)
            {
                res.Code = 500;
                res.Mess = ex.InnerException.Message;
                return res;

            }
        }

        // POST: api/Bars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<Responsive> PostBar(BarCreateModule item)
        {
            try
            {
                var obj = _context.Bar.Where(x => x.MaMatHang == item.MaMatHang).FirstOrDefaultAsync().Result;
                if (obj != null)
                {
                    obj.Id = Guid.NewGuid();
                    obj.MaMatHang = item.MaMatHang;
                    obj.TenMatHang = item.TenMatHang;
                    obj.NhomMatHang = item.NhomMatHang;
                    obj.SoLuong = item.SoLuong;
                    obj.DonVi = item.DonVi;
                    obj.DonGia = item.DonGia;
                    obj.ThanhTien = item.ThanhTien;
                    obj.CreatedByUserId = item.CreatedByUserId;
                    obj.CreatedByUserName = item.CreatedByUserName;
                    obj.CreatedOnDate = item.CreatedOnDate;
                    _context.Bar.Add(obj);
                    await _context.SaveChangesAsync();
                    return new Responsive(200, "Cập nhật thành công", obj);
                }
                else
                {
                    var bar = new Bar();
                    bar.Id = Guid.NewGuid();
                    bar.MaMatHang = item.MaMatHang;
                    bar.TenMatHang = item.TenMatHang;
                    bar.NhomMatHang = item.NhomMatHang;
                    bar.SoLuong = item.SoLuong;
                    bar.DonVi = item.DonVi;
                    bar.DonGia = item.DonGia;
                    bar.ThanhTien = item.ThanhTien;
                    bar.CreatedByUserId = item.CreatedByUserId;
                    bar.CreatedByUserName = item.CreatedByUserName;
                    bar.CreatedOnDate = item.CreatedOnDate;
                    _context.Bar.Add(bar);
                    await _context.SaveChangesAsync();
                    return new Responsive(200, "Thêm mới thành công", bar);
                }
                
            }
            catch (Exception ex)
            {
                return new Responsive(500, ex.InnerException.Message, null);
            }
        }

        // DELETE: api/Bars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBar(Guid id)
        {
            var bar = await _context.Bar.FindAsync(id);
            if (bar == null)
            {
                return NotFound();
            }

            _context.Bar.Remove(bar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BarExists(Guid id)
        {
            return _context.Bar.Any(e => e.Id == id);
        }
        [HttpGet("filter")]
        public async Task<Responsive> GetFilterBar([FromQuery] string _filter)
        {
            try
            {

                var filter = JsonConvert.DeserializeObject<BarFilter>(_filter);
                var query = from s in _context.Bar select s;
                if (filter.Id != Guid.Empty)
                {
                    query = query.Where((x) => x.Id == filter.Id);
                }
                if (filter.TextSearch != null && filter.TextSearch.Length > 0)
                {
                    query = query.Where((x) => x.TenMatHang.Contains(filter.TextSearch));
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

        class BarFilter : BaseFilter
        {
        }
    }
}
