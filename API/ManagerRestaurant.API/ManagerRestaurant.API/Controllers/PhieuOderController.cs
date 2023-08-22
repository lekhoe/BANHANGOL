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
    public class PhieuOderController : ControllerBase
    {
        private readonly DataContext _context;

        public PhieuOderController(DataContext context)
        {
            _context = context;
        }

        // GET: api/PhieuOder
        [HttpGet]
        public async Task<Responsive> GetPhieuOder()
        {
            var res = new Responsive();
            try
            {
                var data = await _context.PhieuOder.ToListAsync();
                res.Code = 200;
                res.Mess = "Get success";
                var Data = new List<PhieuOderModel>();
                foreach (var item in data)
                {
                    Data.Add(await CoverPhieuOder(item));
                }

                res.Data = Data;
                return res;
            }
            catch (Exception ex)
            {
                res.Code = 500;
                res.Mess = ex.InnerException.Message;
                return res;
            }

        }

        async Task<PhieuOderModel> CoverPhieuOder(PhieuOder item)
        {
            var phieuOder = new PhieuOderModel();

            phieuOder.Id = item.Id;
            phieuOder.TongTien = item.TongTien;
            phieuOder.ThucThu = item.ThucThu;
            phieuOder.Vocher = item.Vocher;
            phieuOder.SoTienGiam = item.SoTienGiam;
            phieuOder.TrangThai = item.TrangThai;
            phieuOder.ThoiGianThanhToan = item.ThoiGianThanhToan;
            phieuOder.CreatedByUserId = item.CreatedByUserId;
            phieuOder.CreatedByUserName = item.CreatedByUserName;
            phieuOder.CreatedOnDate = item.CreatedOnDate;
            phieuOder.LastModifiedByUserId = item.LastModifiedByUserId;
            phieuOder.LastModifiedByUserName = item.LastModifiedByUserName;

            phieuOder.Ban = await (from s in _context.Ban
                                   where s.Id == item.IdBan
                                   select new BanModel
                                   {
                                       Id = s.Id,
                                       Name = s.Name
                                   }

                              ).FirstOrDefaultAsync();
            var thungantemp = _context.User.Find(item.IdThuNgan);
            phieuOder.ThuNgan = await (from s in _context.User
                                       where s.Id == item.IdThuNgan
                                       select new UserModel
                                       {
                                           Id = s.Id,
                                           MaNV = s.MaNV,
                                           FullName = s.FullName,
                                           UserName = s.UserName
                                       }

                              ).FirstOrDefaultAsync();
            var khachhangtemp = _context.KhachHang.Find(item.IdKhachHang);
            phieuOder.KhachHang = await (from s in _context.KhachHang
                                         where s.Id == item.IdKhachHang
                                         select new KhachHangModel
                                         {
                                             Id = s.Id,
                                             Name = s.Name,
                                             SoDienThoai = s.SoDienThoai
                                         }

                              ).FirstOrDefaultAsync();

            var odertemp = _context.Oder.Where((x) => x.IdPhieuOder == item.Id).ToList();
            var doans = new List<DoAnV2Model>();
            foreach (var element in odertemp)
            {
                var ele = await _context.DoAn.FindAsync(element.IdDoAn);
                doans.Add(new DoAnV2Model
                {
                    Id = ele.Id,
                    Name = ele.Name,
                    MaTheLoai = ele.MaTheLoai,
                    TenTheLoai = _context.TheLoaiDoAn.FindAsync(ele.MaTheLoai).Result.Name,
                    LinkAnh = ele.LinkAnh,
                    GhiChu = ele.GhiChu,
                    DanhSachMonAn = ele.DanhSachMonAn,
                    DonViTinh = ele.DonViTinh,
                    DonGia = ele.DonGia,
                    TrangThai = ele.TrangThai,
                    CreatedByUserId = ele.CreatedByUserId,
                    CreatedByUserName = ele.CreatedByUserName,
                    CreatedOnDate = ele.CreatedOnDate,
                    LastModifiedByUserId = ele.LastModifiedByUserId,
                    SoLuong = element.SoLuong
                });
            }
            phieuOder.DoAns = doans;
            return phieuOder;
        }
        // GET: api/PhieuOder/5
        [HttpGet("{id}")]
        public async Task<Responsive> GetPhieuOder(Guid id)
        {
            var res = new Responsive();
            try
            {
                var phieuOder = await _context.PhieuOder.FindAsync(id);
                if (phieuOder == null)
                {

                    res.Code = 204;
                    res.Mess = "not found";
                    return res;
                }
                else
                {
                    res.Data = CoverPhieuOder(phieuOder);
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

        // PUT: api/PhieuOder/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<Responsive> PutPhieuOder(Guid id, PhieuOderUpdateModel item)
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
                var phieuOder = _context.PhieuOder.Find(id);
                if (phieuOder != null)
                {
                    phieuOder.IdBan = item.IdBan;
                    phieuOder.IdThuNgan = item.IdThuNgan;
                    phieuOder.IdKhachHang = item.IdKhachHang;
                    phieuOder.Vocher = item.Vocher;
                    phieuOder.TongTien = item.TongTien;
                    phieuOder.ThucThu = item.ThucThu;
                    phieuOder.SoTienGiam = item.SoTienGiam;
                    phieuOder.TrangThai = item.TrangThai;
                    phieuOder.LastModifiedByUserId = item.LastModifiedByUserId;
                    phieuOder.LastModifiedByUserName = item.LastModifiedByUserName;
                    if (item.TrangThai == 1) { 
                        var ban = await _context.Ban.FindAsync(item.IdBan);
                        ban.TrangThai = 0;
                        phieuOder.ThoiGianThanhToan = DateTime.Now;
                        await _context.SaveChangesAsync();
                    }
                    
                    //delete all order old
                    _context.Oder.RemoveRange(_context.Oder.Where(x => x.IdPhieuOder == phieuOder.Id));

                    if (item.MonAns != null)
                    {
                        foreach (var i in item.MonAns)
                        {
                            _context.Oder.Add(new Oder
                            {
                                Id = Guid.NewGuid(),
                                IdPhieuOder = phieuOder.Id,
                                PhieuOder = phieuOder,
                                IdDoAn = i.Id,
                                DoAn = _context.DoAn.Find(i.Id),
                                SoLuong = i.SoLuong,
                                IdBan = phieuOder.IdBan,
                                CreatedByUserId = i.CreatedByUserId,
                                CreatedByUserName = i.CreatedByUserName,
                                CreatedOnDate = DateTime.Now,
                                LastModifiedByUserId = i.LastModifiedByUserId,
                                LastModifiedByUserName = i.LastModifiedByUserName,
                            });
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

        // POST: api/PhieuOder
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<Responsive> PostPhieuOder(PhieuOderCreateModel item)
        {
            try
            {
                //conver
                var phieuOder = new PhieuOder();
                phieuOder.Id = Guid.NewGuid();
                phieuOder.IdBan = item.IdBan;
                phieuOder.IdThuNgan = item.IdThuNgan;
                phieuOder.IdKhachHang = item.IdKhachHang;
                phieuOder.Vocher = item.Vocher;
                phieuOder.TongTien = item.TongTien;
                phieuOder.ThucThu = item.ThucThu;
                phieuOder.SoTienGiam = item.SoTienGiam;
                phieuOder.TrangThai = 0;
                phieuOder.CreatedByUserId = item.CreatedByUserId;
                phieuOder.CreatedByUserName = item.CreatedByUserName;
                phieuOder.CreatedOnDate = DateTime.Now;

                if (item.MonAns != null)
                {
                    foreach (var i in item.MonAns)
                    {
                        _context.Oder.Add(new Oder
                        {
                            Id = Guid.NewGuid(),
                            IdPhieuOder = phieuOder.Id,
                            PhieuOder = phieuOder,
                            IdDoAn = i.Id,
                            DoAn = _context.DoAn.Find(i.Id),
                            SoLuong = i.SoLuong,
                            IdBan = phieuOder.IdBan,
                            CreatedByUserId = i.CreatedByUserId,
                            CreatedByUserName = i.CreatedByUserName,
                            CreatedOnDate = DateTime.Now,
                            LastModifiedByUserId = i.LastModifiedByUserId,
                            LastModifiedByUserName = i.LastModifiedByUserName,
                        });
                    }

                }
                _context.PhieuOder.Add(phieuOder);
                await _context.SaveChangesAsync();

                return new Responsive(200, "Thêm mới thành công", phieuOder);
            }
            catch (Exception ex)
            {
                return new Responsive(500, ex.InnerException.Message, null);
            }
        }

        [HttpGet("findbyidban/{idban}")]
        public async Task<Responsive> GetByIdBan(Guid idban)
        {
            try
            {
                //Lấy thông tin khách hàng theo bàn
                var phieu = await _context.PhieuOder.Where(x => x.IdBan == idban && x.TrangThai == 0).FirstOrDefaultAsync();
                if (phieu != null)
                {
                    var res = new Responsive();
                    res.Code = 200;
                    res.Data = await CoverPhieuOder(phieu);
                    res.Mess = "Get success";
                    return res;
                }
                else
                {
                    var res = new Responsive();
                    res.Data = null;
                    res.Code = 204;
                    res.Mess = "not exist";
                    return res;
                }

            }
            catch (Exception ex)
            {
                return new Responsive(500, ex.InnerException.Message, null);
            }
        }

        // DELETE: api/PhieuOder/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhieuOder(Guid id)
        {
            var phieuOder = await _context.PhieuOder.FindAsync(id);
            if (phieuOder == null)
            {
                return NotFound();
            }

            _context.PhieuOder.Remove(phieuOder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //[HttpGet("filter")]
        //public async Task<Responsive> GetFilterPhieuOder([FromQuery] string _filter)
        //{
        //    try
        //    {

        //        var filter = JsonConvert.DeserializeObject<PhieuOderFilter>(_filter);
        //        var query = from s in _context.PhieuOder select s;
        //        if (filter.Id != Guid.Empty)
        //        {
        //            query = query.Where((x) => x.Id == filter.Id);
        //        }
        //        if (filter.TextSearch.Length > 0)
        //        {
        //            query = query.Where((x) => x.Name.Contains(filter.TextSearch));
        //        }

        //        if (filter.PageNumber > 0 && filter.PageSize > 0)
        //        {
        //            query = query.Skip(filter.PageSize * (filter.PageNumber - 1)).Take(filter.PageSize);
        //        }

        //        var data = await query.ToListAsync();

        //        var mes = "";
        //        if (data.Count == 0)
        //        {
        //            mes = "Not data";
        //        }
        //        else
        //        {
        //            mes = "Get success";
        //        }

        //        var res = new Responsive(200, mes, data);
        //        return res;
        //    }
        //    catch (Exception err)
        //    {
        //        var res = new Responsive(500, err.Message, err.ToString());
        //        return res;
        //    }
        //}

        class PhieuOderFilter : BaseFilter
        {
            public Guid? IdBan { get; set; }
        }
    }
}
