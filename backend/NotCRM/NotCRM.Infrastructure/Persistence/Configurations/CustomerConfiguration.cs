using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotCRM.Domain.Entities;

namespace NotCRM.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration
    : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        // Customer зберігатиметься
        // в таблиці "Customers".
        builder.ToTable("Customers");

        // Id є Primary Key.
        builder.HasKey(x => x.Id);

        // Name обов'язкове поле.
        // Максимально 200 символів.
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        // Phone необов'язковий.
        builder.Property(x => x.Phone)
            .HasMaxLength(50);

        // Email необов'язковий.
        builder.Property(x => x.Email)
            .HasMaxLength(320);

        // CreatedAt обов'язковий.
        builder.Property(x => x.CreatedAt)
            .IsRequired();

        // ОСЬ ТУТ СТВОРЮЄМО RELATIONSHIP.
        //
        // Customer має один Business.
        builder.HasOne<Business>()

            // Один Business може мати багато Customers.
            //
            // Ми поки не додаємо
            // Business.Customers collection,
            // тому WithMany() без параметрів.
            .WithMany()

            // Foreign key — Customer.BusinessId.
            .HasForeignKey(x => x.BusinessId)

            // Якщо хтось спробує видалити Business,
            // у якого є Customers,
            // база не дозволить це зробити автоматично.
            //
            // Це безпечніше за випадкове видалення
            // сотень клієнтів.
            .OnDelete(DeleteBehavior.Restrict);

        // Створюємо index по BusinessId.
        //
        // Ми дуже часто будемо робити:
        // "дай усіх клієнтів цього бізнесу".
        //
        // Index прискорює такий пошук.
        builder.HasIndex(x => x.BusinessId);
    }
}