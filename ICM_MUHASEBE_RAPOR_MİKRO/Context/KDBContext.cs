
using ICM_MUHASEBE_RAPOR_MİKRO.Tables;
using Microsoft.EntityFrameworkCore;

namespace ICM_MUHASEBE_RAPOR_MİKRO.Context
{
    public class KDBContext : DbContext
    {
        public DbSet<KUR_ISIMLERI> KUR_ISIMLERI { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            string connectionString = "Data Source=SRVMIKRO;Initial Catalog=MikroDB_V16;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// Tablo adını ve sütun adlarını özelleştirme
            modelBuilder.Entity<KUR_ISIMLERI>().ToTable("KUR_ISIMLERI").HasKey(x => x.Kur_Guid);


        }

    }
}
