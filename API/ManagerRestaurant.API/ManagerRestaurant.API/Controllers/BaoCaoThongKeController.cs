using Infratructure;
using Infratructure.Datatables;
using ManagerRestaurant.API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManagerRestaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaoCaoThongKeController : ControllerBase
    {
        private readonly DataContext _context;

        public BaoCaoThongKeController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ChiTieuTrongNgay
        [HttpGet("thuchi/{filter}")]
        public Responsive GetThongKeThuChi(string filter)
        {
            Responsive res = new Responsive();
            try
            {
                var f = JsonConvert.DeserializeObject<FilterThongKe>(filter);
                if (f.isMonth == null)
                {
                    res.Code = 204;
                    res.Mess = "Thiếu trường kiểm tra tháng";
                    return res;
                }

                var ListCTNgay = (from s in _context.ChiTieuTrongNgay where s.NgayHoaDon >= f.TimeStart && s.NgayHoaDon <= f.TimeEnd select s).ToList();
                var ListCTVT = (from s in _context.PhieuNhapVatTu where s.NgayHoaDon >= f.TimeStart && s.NgayHoaDon <= f.TimeEnd select s).ToList();


                //thu
                var ListPhieuOder = (from s in _context.PhieuOder where s.ThoiGianThanhToan >= f.TimeStart && s.ThoiGianThanhToan <= f.TimeEnd && s.TrangThai == 1 select s);

                var Data = new List<ThongKeReturn>();
                if (f.isMonth != true)
                {
                    var ListNgay = new List<DateTime>();
                    foreach (var item in ListCTNgay)
                    {
                        DateTime d1 = new DateTime(item.NgayHoaDon.Year, item.NgayHoaDon.Month, item.NgayHoaDon.Day, 0, 0, 0);
                        if (ListNgay.IndexOf(d1) == -1)
                        {
                            ListNgay.Add(d1);
                        }
                    }
                    foreach (var item in ListCTVT)
                    {
                        DateTime d1 = new DateTime(item.NgayHoaDon.Year, item.NgayHoaDon.Month, item.NgayHoaDon.Day, 0, 0, 0);
                        if (ListNgay.IndexOf(d1) == -1)
                        {
                            ListNgay.Add(d1);
                        }
                    }

                    foreach (var item in ListNgay)
                    {
                        float tienchi = 0;
                        tienchi += ListCTNgay.Where(x => x.NgayHoaDon.Year == item.Year && x.NgayHoaDon.Month == item.Month && x.NgayHoaDon.Day == item.Day).Select(x => x.TongSoTien).Sum();
                        tienchi += ListCTVT.Where(x => x.NgayHoaDon.Year == item.Year && x.NgayHoaDon.Month == item.Month && x.NgayHoaDon.Day == item.Day).Select(x => x.TongSoTien).Sum();

                        float tienthu = 0;
                        tienthu += ListPhieuOder.Where(x => x.ThoiGianThanhToan.Year == item.Year && x.ThoiGianThanhToan.Month == item.Month && x.ThoiGianThanhToan.Day == item.Day).Select(x => x.ThucThu).Sum();
                        Data.Add(new ThongKeReturn
                        {
                            Ngay = item,
                            SoChi = tienchi,
                            SoThu = tienthu
                        });
                    }

                }
                else
                {
                    var ListNgay = new List<DateTime>();
                    foreach (var item in ListCTNgay)
                    {
                        DateTime d1 = new DateTime(item.NgayHoaDon.Year, item.NgayHoaDon.Month, 1, 0, 0, 0);
                        if (ListNgay.IndexOf(d1) == -1)
                        {
                            ListNgay.Add(d1);
                        }
                    }
                    foreach (var item in ListCTVT)
                    {
                        DateTime d1 = new DateTime(item.NgayHoaDon.Year, item.NgayHoaDon.Month, 1, 0, 0, 0);
                        if (ListNgay.IndexOf(d1) == -1)
                        {
                            ListNgay.Add(d1);
                        }
                    }

                    foreach (var item in ListNgay)
                    {
                        float tienchi = 0;
                        tienchi += ListCTNgay.Where(x => x.NgayHoaDon.Year == item.Year && x.NgayHoaDon.Month == item.Month).Select(x => x.TongSoTien).Sum();
                        tienchi += ListCTVT.Where(x => x.NgayHoaDon.Year == item.Year && x.NgayHoaDon.Month == item.Month).Select(x => x.TongSoTien).Sum();

                        float tienthu = 0;
                        tienthu += ListPhieuOder.Where(x => x.ThoiGianThanhToan.Year == item.Year && x.ThoiGianThanhToan.Month == item.Month).Select(x => x.ThucThu).Sum();
                        Data.Add(new ThongKeReturn
                        {
                            Ngay = item,
                            SoChi = tienchi,
                            SoThu = tienthu
                        });
                    }
                }
                res.Data = Data;
                res.Code = 200;
                return res;
            }
            catch (Exception ex)
            {
                res.Mess = ex.InnerException.Message;
                return res;
            }

        }
        // GET: api/ChiTieuTrongNgay
        [HttpGet("monan/{filter}")]
        public async Task<Responsive> GetThongKeMonAn(string filter)
        {
            Responsive res = new Responsive();
            try
            {
                var f = JsonConvert.DeserializeObject<FilterThongKe>(filter);
                var phieuOder = (from s in _context.PhieuOder where (s.ThoiGianThanhToan >= f.TimeStart && s.ThoiGianThanhToan <= f.TimeEnd && s.TrangThai == 1) select s.Id).ToList();

                var listDoAn = new List<Guid>();
                var oders = new List<Oder>();
                foreach (var item in phieuOder)
                {
                    oders.AddRange(_context.Oder.Where(x => x.IdPhieuOder == item).ToList());

                }
                var Data = new List<TKMonAnReturn>();
                foreach (var item in oders)
                {
                    var solan = (from s in oders where (s.IdDoAn == item.IdDoAn) select s.SoLuong).Sum();
                    Data.Add(new TKMonAnReturn
                    {
                        SoLanGoi = solan,
                        MonAn = await _context.DoAn.FindAsync(item.IdDoAn)
                    });
                }
                res.Mess = "Get Success";
                res.Data = Data;
                res.Code = 200;
                return res;
            }
            catch (Exception ex)
            {
                res.Mess = ex.InnerException.Message;
                return res;
            }
        }
        //// GET: api/ChiTieuTrongNgay
        //[HttpGet("khach/{filter}")]
        //public async Task<Responsive> GetThongKeNhapXuat(string filter)
        //{
        //    Responsive res = new Responsive();
        //    try
        //    {
        //        var f = JsonConvert.DeserializeObject<FilterThongKe>(filter);
        //        if (f.isMonth != null)
        //        {
        //            throw new Exception("Thiếu trường kiểm tra tháng");
        //        }

        //        var ListCTNgay = (from s in _context.ChiTieuTrongNgay where s.NgayHoaDon >= f.TimeStart && s.NgayHoaDon <= f.TimeEnd select s).ToList();
        //        var ListCTVT = (from s in _context.PhieuNhapVatTu where s.NgayHoaDon >= f.TimeStart && s.NgayHoaDon <= f.TimeEnd select s).ToList();


        //        //thu
        //        var ListPhieuOder = (from s in _context.PhieuOder where s.ThoiGianThanhToan >= f.TimeStart && s.ThoiGianThanhToan <= f.TimeEnd && s.TrangThai == 1 select s);

        //        var Data = new List<ThongKeReturn>();
        //        if (f.isMonth != true)
        //        {
        //            var ListNgay = new List<DateTime>();
        //            foreach (var item in ListCTNgay)
        //            {
        //                DateTime d1 = new DateTime(item.NgayHoaDon.Year, item.NgayHoaDon.Month, item.NgayHoaDon.Day, 0, 0, 0);
        //                if (ListNgay.IndexOf(d1) == -1)
        //                {
        //                    ListNgay.Add(d1);
        //                }
        //            }
        //            foreach (var item in ListCTVT)
        //            {
        //                DateTime d1 = new DateTime(item.NgayHoaDon.Year, item.NgayHoaDon.Month, item.NgayHoaDon.Day, 0, 0, 0);
        //                if (ListNgay.IndexOf(d1) == -1)
        //                {
        //                    ListNgay.Add(d1);
        //                }
        //            }

        //            foreach (var item in ListNgay)
        //            {
        //                float tienchi = 0;
        //                tienchi += ListCTNgay.Where(x => x.NgayHoaDon.Year == item.Year && x.NgayHoaDon.Month == item.Month && x.NgayHoaDon.Day == item.Day).Select(x => x.TongSoTien).Sum();
        //                tienchi += ListCTVT.Where(x => x.NgayHoaDon.Year == item.Year && x.NgayHoaDon.Month == item.Month && x.NgayHoaDon.Day == item.Day).Select(x => x.TongSoTien).Sum();

        //                float tienthu = 0;
        //                tienthu += ListPhieuOder.Where(x => x.ThoiGianThanhToan.Year == item.Year && x.ThoiGianThanhToan.Month == item.Month && x.ThoiGianThanhToan.Day == item.Day).Select(x => x.ThucThu).Sum();
        //                Data.Add(new ThongKeReturn
        //                {
        //                    Ngay = item,
        //                    SoChi = tienchi,
        //                    SoThu = tienthu
        //                });
        //            }

        //        }
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        res.Mess = ex.InnerException.Message;
        //        return res;
        //    }
        //}
        //// GET: api/ChiTieuTrongNgay
        //[HttpGet("hangden/{filter}")]
        //public async Task<Responsive> GetHangDaDen(string filter)
        //{
        //    Responsive res = new Responsive();
        //    try
        //    {
        //        var f = JsonConvert.DeserializeObject<FilterThongKe>(filter);
        //        var phieuOder = (from s in _context.PhieuOder where (s.ThoiGianThanhToan >= f.TimeStart && s.ThoiGianThanhToan <= f.TimeEnd && s.TrangThai == 1) select s.Id).ToList();

        //        var listDoAn = new List<Guid>();
        //        var oders = new List<Oder>();
        //        foreach (var item in phieuOder)
        //        {
        //            oders.AddRange(_context.Oder.Where(x => x.IdPhieuOder == item).ToList());

        //        }
        //        var Data = new List<TKMonAnReturn>();
        //        foreach (var item in oders)
        //        {
        //            var solan = (from s in oders where (s.IdDoAn == item.IdDoAn) select s.SoLuong).Sum();
        //            Data.Add(new TKMonAnReturn
        //            {
        //                SoLanGoi = solan,
        //                MonAn = await _context.DoAn.FindAsync(item.IdDoAn)
        //            });
        //        }
        //        res.Mess = "Get Success";
        //        res.Data = Data;
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        res.Mess = ex.InnerException.Message;
        //        return res;
        //    }
        //}

        public class FilterThongKe
        {
            public DateTime TimeStart { get; set; }
            public DateTime TimeEnd { get; set; }
            public bool? isMonth { get; set; }
            public int? PageSize { get; set; } = 10;
        }
        public class ThongKeReturn
        {
            public DateTime Ngay { get; set; }
            public float SoThu { get; set; }
            public float SoChi { get; set; }
        }
        public class TKMonAnReturn
        {
            public int SoLanGoi { get; set; }
            public DoAn MonAn { get; set; }
        }
    }
}
