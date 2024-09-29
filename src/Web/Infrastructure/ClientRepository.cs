using Ardalis.Result;

using Microsoft.EntityFrameworkCore;

using Pri.IdentityObsession.Domain;

namespace Pri.IdentityObsession.Web.Infrastructure;

public class ClientRepository(DatabaseContext dbContext, ISsnRegistry ssnRegistry) : IClientRepository
{
	private readonly DatabaseContext dbContext = dbContext;
	private readonly ISsnRegistry ssnRegistry = ssnRegistry;

	public async Task<Result> SaveAsync(Client client, CancellationToken cancellationToken)
	{
		var entry = dbContext.Entry(client);
		switch (entry.State)
		{
			case EntityState.Detached:
				{
					var addResult = await AddAsync(client, cancellationToken);
					return addResult.IsSuccess
						? Result.Success()
						: Result.CriticalError(addResult.Errors.ToArray());
				}
			case EntityState.Modified:
				await dbContext.SaveChangesAsync(cancellationToken);
				return Result.Success();
			default:
				return Result.Success();
		}
	}

	public Task<Result<Client>> FindByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		return dbContext.GetClientByIdAsync(id, cancellationToken);
	}

	public Task<Result<Client>> FindBySsnAsync(Ssn ssn, CancellationToken cancellationToken)
	{
		return dbContext.GetClientBySsnAsync(ssn, cancellationToken);
	}

	public async Task<Result<Ssn>> AddAsync(Client client, CancellationToken cancellationToken)
	{
		var reservationResult = ssnRegistry.Reserve();
		if (reservationResult.IsError()) return Result<Ssn>.Error();

		using var ssnReservation = reservationResult.Value;
		Ssn newSsn = new(ssnReservation.Value);
		await dbContext.AddClientAsync(client, newSsn, cancellationToken);
		await dbContext.SaveChangesAsync(cancellationToken);
		ssnReservation.Commit();
		return newSsn;
	}
}