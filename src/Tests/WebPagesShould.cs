using Microsoft.AspNetCore.Mvc.Testing;

namespace Pri.IdentityObsession.Tests;

public class WebPagesShould(WebApplicationFactory<Program> factory)
	: IClassFixture<WebApplicationFactory<Program>>
{
	[Theory]
	[InlineData("/")]
	[InlineData("/counter")]
	[InlineData("/weather")]
	public async Task HaveSuccessResultForEndpoint(string url)
	{
		var client = factory.CreateClient();

		var response = await client.GetAsync(url);

		response.EnsureSuccessStatusCode();

		Assert.Equal(System.Net.Mime.MediaTypeNames.Text.Html, response.Content.Headers.ContentType!.MediaType);
	}
}