using Ardalis.Result;

namespace Pri.IdentityObsession.Domain;

public interface ISsnReservation : IDisposable
{
	Result Commit();
	void Release();
	string Value { get; }
}