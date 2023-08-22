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
    public class ChiTieuTrongNgayController : ControllerBase
    {
        private readonly DataContext _context;

        public ChiTieuTrongNgayController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ChiTieuTrongNgay
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChiTieuTrongNgay>>> GetChiTieuTrongNgay()
        {
            return await _context.ChiTieuTrongNgay.ToListAsync();
        }

        // GET: api/ChiTieuTrongNgay/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChiTieuTrongNgay>> GetChiTieuTrongNgay(Guid id)
        {
            var chiTieuTrongNgay = await _context.ChiTieuTrongNgay.FindAsync(id);

            if (chiTieuTrongNgay == null)
            {
                return NotFound();
            }

            return chiTieuTrongNgay;
        }

        // PUT: api/ChiTieuTrongNgay/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<Responsive> PutChiTieuTrongNgay(Guid id, ChiTieuTrongNgayUpdateModel item)
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
                var doAn = _context.ChiTieuTrongNgay.Find(id);
                if (doAn != null)
                {
                    doAn.Name = item.Name;
                    doAn.MatHang = item.MatHang;
                    doAn.Anh = item.Anh;
                    doAn.TongSoTien = item.TongSoTien;
                    doAn.TrangThaiHienTai = item.TrangThaiHienTai;
                    doAn.GhiChu = item.GhiChu;
                    doAn.NgayHoaDon = item.NgayHoaDon;
                    doAn.ThoiGianKeToanDuyet = item.ThoiGianKeToanDuyet;
                    doAn.NameKeToanDuyet = item.NameKeToanDuyet;
                    doAn.ThoiGianQuanLyDuyet = item.ThoiGianQuanLyDuyet;
                    doAn.NameQuanLyDuyet = item.NameQuanLyDuyet;
                    doAn.LastModifiedByUserId = item.LastModifiedByUserId;
                    doAn.LastModifiedByUserName = item.LastModifiedByUserName;
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

        // POST: api/ChiTieuTrongNgay
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<Responsive> PostChiTieuTrongNgay(ChiTieuTrongNgayCreateModule item)
        {
            try
            {
                var chiTieuTrongNgay = new ChiTieuTrongNgay();
                chiTieuTrongNgay.Name = item.Name;
                chiTieuTrongNgay.MatHang = item.MatHang;
                chiTieuTrongNgay.Anh = item.Anh;
                chiTieuTrongNgay.TongSoTien = item.TongSoTien;
                chiTieuTrongNgay.TrangThaiHienTai = item.TrangThaiHienTai;
                chiTieuTrongNgay.GhiChu = item.GhiChu;
                chiTieuTrongNgay.NgayHoaDon = item.NgayHoaDon;
                chiTieuTrongNgay.ThoiGianKeToanDuyet = item.ThoiGianKeToanDuyet;
                chiTieuTrongNgay.NameKeToanDuyet = item.NameKeToanDuyet;
                chiTieuTrongNgay.ThoiGianQuanLyDuyet = item.ThoiGianQuanLyDuyet;
                chiTieuTrongNgay.NameQuanLyDuyet = item.NameQuanLyDuyet;
                chiTieuTrongNgay.CreatedByUserId = item.CreatedByUserId;
                chiTieuTrongNgay.CreatedByUserName = item.CreatedByUserName;
                chiTieuTrongNgay.CreatedOnDate = item.CreatedOnDate;
                _context.ChiTieuTrongNgay.Add(chiTieuTrongNgay);
                await _context.SaveChangesAsync();

                return new Responsive(200, "Thêm mới thành công", chiTieuTrongNgay);
            }
            catch (Exception ex)
            {
                return new Responsive(500, ex.InnerException.Message, null);
            }
        }

        // DELETE: api/ChiTieuTrongNgay/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChiTieuTrongNgay(Guid id)
        {
            var chiTieuTrongNgay = await _context.ChiTieuTrongNgay.FindAsync(id);
            if (chiTieuTrongNgay == null)
            {
                return NotFound();
            }

            _context.ChiTieuTrongNgay.Remove(chiTieuTrongNgay);
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

                var filter = JsonConvert.DeserializeObject<ChiTieuTrongNgayFilter>(_filter);
                var query = from s in _context.ChiTieuTrongNgay select s;
                if (filter.Id != Guid.Empty)
                {
                    query = query.Where((x) => x.Id == filter.Id);
                }
                if (filter.TextSearch.Length > 0)
                {
                    query = query.Where((x) => x.Name.Contains(filter.TextSearch));
                }
                if (filter.NgayHoaDon != null)
                {
                    query = query.Where((x) => x.NgayHoaDon.Equals(filter.NgayHoaDon));
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
        class ChiTieuTrongNgayFilter : BaseFilter
        {
            public DateTime? NgayHoaDon { get; set; }
        }
    }
   
}
