. $env:USERPROFILE\project.ps1

$solutionName = 'IdentityObsession';
$rootDir = $solutionName;
$srcDir = Join-Path $solutionName 'src';

if(Test-Path $rootDir) {
    throw "Directory `"$rootDir`" already exists!";
}
$rootNamespace = "Pri.$solutionName";

# supporting files ############################################################

dotnet new gitignore -o $rootDir 2>&1 >NUL || $(throw 'error creating .gitignore');
dotnet new editorconfig -o $srcDir 2>&1 >NUL || $(throw 'error creating .editorconfig');
$text = (get-content -Raw $srcDir\.editorconfig);
$text = ($text -replace "(\[\*\]`r`nindent_style\s*=\s)space","`$1tab");
$text = $text.Replace('dotnet_naming_rule.private_fields_should_be__camelcase.style = _camelcase', 'dotnet_naming_rule.private_fields_should_be_camelcase.style = camelcase');
$text = $text.Replace('dotnet_naming_rule.private_fields_should_be__camelcase', 'dotnet_naming_rule.private_fields_should_be_camelcase');
$text = $text.Replace('dotnet_naming_rule.private_fields_should_be__camelcase', 'dotnet_naming_rule.private_fields_should_be_camelcase');
Set-Content -Path $srcDir\.editorconfig -Value $text;

$solution = [Solution]::Create($srcDir, $solutionName);

# domain project ##############################################################
$domainProject = $solution.NewClassLibraryProject("$($rootNamespace).Domain");
$domainProject.AddClass('Person');
$domainProject.AddClass('Client');
$domainProject.AddClass('Ssn');
$domainProject.AddClass('SsnRegistry');
$domainProject.AddClass('SsnReservation');
$domainProject.AddInterface('IClientRepository');
$domainProject.AddInterface('ISsnRegistry');
$domainProject.AddInterface('ISsnReservation');
$domainProject.AddPackageReference('Ardalis.Result');

# main/application project ####################################################
$applicationProject = $solution.NewBlazorProject("$($rootNamespace).Web", $domainProject);
$applicationProject.AddPackageReference('AutoMapper');
# 		builder.Services.AddAutoMapper(typeof(Mappings));
#       builder.Services.AddSingleton<IClientRepository, ClientRepository>();
#       builder.Services.AddSingleton<DatabaseContext>();
$applicationProject.AddPackageReference('Microsoft.EntityFrameworkCore.Sqlite');
$applicationProject.AddPackageReference('Microsoft.EntityFrameworkCore.Design');
# need ability to create class in namespace/directory
$applicationProject.AddClass('Infrastructure\ClientRepository');
$applicationProject.AddClass('Infrastructure\DatabaseContext');
$applicationProject.AddClass('Infrastructure\ClientEntityTypeConfiguration');

# tests project ###############################################################
$testProject = $solution.NewTestProject("$($rootNamespace).Tests", $applicationProject);
$testProject.AddPackageReference('Microsoft.AspNetCore.Mvc.Testing');

## Create readme ##############################################################
Set-Content -Path $rootDir\README.md -Value "# $solutionName`r`n`r`n## Scaffolding`r`n`r`n``````powershell";

foreach($cmd in $solution.ExecutedCommands)
{
    Add-Content -Path $rootDir\README.md -Value $cmd;
}
Add-Content -Path $rootDir\README.md -Value ``````;

###############################################################################

md "$($rootDir)\scaffolding";
copy scaffold-identityObsession.ps1 "$($rootDir)\scaffolding";

copy .\idob-files\Client.cs $srcDir\Domain\Client.cs
copy .\idob-files\IClientRepository.cs $srcDir\Domain\IClientRepository.cs
copy .\idob-files\Person.cs $srcDir\Domain\Person.cs
copy .\idob-files\Ssn.cs $srcDir\Domain\Ssn.cs
copy .\idob-files\Ssn.Factory.cs $srcDir\Domain\Ssn.Factory.cs
copy .\idob-files\Ssn.Object.cs $srcDir\Domain\Ssn.Object.cs
copy .\idob-files\SsnRegistry.cs $srcDir\Domain\SsnRegistry.cs
copy .\idob-files\SsnReservation.cs $srcDir\Domain\SsnReservation.cs
copy .\idob-files\SsnRegistryReservationShould.cs $srcDir\Tests\SsnRegistryReservationShould.cs
copy .\idob-files\ClientEntityTypeConfiguration.cs $srcDir\Web\Infrastructure\ClientEntityTypeConfiguration.cs
copy .\idob-files\ClientRepository.cs $srcDir\Web\Infrastructure\ClientRepository.cs
copy .\idob-files\ColumnNames.cs $srcDir\Web\Infrastructure\ColumnNames.cs
copy .\idob-files\DatabaseContext.cs $srcDir\Web\Infrastructure\DatabaseContext.cs

# git init ####################################################################
git init $rootDir;
git --work-tree=$rootDir --git-dir=$rootDir/.git add .;
git --work-tree=$rootDir --git-dir=$rootDir/.git commit -m "initial commit";


# dotnet ef migrations add InitialCreate && dotnet ef database update

# dotnet ef migrations add InitialCreate
# dotnet ef database update
# rmdir -r -force .\Migrations\; del $env:LOCALAPPDATA\clients.db