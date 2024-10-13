using Ardalis.Result;

namespace Pri.IdentityObsession.Domain;

internal interface ISsnReservation : IDisposable
{
	Result Commit();
	void Release();
	string Value { get; }
}