using Hotel.Core.Entities.FeedbackModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.ToTable("Feedbacks");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Comment)
            .HasMaxLength(500)
            .IsRequired(false); // Comment is optional

        builder.Property(f => f.Rating)
            .IsRequired();

        // ✅ Ensure CustomerId is treated as a string
        builder.Property(f => f.CustomerId)
            .IsRequired()
            .HasColumnType("nvarchar(450)"); // Ensure matches DB type

        // One-to-One Relationship
        builder.HasOne(f => f.Reservation)
            .WithOne(r => r.Feedback)
            .HasForeignKey<Feedback>(f => f.ReservationId);
    }
}
