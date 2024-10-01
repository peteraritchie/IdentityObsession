using Ardalis.Result;

using Pri.IdentityObsession.Domain;
using Pri.IdentityObsession.Web.Infrastructure;

using Client = Pri.IdentityObsession.Web.Models.Client;

namespace Pri.IdentityObsession.Web.Services;

public class ClientService(IClientRepository clientRepository)
{
	public async Task<IEnumerable<Client>> GetClientsAsync(CancellationToken cancellationToken = default)
	{
		var domainClientsResult = await clientRepository.FindClientsAsync(cancellationToken);
		if (!domainClientsResult.IsSuccess)
		{
			return [];
		}

		var domainClients = domainClientsResult.Value;
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

	private static Domain.Client ToDomain(Client model)
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