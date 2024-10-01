using Ardalis.Result;

namespace Pri.IdentityObsession.Domain;

public interface IClientRepository
{
	Task<Result<Client>> FindBySsnAsync(Ssn ssn, CancellationToken cancellationToken);
	Task<Result> SaveAsync(Client client, CancellationToken cancellationToken);
	Task<Result<Ssn>> AddAsync(Client client, CancellationToken cancellationToken);
	Task<Result<IEnumerable<Client>>> FindClientsAsync(CancellationToken cancellationToken);
	Result<Ssn> GetClientSsn(Client client);
	Task<Result<Client>> FindByIdAsync(Guid id, CancellationToken cancellationToken);
}
