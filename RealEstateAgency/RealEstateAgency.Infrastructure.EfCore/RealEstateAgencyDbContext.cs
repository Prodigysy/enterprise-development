using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Domain.Model;

namespace RealEstateAgency.Infrastructure.EfCore;

/// <summary>
/// Контекст базы данных риэлторского агентства
/// Управляет сущностями контрагентов объектов недвижимости и заявок на недвижимость
/// Содержит DbSet для Counterparty, RealEstate и RealEstateApplication
/// Настраивает таблицы, поля, ограничения длины строк, связи и каскадное удаление
/// </summary>
public class RealEstateAgencyDbContext(DbContextOptions options) : DbContext(options)
{
    /// <summary>
    /// Таблица контрагентов
    /// </summary>
    public DbSet<Counterparty> Counterparties { get; set; }

    /// <summary>
    /// Таблица объектов недвижимости
    /// </summary>
    public DbSet<RealEstate> RealEstates { get; set; }

    /// <summary>
    /// Таблица заявок на недвижимость
    /// </summary>
    public DbSet<RealEstateApplication> RealEstateApplications { get; set; }

    /// <summary>
    /// Конфигурирует модели базы данных
    /// Задает таблицы, ключи, свойства, ограничения длины строк и связи между сущностями
    /// Настраивает каскадное удаление для связанных сущностей
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Counterparty>(builder =>
        {
            builder.ToTable("counterparties");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("id");
            builder.Property(c => c.PassportNumber).IsRequired().HasMaxLength(20).HasColumnName("passport_number");
            builder.Property(c => c.FirstName).IsRequired().HasMaxLength(50).HasColumnName("first_name");
            builder.Property(c => c.Patronymic).HasMaxLength(50).HasColumnName("patronymic");
            builder.Property(c => c.LastName).IsRequired().HasMaxLength(50).HasColumnName("last_name");
            builder.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(20).HasColumnName("phone_number");
        });

        modelBuilder.Entity<RealEstate>(builder =>
        {
            builder.ToTable("real_estates");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id).HasColumnName("id");
            builder.Property(r => r.RealEstateType).IsRequired().HasColumnName("real_estate_type");
            builder.Property(r => r.RealEstatePurpose).IsRequired().HasColumnName("real_estate_purpose");
            builder.Property(r => r.CadastralNumber).IsRequired().HasMaxLength(50).HasColumnName("cadastral_number");
            builder.Property(r => r.Address).IsRequired().HasMaxLength(200).HasColumnName("address");
            builder.Property(r => r.NumberOfFloors).IsRequired().HasColumnName("number_of_floors");
            builder.Property(r => r.TotalArea).IsRequired().HasColumnName("total_area");
            builder.Property(r => r.NumberOfRooms).HasColumnName("number_of_rooms");
            builder.Property(r => r.CeilingHeight).HasColumnName("ceiling_height");
            builder.Property(r => r.FloorNumber).HasColumnName("floor_number");
            builder.Property(r => r.HasEncumbrances).IsRequired().HasColumnName("has_encumbrances");
        });

        modelBuilder.Entity<RealEstateApplication>(builder =>
        {
            builder.ToTable("real_estate_applications");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id).HasColumnName("id");
            builder.Property(r => r.CounterpartyId).IsRequired().HasColumnName("counterparty_id");
            builder.Property(r => r.RealEstateId).IsRequired().HasColumnName("real_estate_id");
            builder.Property(r => r.ApplicationType).IsRequired().HasColumnName("application_type");
            builder.Property(r => r.Amount).HasColumnName("amount");
            builder.Property(r => r.DateCreated).IsRequired().HasColumnName("date_created");

            builder.HasOne(r => r.Counterparty)
                   .WithMany()
                   .HasForeignKey(r => r.CounterpartyId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.RealEstate)
                   .WithMany()
                   .HasForeignKey(r => r.RealEstateId)
                   .OnDelete(DeleteBehavior.Cascade);
        });
    }
}