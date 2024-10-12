using HospitalAppointmentSystem.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.WebApi.Contexts;

public class MsSqlContext : DbContext
{
    // Bu constructor, alınan DbContextOptions parametresini base sınıfa (DbContext) iletir.
    public MsSqlContext(DbContextOptions opt ) : base(opt)
    {
        // DbContextOptions, Entity Framework'e hangi veritabanını kullanacağını,
        // bağlantı ayarlarını ve diğer yapılandırmaları sağlar.
        
        // Bu yapı, dependency injection (bağımlılık enjeksiyonu) ile bu sınıfın dışarıdan yapılandırılmasına olanak tanır.
    }
    
    // Eğer DbContextOptions dışarıdan dependency injection ile yapılandırılmamışsa,
    // OnConfiguring metodu devreye girer ve veritabanı sağlayıcısını burada tanımlarsın.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=Localhost,1907; Database=HospitalAppointmentSystem_db; User=sa; Password=admin12345678; TrustServerCertificate=true");
    }
    
    // Her DbSet, bir veritabanı tablosu ile birebir eşleştirilir.
    // Bu sayede, veritabanı işlemleri nesne yönelimli bir yaklaşımla yönetilebilir.
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
}