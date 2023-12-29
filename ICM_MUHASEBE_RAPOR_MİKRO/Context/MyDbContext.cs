
using ICM_MUHASEBE_RAPOR_MİKRO.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICM_MUHASEBE_RAPOR_MİKRO.Context
{
    public class MyDbContext : DbContext
    {
        public DbSet<PROJELER> PROJELER { get; set; }
        public DbSet<CARI_HESAPLAR> CARI_HESAPLAR { get; set; }
        //public DbSet<SIPARISLER> SIPARISLER { get; set; }
        //public DbSet<URETIM_OPERASYONLARI> URETIM_OPERASYONLARI { get; set; }
        //public DbSet<URETIM_ROTA_PLANLARI> URETIM_ROTA_PLANLARI { get; set; }
        //public DbSet<CARI_HESAPLAR> CARI_HESAPLAR { get; set; }
        //public DbSet<ISEMIRLERI_USER> ISEMIRLERI_USER { get; set; }
        //public DbSet<STOKLAR> STOKLAR { get; set; }
        //public DbSet<STOK_HAREKETLERI> STOK_HAREKETLERI {  get; set; }
        //public DbSet<IS_MERKEZLERI> IS_MERKEZLERI { get; set; }
        //public DbSet<URETIM_OPERASYON_HAREKETLERI> URETIM_OPERASYON_HAREKETLERI {  get; set; }
        //public DbSet<CARI_PERSONEL_TANIMLARI> CARI_PERSONEL_TANIMLARI { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Burada veritabanı bağlantı bilgilerini tanımlayın.
            // Örnek olarak SQL Server kullanalım:
            string connectionString = "Data Source=SRVMIKRO;Initial Catalog=MikroDB_V16_ICM;Integrated Security=True;Connect Timeout=10;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// Tablo adını ve sütun adlarını özelleştirme
            modelBuilder.Entity<PROJELER>().ToTable("PROJELER").HasKey(x => x.pro_Guid);
            modelBuilder.Entity<CARI_HESAPLAR>().ToTable("CARI_HESAPLAR").HasKey(x => x.cari_Guid);
            //modelBuilder.Entity<SIPARISLER>().ToTable("SIPARISLER").HasKey(x => x.sip_Guid);
            //modelBuilder.Entity<URETIM_ROTA_PLANLARI>().ToTable("URETIM_ROTA_PLANLARI").HasKey(x => x.RtP_Guid);
            //modelBuilder.Entity<URETIM_OPERASYONLARI>().ToTable("URETIM_OPERASYONLARI").HasKey(x => x.Op_Guid);
            //modelBuilder.Entity<CARI_HESAPLAR>().ToTable("CARI_HESAPLAR").HasKey(x => x.cari_Guid);
            //modelBuilder.Entity<ISEMIRLERI_USER>().ToTable("ISEMIRLERI_USER").HasKey(x => x.Record_uid);
            //modelBuilder.Entity<STOKLAR>().ToTable("STOKLAR").HasKey(x => x.sto_Guid);
            //modelBuilder.Entity<STOK_HAREKETLERI>().ToTable("STOK_HAREKETLERI").HasKey(x => x.sth_Guid);
            //modelBuilder.Entity<IS_MERKEZLERI>().ToTable("IS_MERKEZLERI").HasKey(x => x.IsM_Guid);
            //modelBuilder.Entity<URETIM_OPERASYON_HAREKETLERI>().ToTable("URETIM_OPERASYON_HAREKETLERI").HasKey(x => x.OpT_Guid);
            //modelBuilder.Entity<CARI_PERSONEL_TANIMLARI>().ToTable("CARI_PERSONEL_TANIMLARI").HasKey(x=>x.cari_per_Guid);
        }

    }
}
