# ASP.Net Core 3.1 and React: MyClickwrap Sample Application

### GitHub repo: sample-app-myclickwrap-csharp
Visit [MyClickwrap sample application](https://github.com/docusign/sample-app-myclickwrap-csharp/) on DocuSign to see this code publicly hosted

## Introduction
MyClickwrap is a sample application written in ASP.Net Core 3.1 (server) and React 9 (client). You can find a live instance running at [https://github.com/docusign/sample-app-myclickwrap-csharp](https://github.com/docusign/sample-app-myclickwrap-csharp).

MyClickwrap contains three different projects for three Click API use-cases bundled into a single solution.
1. <b> COVID-19 Health Declaration. </b> 
This scenario shows how the Click API can be used to build a simple tool to enable organizations to capture consent from individuals releasing the organization from responsibility in case they contract COVID-19 while on their premises. 
2. <b> Non-Disclosure Agreement (NDA). </b>
This scenario shows how the Click API can be used when visitors are signing an NDA before they can attend a meeting (similar to a Lobby Application). The visitor has to provide their email address so that is used to secure the agreement and call it server-side.
3. <b> Terms and Conditions. </b>
This scenario shows how the Click API can be used to enable your application to capture users consent to terms and conditions and a privacy policy.

Scenarios are showcasing the Click functionality and using the Click API. Each Scenario demonstrates the following:
1. Authentication with [JSON Web Token (JWT) Grant](https://developers.docusign.com/esign-rest-api/guides/authentication/oauth2-jsonwebtoken)

2. How to use the Click API to create a clickwrap programmatically, render it in your UI, and then submit it. Once the user submits the form, the app uses a clickwrap to present information related to the scenario and requesting confirmation.
   * [More information about Click API](https://developers.docusign.com/click-api)

## Installation

### Prerequisites
* A DocuSign Developer Sandbox account (email and password) on [demo.docusign.net](https://demo.docusign.net). If you don't have a developer sandbox, create a [free account](https://go.docusign.com/sandbox/productshot/?elqCampaignId=16535).
* A DocuSign integration key (a client ID) that is configured to use **JSON Web Token (JWT) Grant**.
   You will need the **integration key** itself and its **RSA key pair**. To use this application, you must add your application's **Redirect URI** to your integration key. This [**video**](https://www.youtube.com/watch?v=GgDqa7-L0yo) demonstrates how to create an integration key (client ID) for a user application like this example. 
* C# .NET Core version 3.1 or later.
* [Node.js](https://nodejs.org/) v10+

### Installation steps
**Manual**
1. Download or clone this repository to your workstation in a new folder named **sample-app-MyClickwrap-csharp**.
2. The repository includes a Visual Studio 2019 solution file with four .Net Core projects: DocuSign.MyClickwrap.COVID19Waiver, DocuSign.MyClickwrap.NonDisclosureAgreement and DocuSign.MyClickwrap.TermsAndConditions with Click API examples and DocuSign.MyClickwrap.UI which is UI project that is an entry point for examples projects.
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
> **Note:** Protect your integration key and client secret. You should make sure that the **package.json** or  **launchsettings.json** files will not be stored in your source code repository with configured integration keys.

## Running MyClickwrap project
**Manual**
1. Build the solution.
2. Select project you would like to run and set it as startup project.
3. Run project from Visual Studio or from cmd  <i>dotnet</i> command or from Visual Studio debug.

**Using docker-compose file**

In order to start all four projects at once docker-copose file must be used.
1. Navigate to **docker-compose.yml** file under docker-compose project
2. Configure **docker-compose.yml** file by spefiying following environment variables for each docker container:
    
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

3. Configure volume for RSA private key file:

 ```   
   volumes:
       - /path to the folder with your certificate:/app/Certificate 
 ```
4. Using cmd run following command:
    
 ```
   docker-compose up
```
5. The application must be started in your defaiult browser by following URL: http://localhost:8088/myclickwrap/
6. To stop application run: 
```
   docker-compose down
```
7. For further details regarding docker-compose please see https://docs.docker.com/compose/

## License information
This repository uses the MIT License. See the [LICENSE](./LICENSE) file for more information.