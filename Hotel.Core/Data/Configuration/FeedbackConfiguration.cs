using Hotel.Core.Entities.FeedbackModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Data.Configuration
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.ToTable("Feedbacks");

            builder.HasKey(f => f.FeedbackId);

            builder.Property(f => f.Comment)
                .HasMaxLength(500)
                .IsRequired(false); // Comment is optional

            builder.Property(f => f.Rating)
                .IsRequired();

            // One-to-One Relationship
            builder.HasOne(f => f.Reservation)
                .WithOne(r => r.Feedback)
                .HasForeignKey<Feedback>(f => f.ReservationId);
        }
    }
}
