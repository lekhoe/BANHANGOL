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
    public class OderController : ControllerBase
    {
        private readonly DataContext _context;

        public OderController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Oder
        [HttpGet]
        public async Task<Responsive> GetOder()
        {
            var res = new Responsive();
            try
            {
                var data = await _context.Oder.ToListAsync();
                res.Code = 200;
                res.Mess = "Get success";
                var Data = new List<OderModel>();
                foreach (var item in data)
                {
                    Data.Add(await ConventOder(item));
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

        private async Task<OderModel> ConventOder(Oder item)
        {
            OderModel oder = new OderModel();
            oder.Id = item.Id;
            oder.PhieuOder = await (from s in _context.PhieuOder
                                    where s.Id == item.IdPhieuOder
                                    select new PhieuOderModel
                                    {
                                        Id = s.Id
                                    }

                              ).FirstOrDefaultAsync(); ;
            oder.DoAn = await (from s in _context.DoAn
                               where s.Id == item.IdDoAn
                               select new DoAnModel
                               {
                                   Id = s.Id,
                                   Name = s.Name
                               }

                              ).FirstOrDefaultAsync();
            oder.SoLuong = item.SoLuong;
            oder.Ban = await (from s in _context.Ban
                              where s.Id == item.IdBan
                              select new BanModel
                              {
                                  Id = s.Id,
                                  Name = s.Name
                              }

                              ).FirstOrDefaultAsync();
            oder.CreatedByUserId = item.CreatedByUserId;
            oder.CreatedByUserName = item.CreatedByUserName;
            oder.CreatedOnDate = item.CreatedOnDate;
            oder.LastModifiedByUserId = item.LastModifiedByUserId;
            oder.LastModifiedByUserName = item.LastModifiedByUserName;
            return oder;
        }
        // GET: api/Oder/5
        [HttpGet("{id}")]
        public async Task<Responsive> GetOder(Guid id)
        { 
            var res = new Responsive();
            res.Code = 204;
            
            var oder = await _context.Oder.FindAsync(id);

            if (oder == null)
            {
                res.Mess = "Not foud";
                return res;
            }
            else
            {
                res.Mess = "Find Sucsses";
                res.Data = await ConventOder(oder);
                return res;
            }
             
        }

        // PUT: api/Oder/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<Responsive> PutOder(Guid id, OderUpdateModel item)
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
                var oder = _context.Oder.Find(id);
                if (oder != null)
                {
                    oder.IdPhieuOder = item.IdPhieuOder;
                    oder.PhieuOder = await _context.PhieuOder.FindAsync(item.IdPhieuOder);
                    oder.IdDoAn = item.IdDoAn;
                    oder.DoAn = await _context.DoAn.FindAsync(item.IdDoAn);
                    oder.SoLuong = item.SoLuong;
                    oder.IdBan = item.IdBan;
                    oder.LastModifiedByUserId = item.LastModifiedByUserId;
                    oder.LastModifiedByUserName = item.LastModifiedByUserName;
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

        // POST: api/Oder
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<Responsive> PostOder(OderCreateModel item)
        {
            try
            {
                //conver
                var oder = new Oder();
                oder.Id = item.Id;
                oder.IdPhieuOder = item.IdPhieuOder;
                oder.PhieuOder = await _context.PhieuOder.FindAsync(item.IdPhieuOder);
                oder.IdDoAn = item.IdDoAn;
                oder.DoAn = await _context.DoAn.FindAsync(item.IdDoAn);
                oder.SoLuong = item.SoLuong;
                oder.IdBan = item.IdBan;
                oder.CreatedByUserId = item.CreatedByUserId;
                oder.CreatedByUserName = item.CreatedByUserName;
                oder.CreatedOnDate = item.CreatedOnDate;
                _context.Oder.Add(oder);
                await _context.SaveChangesAsync();

                return new Responsive(200, "Thêm mới thành công", oder);
            }
            catch (Exception ex)
            {
                return new Responsive(500, ex.InnerException.Message, null);
            }
        }

        // DELETE: api/Oder/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOder(Guid id)
        {
            var oder = await _context.Oder.FindAsync(id);
            if (oder == null)
            {
                return NotFound();
            }

            _context.Oder.Remove(oder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OderExists(Guid id)
        {
            return _context.Oder.Any(e => e.Id == id);
        }
        //[HttpGet("filter")]
        //public async Task<Responsive> GetFilterOder([FromQuery] string _filter)
        //{
        //    try
        //    {

        //        var filter = JsonConvert.DeserializeObject<OderFilter>(_filter);
        //        var query = from s in _context.Oder select s;
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
         
        class OderFilter : BaseFilter
        {
        }
    }
}
