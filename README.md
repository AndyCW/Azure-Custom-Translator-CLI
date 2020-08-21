# Azure Custom Translator Manager CLI

- [Azure Custom Translator Manager CLI](#azure-custom-translator-manager-cli)
  - [Installation](#installation)
    - [Installing the tool](#installing-the-tool)
    - [Configuring required resources in Azure](#configuring-required-resources-in-azure)
      - [Creating the App registration](#creating-the-app-registration)
      - [Creating the Azure Key Vault](#creating-the-azure-key-vault)
    - [Setting CLI tool configuration](#setting-cli-tool-configuration)
      - [Using an appSettings.config configuration file](#using-an-appsettingsconfig-configuration-file)
      - [Setting environment variables](#setting-environment-variables)
  - [Usage](#usage)
    - [Using `translator config` to set your Azure Translator resource key](#using-translator-config-to-set-your-azure-translator-resource-key)
    - [First time authentication](#first-time-authentication)
    - [Help](#help)
    - [Entity operations](#entity-operations)
    - [Wait](#wait)
    - [JSON](#json)
  - [Example workflow](#example-workflow)
  - [Using the CLI tool in a DevOps workflow](#using-the-cli-tool-in-a-devops-workflow)

Azure Custom Translator Manager CLI is an unofficial command-line tool for [Microsoft Azure Cognitive Services Custom Translator](https://docs.microsoft.com/azure/cognitive-services/translator/custom-translator/overview) management -workspaces, projects, models, tests, endpoints etc. Useful especially for automation and CI/CD.

![Build status](https://dev.azure.com/andycw/Azure-Custom-Translator-CLI/_apis/build/status/Azure-Custom-Translator-CLI-GitHub)

This tool is using [Custom Translator API Preview v1.0](https://custom-api.cognitive.microsofttranslator.com/swagger/). SDK was generated automatically from the Swagger definition using [AutoRest](https://github.com/Azure/AutoRest), but many adjustments had to be made to the generated code. If you want to change the source and build your own version of the tool and you regenerate the SDK with AutoRest, significant rework will be required to make the solution build.

## Installation

To use the tool, you must:

- Install the tool on your system
- Configure Azure services required by the tool
- Set configuration parameters for the tool.

### Installing the tool

With .NET Core installed just run:

```bash
dotnet tool install -g azure-translator-cli
```

Alternatively, you can go to [Releases](https://github.com/andycw/Azure-Custom-Translator-CLI/releases) and download a compiled version for your operating system, or build directly from sources.

> CLI is created with .NET Core and builds are currently running for Windows, MacOS and Linux.
>

### Configuring required resources in Azure

The Custom Translator CLI requires that you setup the following in Azure:

-Register an application at the Microsoft App Registration portal
-Create an Azure Key Vault for the CLI tool to use to store authentication tokens

You will then update the CLI configuration so that it can use these resources.

#### Creating the App registration

The CLI tool must authenticate against your organisations Azure Active Directory each time you use it to get an access token that is validated by the Azure Custom Translation service. You must register an application at the Microsoft App Registration portal to enable this:

1. Go to [https://ms.portal.azure.com/#blade/Microsoft_AAD_RegisteredApps/ApplicationsListBlade](https://ms.portal.azure.com/#blade/Microsoft_AAD_RegisteredApps/ApplicationsListBlade).
1. Click **+ New Registration**.
1. Enter the name of your application (e.g. **MyTranslatorCLI**) and select account type. Use **Accounts in this organizational directory only**.
1. In the **Redirect URI**, select **Public client/native (mobile & desktop)*-in the dropdown and enter **<http://localhost>*-as the URI.
1. Click **Register**.
1. Note the following values displayed on the Overview tab which you will need to configure your CLI tool installation:
   - **Application (client) ID**
   - **Directory (tenant) ID**
1. Now click on **Certificates & secrets*-under Manage in the left menu options.
   1. Under *Client Secrets*, click **+ New client secret**.
   1. Enter a description and select your required expiry period (Use *Never-if unsure).
   1. Click **Add**.
   1. Note down the CLI secret. You will not be able to see the secret value once you leave this blade, although you can generate a new value.

#### Creating the Azure Key Vault

The CLI tool uses an Azure Key Vault instance to store the client access token and refresh token. You must configure the Key Vault now:

1. In the Azure Portal home page, click **+ Create a resource**.
1. Search for **Key Vault**. On the *Key Vault-resource page, click **Create**.
   - Select your subscription and enter the **Resource group*-you want the Key Vault to be created in.
   - Enter the **Name*-and select the **Region**, and the **Standard*-pricing tier.
   - Click **Review + create*-and then click **Create**.
1. When the Key Vault is created, go to the new Key Vault resource, and click on **Access policies*-under *Settings-in the menu.
   - Click **+ Add Access Policy**.
   - Click on the **Secret permissions*-dropdown and select:
      -**Get**
      -**List**
      -**Set**
   - Click the *None selected-link next to **Select principal**.
   - In the search box on the **Principal*-selection pane, enter the *Application (client) ID-for the new Application you created in the previous step. Select the application when it shows up.
   - Click **Add**.
1. From the *Overview-tab, note the **DNS Name*-of your Key Vault (e.g. *<https://mytranslatorkv.vault.azure.net/>*) which you will need to configure your Custom Translator CLI tool.

### Setting CLI tool configuration

Now you have all the values needed, you need to configure the tool so that it can use them. There are two ways you can do this:

-Entering the values in an app.config configuration file
-Saving the values as environment variables

#### Using an appSettings.config configuration file

Go to the folder where .NET Core global tools are installed. Global tools are installed in the following directories by default when you install using the `-g` or `--global` option:

| OS | Path |
|---|-----|
| Linux/macOS | `$HOME/.dotnet/tools` |
| Windows | `%USERPROFILE%\.dotnet\tools` |

- Beneath that folder, navigate to the *\.store\custom-translator-cli\<version>\custom-translator-cli\<version>\tools\netcoreapp3.1\any-folder.
- Rename the **appSettings.sample.config*-file to **appSettings.config**, and enter the values you collected above:

```xml
{
  "TRANSLATOR_VAULT_URI": "your-keyvault-DNS-hostname",
  "AZURE_CLIENT_ID": "Application (client) ID",
  "AZURE_TENANT_ID": "Directory (tenant) ID",
  "AZURE_CLIENT_SECRET": "Application client secret"
}
```

- Save your changes.

#### Setting environment variables

The CLI tool can get the configuration values it needs from environment variables instead of from an appSettings.config file. Set the following environment variables:

- **TRANSLATOR_VAULT_URI**: *your-keyvault-DNS-hostname*
- **AZURE_CLIENT_ID**: *Application (client) ID*
- **AZURE_TENANT_ID**: *Directory (tenant) ID*
- **AZURE_CLIENT_SECRET**: *Application client secret*

## Usage

Before using the tool to manage workspaces, projects, documents and models, you need to set your Custom Translator service credentials.

### Using `translator config` to set your Azure Translator resource key

Set your Custom Translator service credentials as follows

```bash
translator config set --name Project1 --key ABCD12345 --region global --select
```

Or shorter version:

```bash
translator config set -n Project2 -k ABCD54321 -s
```

Both commands store your credentials as a configuration set and automatically make these credentials selected (by using the `--select` parameter). You can have multiple sets and switch between them:

```bash
translator config select Project1
```

This can be useful when you work with multiple subscriptions.

### First time authentication

The first time you use any **translator*-command *other than-**config**, for example, *translator workspace list*, the tool will launch a browser window and you must sign into Azure using the subscription you used to configure the Azure resources for the tool.

This is a one-time requirement and is required to get the authentication token and refresh token that the tool uses when it authenticates against your Azure Active Directory everytime you use the tool thereafter. The tool stores the authentication token and refresh token in the Azure Key Vault that you configured earlier. If you manually delete the entry in your Azure Key Vault, the next time you use the translator CLI tool, you will be required to sign in again.

### Help

If you're not sure what commands and parameters are available, try adding `--help` to the command you want to use.

For example:

```bash
translator --help
translator model --help
translator document upload --help
```

### Entity operations

Every entity (workspace|project|document|model) supports basic set of operations:

- `create`
- `list`
- `show`
- `delete`

When working with a specific entity, ID is usually required:

```bash
translator project list -ws <GUID>
translator model delete --modelid <Int64>
```

### Wait

Every *create-command offers optional `--wait` (`-w`) flag which makes the CLI block and wait for the create operation to complete (dataset processed, model trained, endpoint provisioned etc.). When new entity is created, it writes corresponding ID to console.

This is useful in automation pipelines when commands are run as individual steps in a complex process.

```bash
translator model create -p 00000000-0000-0000-0000-000000000000 -n testmodel -d 1,2 --train --wait

Creating model...
Processing [..............]
1234       testmodel                                          trained
```

### JSON

Every command offers optional `--json` (`-j`) flag which forces the output from the CLI to be formatted as JSON.

This is useful in automation pipelines when the output from commands need to be parsed to determine status.

```bash
translator model create -p 00000000-0000-0000-0000-000000000000 -n testmodel -d 1,2 --train --wait --json

Creating model...
Processing [.........] Done
{
  "id": 1234,
  "name": "testmodel",
  "modelIdentifier": null,
  "projectId": "00000000-0000-0000-0000-000000000000",
  "documents": null,
  "modelRegionStatuses": null,
  "baselineBleuScorePunctuated": null,
  "bleuScorePunctuated": null,
  "baselineBleuScoreUnpunctuated": null,
  "bleuScoreUnpunctuated": null,
  "baselineBleuScoreCIPunctuated": null,
  "bleuScoreCIPunctuated": null,
  "baselineBleuScoreCIUnpunctuated": null,
  "bleuScoreCIUnpunctuated": null,
  "startDate": null,
  "completionDate": null,
  "modifiedDate": "0001-01-01T00:00:00",
  "createdDate": "0001-01-01T00:00:00",
  "createdBy": null,
  "modifiedBy": null,
  "trainingSentenceCount": null,
  "tuningSentenceCount": null,
  "testingSentenceCount": null,
  "phraseDictionarySentenceCount": null,
  "sentenceDictionarySentenceCount": null,
  "monolingualSentenceCount": null,
  "modelStatus": "trained",
  "statusInfo": null,
  "isTuningAuto": false,
  "isTestingAuto": false,
  "isAutoDeploy": false,
  "autoDeployThreshold": 0.0,
  "hubBLEUScore": null,
  "hubCategory": null,
  "errorCode": null
}
```

## Example workflow

The following is an example of a typical workflow using the CLI. Note that output from the commands is not shown.

Start by setting your configuration for your translator subscription key:

```bash
translator config set --name Project1 --key ABCD12345 --select
```

Then you may wish to see the existing workspaces:

```bash
translator workspace list
```

You can use the CLI to create a new workspace, or work within an existing one. Specify the ID of the workspace when creating a new project:

```bash
translator project create -ws <GUID>
```

Next upload documents to the workspace:

```bash
translator document upload -ws <GUID> -lp en:es -dt training -c abc.xlsx
```

List documents to get their IDs:

```bash
translator document list -ws <GUID>
```

Create a model in your project, specifying the document ID(s) and weather you want to train it immediately:

```bash
translator model create -p <GUID> -n myNewModel -d 12 -w -t
```

Deploy the model:

```bash
translator model deploy -m <Id>
```

## Using the CLI tool in a DevOps workflow

When using the CLI in a devops pipeline, there are a few things to remember:

- The one-time authentication in a browser described above in [First Time Authentication](#first-time-authentication) will not work when the tool is used in a pipeline. To get around this it is essential that you **run the CLI once interactively*-on any machine, using a command such as *translator workspace list*.  
This causes the authentication tokens to be cached in Azure Key Vault where the instance of the tool running in a pipeline will be able to find them, so it won't try to put up a browser window.

- Set environment variables or secret variables in your pipeline for the app configuration values. It is recommended that you set them as secrets so that they are not visible in pipeline build logs:
  -**TRANSLATOR_VAULT_URI**: *your-keyvault-DNS-hostname*
  -**AZURE_CLIENT_ID**: *Application (client) ID*
  -**AZURE_TENANT_ID**: *Directory (tenant) ID*
  -**AZURE_CLIENT_SECRET**: *Application client secret*

- Example pipeline step if you are using *GitHub Actions*, where the configuration values above have been defined as **GitHub Secrets*-in your repo:
  
   ```bash
    - name: set config
      run: translator config set -n default -k b12d1367695f403a9abcdefghijk -r global -s

    - name: Get workspaces
      run: translator workspace list
      env:
        AZURE_CLIENT_ID: ${{ secrets.AZURE_CLIENT_ID }}
        AZURE_TENANT_ID: ${{ secrets.AZURE_TENANT_ID }}
        AZURE_CLIENT_SECRET: ${{ secrets.AZURE_CLIENT_SECRET }}
        TRANSLATOR_VAULT_URI: ${{ secrets.TRANSLATOR_VAULT_URI }}
   ```

-----

Contributions to this project are welcome. By participating in this project, you
agree to abide by the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
