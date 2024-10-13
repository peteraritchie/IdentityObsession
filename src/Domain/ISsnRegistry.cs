using Ardalis.Result;

namespace Pri.IdentityObsession.Domain;

internal interface ISsnRegistry
{
	Result<ISsnReservation> Reserve();
	Result Commit(ISsnReservation reservation);
}
