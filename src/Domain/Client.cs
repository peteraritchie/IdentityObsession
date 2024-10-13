namespace Pri.IdentityObsession.Domain;

internal class Client(string givenName, string familyName)
	: Person(givenName, familyName)
{
	public void ChangeName(string givenName, string familyName)
	{
		GivenName = givenName;
		FamilyName = familyName;
	}
}