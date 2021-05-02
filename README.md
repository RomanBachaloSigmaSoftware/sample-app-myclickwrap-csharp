# ASP.NET Core 3.1 and React: MyClickwrap Sample Application

### GitHub repo: sample-app-myclickwrap-csharp
Visit [MyClickwrap](https://myclickwrap.sampleapps.docusign.com/myclickwrap/) on DocuSign to see this code publicly hosted

## Introduction
MyClickwrap is a sample application written in ASP.NET Core 3.1 (server) and React 9 (client). You can find a live instance running at [https://github.com/docusign/sample-app-myclickwrap-csharp](https://github.com/docusign/sample-app-myclickwrap-csharp).

MyClickwrap contains three different projects for three Click API use cases bundled into a single solution.
1. **COVID-19 Health Declaration:** This scenario shows how the Click API can be used to build a simple tool to enable organizations to capture consent from individuals releasing the organization from responsibility in case they contract COVID-19 while on their premises. 
2. **Nondisclosure Agreement (NDA):** This scenario shows how the Click API can be used for visitors to sign an NDA before they can attend a meeting (similar to a Lobby application). The visitor has to provide their email address, so that is used to secure the agreement and call it server-side.
3. **Terms and Conditions.** This scenario shows how the Click API can be used to enable your application to capture users' consent to terms and conditions and a privacy policy.

Scenarios showcase Click functionality and using the Click API. Each Scenario demonstrates the following:

1. Authentication with [JSON Web Token (JWT) Grant](https://developers.docusign.com/platform/auth/jwt/).
2. Usng the Click API to create a clickwrap programmatically, render it in your UI, and then submit it. Once the user submits the form, the app uses a clickwrap to present information related to the scenario and request confirmation.  
    * [More information about the Click API](https://developers.docusign.com/docs/click-api/)

## Installation

### Prerequisites
* A DocuSign developer account (email and password) on [demo.docusign.net](https://demo.docusign.net). If you don't have a developer account, create a [free account](https://go.docusign.com/o/sandbox/).
* A DocuSign integration key (a client ID) that is configured to use **JSON Web Token (JWT) Grant**.
   You will need the **integration key** itself and its **RSA key pair**. To use this application, you must add your application's **Redirect URI** to your integration key. This [**video**](https://www.youtube.com/watch?v=GgDqa7-L0yo) demonstrates how to create an integration key (client ID) for a user application such as this example. 
* C# .NET Core version 3.1 or later.
* [Node.js](https://nodejs.org/) v10+

### Installation steps
**Manual**
1. Download or clone this repository to your workstation in a new folder named **sample-app-MyClickwrap-csharp**.  
The repository includes a Visual Studio 2019 solution file with four .NET Core example projects:  
    * DocuSign.MyClickwrap.COVID19Waiver
    * DocuSign.MyClickwrap.NonDisclosureAgreement
    * DocuSign.MyClickwrap.TermsAndConditions
    * DocuSign.MyClickwrap.UI, which is a UI project that is an entry point for the other three example projects.
3. Configure intergation settings in **appsettings.json** or **launchsettings.json** for each Click API example project.
The following parameters must be configured:

    ```
    "IntegrationKey": "{IntegrationKey}",
    "RedirectURI": "{RedirectURI}",
    "SecretKey": "{SecretKey}",
    "SignerName": "{USER_FULLNAME}",
    "SignerEmail": "{USER_EMAIL}",
    "AuthServer": "{AuthServer}",
    "AuthorizationEndpoint": "{AuthorizationEndpoint}",
    "TokenEndpoint": "{TokenEndpoint}",
    "UserInformationEndpoint": "{UserInformationEndpoint}",
    "UserId": "{UserId}",
    "RSAPrivateKeyFile": "{RSAPrivateKeyFile}",
    "JWTLifeTime": "1"
    ```
> **Note:** Protect your integration key and client secret. You should make sure that the **package.json** and  **launchsettings.json** files will not be stored in your source code repository with configured integration keys.

## Running MyClickwrap
**Manual**
1. Build the solution.
2. Select the project you would like to run and set it as the startup project.
3. Run the project from Visual Studio or from the command-line _dotnet_ command, or from Visual Studio debug.

**Using a docker-compose file**

To start all four projects at once, use a docker-copose file:
1. Navigate to the **docker-compose.yml** file in the docker-compose project.
2. Configure the **docker-compose.yml** file by specifying the following environment variables for each Docker container:  
    ```
    - DocuSign__IntegrationKey= "{IntegrationKey}"
    - DocuSign__SecretKey= "{SecretKey}"
    - DocuSign__AuthServer=account-d.docusign.com
    - DocuSign__AuthorizationEndpoint=https://account-d.docusign.com/oauth/auth 
    - DocuSign__TokenEndpoint=https://account-d.docusign.com/oauth/token
    - DocuSign__UserInformationEndpoint=https://account-d.docusign.com/oauth/userinfo 
    - DocuSign__UserId= "{UserId}"
    - DocuSign__RSAPrivateKeyFile=/app/Certificate/RSA.pem
    ```
3. Configure the volume for the RSA private key file:  
    ```   
    volumes:
        - /path to the folder with your certificate:/app/Certificate 
    ```
4. Using the command line, run the following command:  
    ```
    docker-compose up
    ```
5. Open the application at the following URL: http://127.0.0.1:8088/myclickwrap/
6. To stop the application, run:  
    ```
    docker-compose down
    ```
7. For further details, regarding docker-compose, please see [Overview of Docker Compose](https://docs.docker.com/compose/) at Docker Docs.

## License information
This repository uses the MIT License. See the [LICENSE](./LICENSE) file for more information.
