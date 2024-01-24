
using ICM_MUHASEBE_RAPOR_MİKRO.Tables;
using Microsoft.EntityFrameworkCore;

namespace ICM_MUHASEBE_RAPOR_MİKRO.Context
{
    public class MyDbContext : KDBContext
    {
        public DbSet<PROJELER> PROJELER { get; set; }
        public DbSet<CARI_HESAPLAR> CARI_HESAPLAR { get; set; }
        public DbSet<SIPARISLER> SIPARISLER { get; set; }
        public DbSet<STOK_HAREKETLERI> STOK_HAREKETLERI { get; set; }
        public DbSet<IHRACAT_DOSYALARI> IHRACAT_DOSYALARI { get; set; }
        public DbSet<STOKLAR> STOKLAR {  get; set; }
        public DbSet<CARI_HESAP_HAREKETLERI> CARI_HESAP_HAREKETLERI { get; set; }
        public DbSet<E_FATURA_ISLEMLERI> E_FATURA_ISLEMLERI { get; set; }
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
            modelBuilder.Entity<SIPARISLER>().ToTable("SIPARISLER").HasKey(x => x.sip_Guid);
            modelBuilder.Entity<STOK_HAREKETLERI>().ToTable("STOK_HAREKETLERI").HasKey(x => x.sth_Guid);
            modelBuilder.Entity<IHRACAT_DOSYALARI>().ToTable("IHRACAT_DOSYALARI").HasKey(x => x.ihr_Guid);
            modelBuilder.Entity<STOKLAR>().ToTable("STOKLAR").HasKey(x => x.sto_Guid);
            modelBuilder.Entity<CARI_HESAP_HAREKETLERI>().ToTable("CARI_HESAP_HAREKETLERI").HasKey(x=>x.cha_Guid);
            modelBuilder.Entity<E_FATURA_ISLEMLERI>().ToTable("E_FATURA_ISLEMLERI").HasKey(x => x.efi_Guid);
        }

    }
}
