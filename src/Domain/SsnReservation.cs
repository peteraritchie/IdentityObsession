using Ardalis.Result;

namespace Pri.IdentityObsession.Domain;

public sealed class SsnReservation(string value) : ISsnReservation
{
	private bool disposed;

	public void Dispose()
	{
		if (disposed) return;
		Release();
	}

	public Result Commit()
	{
		disposed = true;
		return Result.Success();
	}

	public void Release()
	{
		disposed = true;
	}

	public string Value { get; } = value;
}
