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
    public class PhieuNhapVatTuController : ControllerBase
    {
        private readonly DataContext _context;

        public PhieuNhapVatTuController(DataContext context)
        {
            _context = context;
        }

        // GET: api/PhieuNhapVatTus
        [HttpGet]
        public async Task<Responsive> GetPhieuNhapVatTu()
        {
            Responsive res = new Responsive();
            try
            {
                List<PhieuNhapVatTuModel> data = new List<PhieuNhapVatTuModel>();
                var d = await _context.PhieuNhapVatTu.ToListAsync();
                foreach (var item in d)
                {
                    data.Add(CovenerPhieu(item));
                }
                res.Mess = "Get sussces";
                res.Data = data;
                res.Code = 200;
                return res;
            }
            catch (Exception ex)
            {
                res.Mess = ex.InnerException.Message;
                res.Data = null;
                res.Code = 500;
                return res;
            }
        }

        // GET: api/PhieuNhapVatTus
        [HttpGet("getbykieu/{kieu}")]
        public async Task<Responsive> GetPhieuNhapVatTuByKieu(string kieu)
        {
            Responsive res = new Responsive();
            try
            {
                List<PhieuNhapVatTuModel> data = new List<PhieuNhapVatTuModel>();
                var d = await _context.PhieuNhapVatTu.Where(x=>x.Kieu.Contains(kieu)).ToListAsync();
                foreach (var item in d)
                {
                    data.Add(CovenerPhieu(item));
                }
                res.Mess = "Get sussces";
                res.Data = data;
                res.Code = 200;
                return res;
            }
            catch (Exception ex)
            {
                res.Mess = ex.InnerException.Message;
                res.Data = null;
                res.Code = 500;
                return res;
            }
        }

        // GET: api/PhieuNhapVatTus/5
        [HttpGet("{id}")]
        public async Task<Responsive> GetPhieuNhapVatTu(Guid id)
        {
            var res = new Responsive();
            var item = await _context.PhieuNhapVatTu.FindAsync(id);
            if (item == null)
            {

                res.Code = 204;
                res.Mess = "not found";
                return res;
            }
            else
            {
                res.Code = 200;
                res.Mess = "get success";
                res.Data = CovenerPhieu(item);
            }
            return res;
        }

        PhieuNhapVatTuModel CovenerPhieu(PhieuNhapVatTu item)
        {
            PhieuNhapVatTuModel res = new PhieuNhapVatTuModel();
            res.Id = item.Id;
            res.Name = item.Name;
            res.Kieu = item.Kieu;
            res.NgayHoaDon = item.NgayHoaDon;
            res.MatHangs = item.MatHangs;
            res.HinhAnh = item.HinhAnh;
            res.GhiChu = item.GhiChu;
            res.TongSoTien = item.TongSoTien;
            res.CreatedByUserId = item.CreatedByUserId;
            res.CreatedByUserName = item.CreatedByUserName;
            res.CreatedOnDate = item.CreatedOnDate;
            res.LastModifiedByUserId = item.LastModifiedByUserId;
            res.LastModifiedByUserName = item.LastModifiedByUserName;
            return res;
        }

        // PUT: api/PhieuNhapVatTus/5
        [HttpPut("{id}")]
        public async Task<Responsive> PutPhieuNhapVatTu(Guid id, PhieuNhapVatTuUpdateModel item)
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
                var phieunhapvt = _context.PhieuNhapVatTu.Find(id);
                if (phieunhapvt != null)
                {
                    phieunhapvt.Name = item.Name;
                    phieunhapvt.Kieu = item.Kieu;
                    phieunhapvt.NgayHoaDon = item.NgayHoaDon;
                    phieunhapvt.MatHangs = item.MatHangs;
                    phieunhapvt.HinhAnh = item.HinhAnh;
                    phieunhapvt.GhiChu = item.GhiChu;
                    phieunhapvt.LastModifiedByUserId = item.LastModifiedByUserId;
                    phieunhapvt.LastModifiedByUserName = item.LastModifiedByUserName;
                    phieunhapvt.TongSoTien = item.TongSoTien;
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

        // POST: api/PhieuNhapVatTus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<Responsive> PostPhieuNhapVatTu(PhieuNhapVatTuCreateModel item)
        {
            try
            {
                var phieunhapvt = new PhieuNhapVatTu();
                phieunhapvt.Id = Guid.NewGuid();
                phieunhapvt.Name = item.Name;
                phieunhapvt.Kieu = item.Kieu;
                phieunhapvt.NgayHoaDon = item.NgayHoaDon;
                phieunhapvt.MatHangs = item.MatHangs;
                phieunhapvt.HinhAnh = item.HinhAnh;
                phieunhapvt.GhiChu = item.GhiChu;
                phieunhapvt.TongSoTien = item.TongSoTien;
                phieunhapvt.CreatedByUserId = item.CreatedByUserId;
                phieunhapvt.CreatedByUserName = item.CreatedByUserName;
                phieunhapvt.CreatedOnDate = item.CreatedOnDate;
                _context.PhieuNhapVatTu.Add(phieunhapvt);
                await _context.SaveChangesAsync();

                return new Responsive(200, "Thêm mới thành công", phieunhapvt);
            }
            catch (Exception ex)
            {
                return new Responsive(500, ex.InnerException.Message, null);
            }
        }

        // DELETE: api/PhieuNhapVatTus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhieuNhapVatTu(Guid id)
        {
            var phieuNhapVatTu = await _context.PhieuNhapVatTu.FindAsync(id);
            if (phieuNhapVatTu == null)
            {
                return NotFound();
            }

            _context.PhieuNhapVatTu.Remove(phieuNhapVatTu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhieuNhapVatTuExists(Guid id)
        {
            return _context.PhieuNhapVatTu.Any(e => e.Id == id);
        }
        [HttpGet("filter")]
        public async Task<Responsive> GetFilterPhieuNhapVT([FromQuery] string _filter)
        {
            try
            {

                var filter = JsonConvert.DeserializeObject<PhieuNhapVatTuFilter>(_filter);
                var query = from s in _context.PhieuNhapVatTu select s;
                if (filter.Id != Guid.Empty)
                {
                    query = query.Where((x) => x.Id == filter.Id);
                }
                if (filter.TextSearch != null && filter.TextSearch.Length > 0)
                {
                    query = query.Where((x) => x.Name.Contains(filter.TextSearch) || x.Kieu.Contains(filter.TextSearch));
                }
                if (filter.NgayHoaDon != null)
                {
                    query = query.Where((x) => x.CreatedOnDate.Equals(filter.NgayHoaDon));
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

        class PhieuNhapVatTuFilter : BaseFilter
        {
            public DateTime? NgayHoaDon { get; set; }
        }
    }
}
