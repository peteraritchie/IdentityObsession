using Ardalis.Result;

namespace Pri.IdentityObsession.Domain;

public sealed class SsnRegistry : ISsnRegistry
{
	public Result<ISsnReservation> Reserve()
	{
		var area = Random.Shared.Next(1, 998);
		var group = Random.Shared.Next(1, 98);
		var serial = Random.Shared.Next(1, 9998);
		return new SsnReservation($"{area:D3}-{group:D2}-{serial:D4}");
	}

	public Result Commit(ISsnReservation reservation)
	{
		reservation.Dispose();
		return Result.Success();
	}
}

