using Architecture.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.Database
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User), nameof(User));
            builder.HasKey(user => user.Id);
            builder.Property(user => user.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(user => user.Status).IsRequired();
            builder.OwnsOne(user => user.Name, userName =>
            {
                userName.Property(name => name.Forename).HasColumnName(nameof(Name.Forename)).HasMaxLength(100).IsRequired();
                userName.Property(name => name.Surname).HasColumnName(nameof(Name.Surname)).HasMaxLength(200).IsRequired();
            });
            builder.OwnsOne(user => user.Email, userEmail =>
            {
                userEmail.Property(email => email.Value).HasColumnName(nameof(User.Email)).HasMaxLength(300).IsRequired();
                userEmail.HasIndex(email => email.Value).IsUnique();
            });
            builder.HasOne(user => user.Auth);
        }
    }
}
