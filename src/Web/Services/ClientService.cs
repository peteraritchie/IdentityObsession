using Ardalis.Result;

using Microsoft.EntityFrameworkCore;

using Pri.IdentityObsession.Domain;
using Pri.IdentityObsession.Web.Infrastructure;

using Client = Pri.IdentityObsession.Web.Models.Client;

namespace Pri.IdentityObsession.Web.Services;

public class ClientService1(IDbContextFactory<DatabaseContext> contextFactory, ISsnRegistry ssnRegistry)
{
	public async Task<IEnumerable<Client>> GetClientsAsync(CancellationToken cancellationToken = default)
	{
		await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
		return await context.Clients.Select(e => ToModel(context, e)).ToListAsync(cancellationToken);
	}

	private static Client ToModel(DatabaseContext context, Domain.Client domain)
	{
		var ssnResult = context.GetClientSsn(domain);
		return new Client
		{
			GivenName = domain.GivenName,
			FamilyName = domain.FamilyName,
			Ssn = ssnResult.IsSuccess ? ssnResult.Value.Value : "unassigned"
		};
	}

	public async Task<Result<Ssn>> AddAsync(Client client, CancellationToken cancellationToken = default)
	{
		var domainClient = new Domain.Client(client.GivenName!, client.FamilyName!);
		var context = await contextFactory.CreateDbContextAsync(cancellationToken);
		var reservationResult = ssnRegistry.Reserve();
		if (reservationResult.IsError()) return Result<Ssn>.Error();

		using var ssnReservation = reservationResult.Value;
		Ssn newSsn = new(ssnReservation.Value);
		await context.AddClientAsync(domainClient, newSsn, cancellationToken);
		await context.SaveChangesAsync(cancellationToken);
		ssnReservation.Commit();
		return newSsn;
	}
}
public class ClientService(IClientRepository clientRepository)
{
	public async Task<IEnumerable<Client>> GetClientsAsync(CancellationToken cancellationToken = default)
	{
		var domainClients = await clientRepository.FindClientsAsync(cancellationToken);
		return domainClients.Select(ToModel);
	}

	private Client ToModel(Domain.Client domain)
	{
		var ssnResult = clientRepository.GetClientSsn(domain);
		return new Client
		{
			GivenName = domain.GivenName,
			FamilyName = domain.FamilyName,
			Ssn = ssnResult.IsSuccess ? ssnResult.Value.Value : "unassigned"
		};
	}

	private Domain.Client ToDomain(Client model)
	{
		if (model.GivenName is null) throw new ArgumentException("GivenName is null", nameof(model));
		if (model.FamilyName is null) throw new ArgumentException("FamilyName is null", nameof(model));

		return new Domain.Client(model.GivenName, model.FamilyName);
	}

	public async Task<Result<Ssn>> AddAsync(Client client, CancellationToken cancellationToken = default)
	{
		return await clientRepository.AddAsync(ToDomain(client), cancellationToken);
	}
}