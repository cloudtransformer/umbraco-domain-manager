# AgeBase: Domain Manager

The Domain Manager package is...

## Installation

The Domain Manager package can be installed via the package's page on [our.umbraco.org](http://our.umbraco.org/projects/backoffice-extensions/domain-manager) or via NuGet. If installing via NuGet, use the following package manager command:

    Install-Package AgeBase.DomainManager

## Configuration



## Usage



## Contributing

To raise a new bug, create an [issue](https://github.com/agebase/umbraco-domain-manager/issues) on the Github repository. To fix a bug or add new features or providers, fork the repository and send a [pull request](https://github.com/agebase/umbraco-domain-manager/pulls) with your changes. Feel free to add ideas to the repository's [issues](https://github.com/agebase/umbraco-domain-manager/issues) list if you would to discuss anything related to the package.

## Publishing

Remember to include all necessary files within the package.xml file. Run the following script, entering the new version number when prompted to created a published version of the package:

    Build\Release.bat

The release script will amend all assembly versions for the package, build the solution and create the package file. The script will also commit and tag the repository accordingly to reflect the new version.