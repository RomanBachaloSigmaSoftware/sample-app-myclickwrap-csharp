#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["DocuSign.MyClick/DocuSign.MyClickwrap.NonDisclosureAgreement/DocuSign.MyClickwrap.NonDisclosureAgreement.csproj", "DocuSign.MyClick/DocuSign.MyClickwrap.NonDisclosureAgreement/"]
RUN dotnet restore "DocuSign.MyClick/DocuSign.MyClickwrap.NonDisclosureAgreement/DocuSign.MyClickwrap.NonDisclosureAgreement.csproj"
COPY . .
RUN dotnet build "DocuSign.MyClick/DocuSign.MyClickwrap.NonDisclosureAgreement/DocuSign.MyClickwrap.NonDisclosureAgreement.csproj" -c Release -o /app/build

FROM build AS publish
RUN curl -sL https://deb.nodesource.com/setup_14.x | bash -
RUN apt-get install -y nodejs
RUN npm update
RUN npm install react-bootstrap
ENV PATH "$PATH:/usr/bin/node"
RUN dotnet publish "DocuSign.MyClick/DocuSign.MyClickwrap.NonDisclosureAgreement/DocuSign.MyClickwrap.NonDisclosureAgreement.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
RUN mkdir /app/NonDisclosureAgreement
COPY --from=publish /app/publish .
COPY --from=publish /app/publish ./NonDisclosureAgreement
RUN mkdir /app/Certificate
COPY DocuSign.MyClick/Certificate/RSA.pem /app/Certificate
ENTRYPOINT ["dotnet", "DocuSign.MyClickwrap.NonDisclosureAgreement.dll"]