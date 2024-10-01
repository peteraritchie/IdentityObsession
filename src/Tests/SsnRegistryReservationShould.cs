using Microsoft.VisualStudio.TestPlatform.TestHost;

using Moq;

using Pri.IdentityObsession.Domain;

namespace Pri.IdentityObsession.Tests;

public class SsnRegistryReservationShould
{
	[Fact]
	public void Succeed()
	{
		var registry = new SsnRegistry();
		var reservationResult = registry.Reserve();

		Assert.True(reservationResult.IsSuccess);
	}

	[Fact]
	public void CreateValidSsn()
	{
		var registry = new SsnRegistry();
		var reservationResult = registry.Reserve();
		Assert.True(reservationResult.IsSuccess);
		using var reservation = reservationResult.Value;

		Assert.Matches(@"^(?!0{3})(?!6{3})[0-8]\d{2}-(?!0{2})\d{2}-(?!0{4})\d{4}$", reservation.Value);
	}
}