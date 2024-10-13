namespace Pri.IdentityObsession.Domain;

internal abstract class Person(string givenName, string familyName)
{
	public string GivenName { get; protected set; } = givenName;
	public string FamilyName { get; protected set; } = familyName;
}