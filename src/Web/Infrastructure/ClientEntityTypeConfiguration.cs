using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Pri.IdentityObsession.Domain;

namespace Pri.IdentityObsession.Web.Infrastructure;

public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
{
	public void Configure(EntityTypeBuilder<Client> builder)
	{
		// Create a string "Id" Shadow Property to hold GUID values
		builder.Property<string>(ColumnNames.Id)
			.HasColumnType("varchar(36)")
			.HasMaxLength(36);
		builder.HasKey(ColumnNames.Id);

		// Create a string "Ssn" Shadow Property that uses the type Sss,
		// with conversion
		builder.Property<Ssn>(ColumnNames.Ssn)
			.HasColumnType("varchar(11)")
			.HasMaxLength(11)
			.HasConversion(ssn => ssn.ToString(), value => Ssn.Parse(value))
			.IsRequired();

		// Seed data
		builder.HasData(
			new
			{
				GivenName = "Nhoj",
				FamilyName = "Eod",
				Id = Guid.NewGuid().ToString(),
				Ssn = new Ssn("999-00-1234")
			}
		);
	}
}