namespace Pri.IdentityObsession.Domain;

public partial class Ssn
{
	public static Ssn Parse(string? text)
	{
		ArgumentNullException.ThrowIfNull(text);
		return new Ssn(text);
	}
}