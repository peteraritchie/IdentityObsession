using Ardalis.Result;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Pri.IdentityObsession.Domain;

namespace Pri.IdentityObsession.Web.Infrastructure;

public class DatabaseContext : DbContext
{
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new ClientEntityTypeConfiguration());

		base.OnModelCreating(modelBuilder);
	}

	public async Task<Result<Client>> GetClientBySsnAsync(Ssn ssn, CancellationToken cancellationToken)
	{
		var client = await Clients
			.SingleOrDefaultAsync(c => EF.Property<Ssn>(c, ColumnNames.Ssn) == ssn, cancellationToken);

		return client ?? Result<Client>.NotFound();
	}

	public async Task<Result<Client>> GetClientByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var client = await Clients
			.SingleOrDefaultAsync(c => EF.Property<string>(c, ColumnNames.Id) == id.ToString(), cancellationToken);

		return client ?? Result<Client>.NotFound();
	}

	public Result<Ssn> GetClientSsn(Client client)
	{
		var entry = Entry(client);
		var currentValue = entry.Property(ColumnNames.Ssn).CurrentValue as Ssn;

		return currentValue ?? Result<Ssn>.NotFound();
	}

	public async Task AddClientAsync(Client client, Ssn ssn, CancellationToken cancellationToken)
	{
		var entry = Entry(client);
		entry.Property(ColumnNames.Ssn).CurrentValue = ssn;
		entry.Property(ColumnNames.Id).CurrentValue = Guid.NewGuid().ToString();
		await AddAsync(client, cancellationToken);

		await SaveChangesAsync(cancellationToken);
	}
	public DbSet<Client> Clients { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite($"Data Source={DataSourcePath}");
	}

	public static string DataSourcePath {
		get
		{
			var localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			return Path.Join(localAppDataPath, "clients.db");
		}
	}
}