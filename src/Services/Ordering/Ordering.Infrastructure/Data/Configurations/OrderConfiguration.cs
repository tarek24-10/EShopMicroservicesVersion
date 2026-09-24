using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.domain.Enums;
using Ordering.domain.Models;
using Ordering.domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasConversion(
                    id => id.Value,
                    value => OrderId.Of(value));

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId)
                .IsRequired();

            builder.ComplexProperty(o => o.OrderName, namebuilder => { 
                namebuilder.Property(n => n.Value)
                .HasColumnName(nameof(OrderName.Value))
                .HasMaxLength(100)
                .IsRequired();
            });

            builder.ComplexProperty(o => o.ShippingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.FirstName).HasColumnName(nameof(Address.FirstName)).HasMaxLength(100).IsRequired();
                addressBuilder.Property(a => a.LastName).HasColumnName(nameof(Address.LastName)).HasMaxLength(100).IsRequired();
                addressBuilder.Property(a => a.EmailAddress).HasColumnName(nameof(Address.EmailAddress)).HasMaxLength(100).IsRequired();
                addressBuilder.Property(a => a.AddressLine).HasColumnName(nameof(Address.AddressLine)).HasMaxLength(200).IsRequired();
                addressBuilder.Property(a => a.State).HasColumnName(nameof(Address.State)).HasMaxLength(100).IsRequired();
                addressBuilder.Property(a => a.Country).HasColumnName(nameof(Address.Country)).HasMaxLength(100).IsRequired();
                addressBuilder.Property(a => a.ZipCode).HasColumnName(nameof(Address.ZipCode)).HasMaxLength(3).IsRequired();
            });

            builder.ComplexProperty(o => o.BillingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.FirstName).HasColumnName(nameof(Address.FirstName)).HasMaxLength(100).IsRequired();
                addressBuilder.Property(a => a.LastName).HasColumnName(nameof(Address.LastName)).HasMaxLength(100).IsRequired();
                addressBuilder.Property(a => a.EmailAddress).HasColumnName(nameof(Address.EmailAddress)).HasMaxLength(100).IsRequired();
                addressBuilder.Property(a => a.AddressLine).HasColumnName(nameof(Address.AddressLine)).HasMaxLength(200).IsRequired();
                addressBuilder.Property(a => a.State).HasColumnName(nameof(Address.State)).HasMaxLength(100).IsRequired();
                addressBuilder.Property(a => a.Country).HasColumnName(nameof(Address.Country)).HasMaxLength(100).IsRequired();
                addressBuilder.Property(a => a.ZipCode).HasColumnName(nameof(Address.ZipCode)).HasMaxLength(3).IsRequired();
            });

            builder.ComplexProperty(o => o.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(p => p.PaymentMethod).HasColumnName(nameof(Payment.PaymentMethod)).HasMaxLength(50).IsRequired();
                paymentBuilder.Property(p => p.CardName).HasColumnName(nameof(Payment.CardName)).HasMaxLength(100).IsRequired();
                paymentBuilder.Property(p => p.CardNumber).HasColumnName(nameof(Payment.CardNumber)).HasMaxLength(16).IsRequired();
                paymentBuilder.Property(p => p.Expiration).HasColumnName(nameof(Payment.Expiration)).HasMaxLength(5).IsRequired();
                paymentBuilder.Property(p => p.CVV).HasColumnName(nameof(Payment.CVV)).HasMaxLength(3).IsRequired();
            });

            builder.Property(o => o.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(
                    status => status.ToString(),
                    value => Enum.Parse<OrderStatus>(value));

            builder.Property(o => o.TotalPrice);
        }
    }
}
