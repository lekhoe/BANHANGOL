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
    public class DatBanController : ControllerBase
    {
        private readonly DataContext _context;

        public DatBanController(DataContext context)
        {
            _context = context;
        }

        // GET: api/DatBan
        [HttpGet]
        public async Task<Responsive> GetDatBan()
        {
            Responsive res = new Responsive();
            try
            {
                List<DatBanModel> data = new List<DatBanModel>();
                var d = await _context.DatBan.ToListAsync();
                foreach (var item in d)
                {
                    data.Add(new DatBanModel
                    {
                        Id = item.Id,
                        IdBan = item.IdBan.Value,
                        KhachHang = await (from s in _context.KhachHang
                                           where s.Id == item.MaKhachHang
                                           select new KhachHangModel
                                           {
                                               Id = s.Id,
                                               Name = s.Name,
                                               SoDienThoai = s.SoDienThoai
                                           }).FirstOrDefaultAsync(),
                        GioDen = item.GioDen.Value,
                        ThoiGian = item.ThoiGian,
                        SoNguoiLon = item.SoNguoiLon,
                        SoTreEm = item.SoTreEm,
                        GhiChu = item.GhiChu,
                        TrangThai = item.TrangThai,
                        CreatedByUserId = item.CreatedByUserId,
                        CreatedByUserName = item.CreatedByUserName,
                        CreatedOnDate = item.CreatedOnDate,
                        LastModifiedByUserId = item.LastModifiedByUserId,
                        LastModifiedByUserName = item.LastModifiedByUserName

                    });
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

        // GET: api/DatBan/5
        [HttpGet("{id}")]
        public async Task<Responsive> GetDatBan(Guid id)
        {
            var res = new Responsive();
            var item = await _context.DatBan.FindAsync(id);
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
                res.Data = new DatBanModel
                {
                    Id = item.Id,
                    IdBan = item.IdBan == null ? Guid.Empty : item.IdBan.Value,
                    KhachHang = await (from s in _context.KhachHang
                                       where s.Id == item.MaKhachHang
                                       select new KhachHangModel
                                       {
                                           Id = s.Id,
                                           Name = s.Name,
                                           SoDienThoai = s.SoDienThoai
                                       }).FirstOrDefaultAsync(),
                    GioDen = item.GioDen.Value,
                    ThoiGian = item.ThoiGian,
                    SoNguoiLon = item.SoNguoiLon,
                    SoTreEm = item.SoTreEm,
                    GhiChu = item.GhiChu,
                    TrangThai = item.TrangThai,
                    CreatedByUserId = item.CreatedByUserId,
                    CreatedByUserName = item.CreatedByUserName,
                    CreatedOnDate = item.CreatedOnDate,
                    LastModifiedByUserId = item.LastModifiedByUserId,
                    LastModifiedByUserName = item.LastModifiedByUserName,

                };
            }
            return res;
        }

        // PUT: api/DatBan/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<Responsive> PutDatBan(Guid id, DatBanUpdateModel item)
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
                var datBan = _context.DatBan.Find(id);
                if (datBan != null)
                {
                    datBan.Id = item.Id;
                    datBan.IdBan = item.IdBan;
                    Guid idkh = Guid.Empty;
                    if (item.IdBan!= Guid.Empty)
                    {
                        var ban = _context.Ban.Find(item.IdBan);
                        if (ban == null)
                        {
                            res.Code = 204;
                            res.Mess = "IdBan not exist";
                            return res;
                        }
                    }
                    if (item.MaKhachHang!= Guid.Empty)
                    {
                        var kh = _context.KhachHang.Find(item.MaKhachHang);
                        if (kh == null)
                        {
                            var khachhang = new KhachHang();
                            khachhang.Id = Guid.NewGuid();
                            khachhang.Name = item.TenKhachHang;
                            khachhang.SoDienThoai = item.SoDienThoai;
                            khachhang.CreatedByUserId = Guid.Empty;
                            khachhang.CreatedByUserName = "";
                            khachhang.CreatedOnDate = DateTime.Now;
                            //create new khach hang
                            idkh = khachhang.Id;
                            _context.KhachHang.Add(khachhang);
                        }
                        else
                        {
                            idkh = kh.Id;
                        }
                    }
                    
                    datBan.MaKhachHang = idkh;
                    datBan.TenKhachHang = item.TenKhachHang;
                    datBan.GioDen = item.GioDen;
                    datBan.ThoiGian = item.ThoiGian;
                    datBan.SoNguoiLon = item.SoNguoiLon;
                    datBan.SoTreEm = item.SoTreEm;
                    datBan.GhiChu = item.GhiChu;
                    datBan.LastModifiedByUserId = item.LastModifiedByUserId;
                    datBan.LastModifiedByUserName = item.LastModifiedByUserName;

                    if (item.IdBan != null && item.IdBan!=Guid.Empty)
                    {
                        var ban = await _context.Ban.FindAsync(item.IdBan);
                        if (ban==null)
                        {
                            res.Mess = "Id bàn không hợp lệ";
                            res.Data = null;
                            return res;
                        }
                        else
                        {
                            ban.TrangThai = 1;
                            datBan.IdBan = item.IdBan;
                            datBan.TrangThai = item.TrangThai;

                        }
                        
                    }
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

        [HttpPost]
        public async Task<Responsive> PostDatBan(DatBanCreateModel item)
        {
            try
            {
                Responsive res = new Responsive();
                if (item.SoDienThoai == null)
                {
                    return new Responsive(500, "Dữ liệu không hợp lệ", null);
                }
                var idKH = Guid.Empty;
                if (item.IdBan != Guid.Empty)
                {
                    var ban = _context.Ban.Find(item.IdBan);
                    if (ban == null)
                    {
                        res.Code = 204;
                        res.Mess = "IdBan not exist";
                        return res;
                    }
                }
                var data = (from s in _context.KhachHang where s.SoDienThoai == item.SoDienThoai select s).FirstOrDefault();
                if (data == null)
                {
                    var khachhang = new KhachHang();
                    khachhang.Id = Guid.NewGuid();
                    khachhang.Name = item.TenKhachHang;
                    khachhang.SoDienThoai = item.SoDienThoai;
                    khachhang.CreatedByUserId = Guid.Empty;
                    khachhang.CreatedByUserName = "";
                    khachhang.CreatedOnDate = DateTime.Now;
                    idKH = khachhang.Id;
                    //create new khach hang
                    _context.KhachHang.Add(khachhang);
                }
                //Tạo mới trường dữ liệu

                DatBan datBan = new DatBan();
                datBan.Id = Guid.NewGuid();
                datBan.MaKhachHang = (idKH == Guid.Empty) ? data.Id: idKH;
                datBan.TenKhachHang = item.TenKhachHang;
                datBan.GioDen = item.GioDen;
                datBan.ThoiGian = item.ThoiGian;
                datBan.SoNguoiLon = item.SoNguoiLon;
                datBan.IdBan = item.IdBan;
                datBan.SoTreEm = item.SoTreEm;
                datBan.GhiChu = item.GhiChu;
                datBan.TrangThai = 0;
                datBan.CreatedByUserId = idKH;
                datBan.CreatedByUserName = item.TenKhachHang;
                datBan.CreatedOnDate = DateTime.Now;
                datBan.LastModifiedByUserId = Guid.Empty;
                datBan.LastModifiedByUserName = "";
                _context.DatBan.Add(datBan);

                var status = await _context.SaveChangesAsync();
                return new Responsive(200, "Create success", datBan);

            }
            catch (Exception ex)
            {
                return new Responsive(500, ex.InnerException.Message, null);
            }
        }

        // DELETE: api/DatBan/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDatBan(Guid id)
        {
            var datBan = await _context.DatBan.FindAsync(id);
            if (datBan == null)
            {
                return NotFound();
            }

            _context.DatBan.Remove(datBan);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // POST: api/DoAn
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpGet("filter")]
        public async Task<Responsive> GetFilterDatBan([FromQuery] string _filter)
        {
            try
            {

                DatBanFilter filter = JsonConvert.DeserializeObject<DatBanFilter>(_filter);
                var query = from s in _context.DatBan select s;
                if (filter.Id != Guid.Empty)
                {
                    query = query.Where((x) => x.Id == filter.Id);
                }
                if (filter.TextSearch.Length > 0)
                {
                    query = query.Where((x) => x.TenKhachHang.Contains(filter.TextSearch));
                }
                if (filter.TimeStart != null && filter.TimeStart > DateTime.MinValue && filter.TimeEnd != null && filter.TimeEnd > DateTime.MinValue)
                {
                    query = query.Where((x) => (x.GioDen >= filter.TimeEnd && x.GioDen <= filter.TimeEnd));
                }
                //if (filter.MaTheLoai != Guid.Empty)
                //{
                //    query = query.Where((x) => x.MaTheLoai == filter.MaTheLoai);
                //}
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
                    mes = "get success";
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
    }

    class DatBanFilter : BaseFilter
    {
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
    }
}
