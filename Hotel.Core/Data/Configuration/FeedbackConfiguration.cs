using Hotel.Core.Entities.FeedbackModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.ToTable("Feedbacks");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Comment)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(f => f.Rating)
            .IsRequired();




    }
}
