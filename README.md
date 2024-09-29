# IdentityObsession

## Scaffolding

```powershell
dotnet new solution -o 'IdentityObsession\src' -n IdentityObsession
dotnet new gitignore -o 'IdentityObsession\src'
dotnet new buildprops -o 'IdentityObsession\src'
dotnet new classlib -n Pri.IdentityObsession.Domain -o 'IdentityObsession\src\Pri.IdentityObsession.Domain' --framework net8.0 --language 'C#'
dotnet sln 'IdentityObsession\src' add 'C:\Users\peter\src\experiment\IdentityObsession\src\Domain'
dotnet new class -n 'Person' -o 'C:\Users\peter\src\experiment\IdentityObsession\src\Domain'
dotnet new class -n 'Client' -o 'C:\Users\peter\src\experiment\IdentityObsession\src\Domain'
dotnet new class -n 'Ssn' -o 'C:\Users\peter\src\experiment\IdentityObsession\src\Domain'
dotnet new class -n 'SsnRegistry' -o 'C:\Users\peter\src\experiment\IdentityObsession\src\Domain'
dotnet new class -n 'SsnReservation' -o 'C:\Users\peter\src\experiment\IdentityObsession\src\Domain'
dotnet new interface -n 'IClientRepository' -o 'C:\Users\peter\src\experiment\IdentityObsession\src\Domain'
dotnet new interface -n 'ISsnRegistry' -o 'C:\Users\peter\src\experiment\IdentityObsession\src\Domain'
dotnet new interface -n 'ISsnReservation' -o 'C:\Users\peter\src\experiment\IdentityObsession\src\Domain'
dotnet add 'C:\Users\peter\src\experiment\IdentityObsession\src\Domain' package 'Ardalis.Result'
dotnet new blazor -n Pri.IdentityObsession.Web -o 'IdentityObsession\src\Pri.IdentityObsession.Web' --framework net8.0 --language 'C#'
dotnet sln 'IdentityObsession\src' add 'C:\Users\peter\src\experiment\IdentityObsession\src\Web'
dotnet add 'C:\Users\peter\src\experiment\IdentityObsession\src\Web' reference 'C:\Users\peter\src\experiment\IdentityObsession\src\Domain'
dotnet add 'C:\Users\peter\src\experiment\IdentityObsession\src\Web' package 'AutoMapper'
dotnet add 'C:\Users\peter\src\experiment\IdentityObsession\src\Web' package 'Microsoft.EntityFrameworkCore.Sqlite'
dotnet add 'C:\Users\peter\src\experiment\IdentityObsession\src\Web' package 'Microsoft.EntityFrameworkCore.Design'
dotnet new class -n 'ClientRepository' -o 'C:\Users\peter\src\experiment\IdentityObsession\src\Web\Infrastructure'
dotnet new class -n 'DatabaseContext' -o 'C:\Users\peter\src\experiment\IdentityObsession\src\Web\Infrastructure'
dotnet new class -n 'ClientEntityTypeConfiguration' -o 'C:\Users\peter\src\experiment\IdentityObsession\src\Web\Infrastructure'
dotnet new xunit -n Pri.IdentityObsession.Tests -o 'IdentityObsession\src\Pri.IdentityObsession.Tests' --framework net8.0 --language 'C#'
dotnet sln 'IdentityObsession\src' add 'C:\Users\peter\src\experiment\IdentityObsession\src\Tests'
dotnet add 'C:\Users\peter\src\experiment\IdentityObsession\src\Tests' package 'Moq'
dotnet add 'C:\Users\peter\src\experiment\IdentityObsession\src\Tests' reference 'C:\Users\peter\src\experiment\IdentityObsession\src\Web'
dotnet add 'C:\Users\peter\src\experiment\IdentityObsession\src\Tests' package 'Microsoft.AspNetCore.Mvc.Testing'
```
