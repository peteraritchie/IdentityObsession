using Ardalis.Result;

namespace Pri.IdentityObsession.Domain;

public interface ISsnRegistry
{
	Result<ISsnReservation> Reserve();
	Result Commit(ISsnReservation reservation);
}
