using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommentManagement.Infrastructure.EFCore.Mapping
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(250);
            builder.Property(x => x.Email).HasMaxLength(250);
            builder.Property(x => x.Website).HasMaxLength(250);
            builder.Property(x => x.Message).HasMaxLength(500);

            //Define a one-to-many relationship between Comment Parent and Comment Children
            builder.HasOne(x => x.Parent)
                        .WithMany(x => x.Children)
                            .HasForeignKey(x => x.ParentId)
                                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
