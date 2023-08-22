using Infratructure.Datatables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagerRestaurant.API.Infratructure.Datatables;

namespace Infratructure
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Ban> Ban { get; set; }
        public DbSet<ChiTieuTrongNgay> ChiTieuTrongNgay { get; set; }
        public DbSet<DatBan> DatBan { get; set; }
        public DbSet<DoAn> DoAn { get; set; }
        public DbSet<KhachHang> KhachHang { get; set; }
        public DbSet<Oder> Oder { get; set; }
        public DbSet<PhieuNhapVatTu> PhieuNhapVatTu { get; set; }
        public DbSet<PhieuOder> PhieuOder { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<YKienDongGop> YKienDongGop { get; set; }
        public DbSet<KhuVuc> KhuVuc { get; set; }
        public DbSet<UuDai> UuDai { get; set; }
        public DbSet<Quyen> Quyen { get; set; }
        public DbSet<TheLoaiDoAn> TheLoaiDoAn { get; set; }
        public DbSet<ManagerRestaurant.API.Infratructure.Datatables.Bar> Bar { get; set; }
    }
}
