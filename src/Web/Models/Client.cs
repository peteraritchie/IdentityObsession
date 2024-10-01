namespace Pri.IdentityObsession.Web.Models;

public class Client
{
	public required string? GivenName { get; set; }
	public required string? FamilyName { get; set; }
	public string? Ssn { get; set; }
}