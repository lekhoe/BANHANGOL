using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infratructure;
using Infratructure.Datatables;
using Newtonsoft.Json;
using ManagerRestaurant.API.Models;

namespace ManagerRestaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoAnController : ControllerBase
    {
        private readonly DataContext _context;

        public DoAnController(DataContext context)
        {
            _context = context;
        }

        // GET: api/DoAn
        [HttpGet]
        public async Task<Responsive> GetDoAn()
        {
            Responsive res = new Responsive();
            try
            {
                List<DoAnModel> data = new List<DoAnModel>();
                var d = await _context.DoAn.ToListAsync();
                foreach (var item in d)
                {
                    data.Add(Convert(item));
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

        // GET: api/DoAn/5
        [HttpGet("{id}")]
        public async Task<Responsive> GetDoAn(Guid id)
        {
            var res = new Responsive();
            var item = await _context.DoAn.FindAsync(id);
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
                res.Data = Convert(item);
            }
            return res;
        }

        DoAnModel Convert(DoAn item)
        {
            DoAnModel res = new DoAnModel();
            res.Id = item.Id;
            res.Name = item.Name;
            res.MaTheLoai = item.MaTheLoai;
            res.TenTheLoai = _context.TheLoaiDoAn.Find(item.MaTheLoai).Name;
            res.LinkAnh = item.LinkAnh;
            res.GhiChu = item.GhiChu;
            res.DanhSachMonAn = item.DanhSachMonAn;
            res.DonViTinh = item.DonViTinh;
            res.DonGia = item.DonGia;
            res.TrangThai = item.TrangThai;
            res.CreatedByUserId = item.CreatedByUserId;
            res.CreatedByUserName = item.CreatedByUserName;
            res.CreatedOnDate = item.CreatedOnDate;
            res.LastModifiedByUserId = item.LastModifiedByUserId;
            res.LastModifiedByUserName = item.LastModifiedByUserName;
            return res;
        }
        // PUT: api/DoAn/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<Responsive> PutDoAn(Guid id, DoAnUpdateModel item)
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
                var doAn = _context.DoAn.Find(id);
                if (doAn != null)
                {
                    doAn.Name = item.Name;
                    doAn.MaTheLoai = item.MaTheLoai;
                    doAn.TheLoaiDoAn = await _context.TheLoaiDoAn.FindAsync(item.MaTheLoai);
                    doAn.LinkAnh = item.LinkAnh;
                    doAn.GhiChu = item.GhiChu;
                    doAn.DanhSachMonAn = item.DanhSachMonAn;
                    doAn.DonViTinh = item.DonViTinh;
                    doAn.TrangThai = item.TrangThai;

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

        // POST: api/DoAn
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<Responsive> PostDoAn(DoAnCreateModel item)
        {
            try
            {
                //conver
                var doAn = new DoAn();
                doAn.Id = Guid.NewGuid();
                doAn.Name = item.Name;
                doAn.MaTheLoai = item.MaTheLoai;
                //doAn.TheLoaiDoAn = await _context.TheLoaiDoAn.FindAsync(item.MaTheLoai);
                doAn.LinkAnh = item.LinkAnh;
                doAn.GhiChu = item.GhiChu;
                doAn.DanhSachMonAn = item.DanhSachMonAn;
                doAn.DonGia = item.DonGia;
                doAn.DonViTinh = item.DonViTinh;
                doAn.TrangThai = item.TrangThai;
                doAn.CreatedByUserId = item.CreatedByUserId;
                doAn.CreatedByUserName = item.CreatedByUserName;
                doAn.CreatedOnDate = DateTime.Now;

                _context.DoAn.Add(doAn);
                await _context.SaveChangesAsync();

                return new Responsive(200, "Thêm mới thành công", doAn);
            }
            catch (Exception ex)
            {
                return new Responsive(500, ex.InnerException.Message, null);
            }
        }

        // POST: api/DoAn
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpGet("filter")]
        public async Task<Responsive> GetFilterDoAn([FromQuery] string _filter)
        {
            try
            {

                var filter = JsonConvert.DeserializeObject<DoAnFilter>(_filter);
                var query = from s in _context.DoAn select s;
                if (filter.Id != Guid.Empty)
                {
                    query = query.Where((x) => x.Id == filter.Id);
                }
                if (filter.TextSearch != null && filter.TextSearch.Length > 0)
                {
                    query = query.Where((x) => x.Name.Contains(filter.TextSearch));
                }
                if (filter.MaTheLoai != Guid.Empty)
                {
                    query = query.Where((x) => x.MaTheLoai == filter.MaTheLoai);
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
                var resData = new List<DoAnModel>();
                foreach (var item in data)
                {
                    resData.Add(Convert(item));
                }

                var res = new Responsive(200, mes, resData);
                return res;
            }
            catch (Exception err)
            {
                var res = new Responsive(500, err.Message, err.ToString());
                return res;
            }
        }

        // DELETE: api/DoAn/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoAn(Guid id)
        {
            var doAn = await _context.DoAn.FindAsync(id);
            if (doAn == null)
            {
                return NotFound();
            }

            _context.DoAn.Remove(doAn);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }

    class DoAnFilter : BaseFilter
    {
        public Guid MaTheLoai { get; set; } = Guid.Empty;
    }
}
