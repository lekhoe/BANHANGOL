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
    public class UuDaiController : ControllerBase
    {
        private readonly DataContext _context;

        public UuDaiController(DataContext context)
        {
            _context = context;
        }

        // GET: api/UuDai
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UuDai>>> GetUuDai()
        {
            return await _context.UuDai.ToListAsync();
        }

        // GET: api/UuDai/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UuDai>> GetUuDai(Guid id)
        {
            var uuDai = await _context.UuDai.FindAsync(id);

            if (uuDai == null)
            {
                return NotFound();
            }

            return uuDai;
        }

        // PUT: api/UuDai/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<Responsive> PutUuDai(Guid id, UuDaiUpdateModule item)
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
                var uudai = _context.UuDai.Find(id);
                if (uudai != null)
                { 
                    uudai.Name = item.Name;
                    uudai.Anh = item.Anh;
                    uudai.NoiDung = item.NoiDung;
                    uudai.GiaTri = item.GiaTri;
                    uudai.LoaiUuDai = item.LoaiUuDai;
                    uudai.DonViTinh = item.DonViTinh;
                    uudai.TrangThai = item.TrangThai;
                    uudai.IdDoAn = item.IdDoAn;
                    uudai.LastModifiedByUserId = item.LastModifiedByUserId;
                    uudai.LastModifiedByUserName = item.LastModifiedByUserName;
                    await _context.SaveChangesAsync();
                    res.Code = 200;
                    res.Mess = "Update success";
                    res.Data = uudai;
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

        // POST: api/UuDai
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<Responsive> PostUuDai(UuDaiCreateModule item)
        {
            try
            {
                var uudai = new UuDai();
                uudai.Id = Guid.NewGuid();
                uudai.Name = item.Name;
                uudai.Anh = item.Anh;
                uudai.NoiDung = item.NoiDung;
                uudai.GiaTri = item.GiaTri;
                uudai.LoaiUuDai = item.LoaiUuDai;
                uudai.DonViTinh = item.DonViTinh;
                uudai.TrangThai = item.TrangThai;
                uudai.IdDoAn = item.IdDoAn;
                uudai.CreatedByUserId = item.CreatedByUserId;
                uudai.CreatedByUserName = item.CreatedByUserName;
                uudai.CreatedOnDate = item.CreatedOnDate;

                _context.UuDai.Add(uudai);
                await _context.SaveChangesAsync();

                return new Responsive(200, "Thêm mới thành công", uudai);
            }
            catch (Exception ex)
            {
                return new Responsive(500, ex.InnerException.Message, null);
            }

        }

        // DELETE: api/UuDai/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUuDai(Guid id)
        {
            var uuDai = await _context.UuDai.FindAsync(id);
            if (uuDai == null)
            {
                return NotFound();
            }

            _context.UuDai.Remove(uuDai);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("filter")]
        public async Task<Responsive> GetFilterUuDai([FromQuery] string _filter)
        {
            try
            {

                var filter = JsonConvert.DeserializeObject<UuDaiFilter>(_filter);
                var query = from s in _context.UuDai select s;
                if (filter.Id != Guid.Empty)
                {
                    query = query.Where((x) => x.Id == filter.Id);
                }
                if (filter.TextSearch != null && filter.TextSearch.Length > 0)
                {
                    query = query.Where((x) => x.Name.Contains(filter.TextSearch));
                }
                if (filter.TrangThai != null)
                {
                    query = query.Where(x=> x.TrangThai == filter.TrangThai);
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

        class UuDaiFilter : BaseFilter
        {
            public int? TrangThai { get; set; }
        }
    }
}
