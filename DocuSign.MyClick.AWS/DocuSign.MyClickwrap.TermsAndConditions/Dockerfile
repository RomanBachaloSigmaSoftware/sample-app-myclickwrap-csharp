#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["DocuSign.MyClick/DocuSign.MyClickwrap.TermsAndConditions/DocuSign.MyClickwrap.TermsAndConditions.csproj", "DocuSign.MyClick/DocuSign.MyClickwrap.TermsAndConditions/"]
RUN dotnet restore "DocuSign.MyClick/DocuSign.MyClickwrap.TermsAndConditions/DocuSign.MyClickwrap.TermsAndConditions.csproj"
COPY . .
RUN dotnet build "DocuSign.MyClick/DocuSign.MyClickwrap.TermsAndConditions/DocuSign.MyClickwrap.TermsAndConditions.csproj" -c Release -o /app/build

FROM build AS publish
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash -
RUN apt-get install -y nodejs
RUN npm update
RUN npm install react-bootstrap
ENV PATH "$PATH:/usr/bin/node"
RUN dotnet publish "DocuSign.MyClick/DocuSign.MyClickwrap.TermsAndConditions/DocuSign.MyClickwrap.TermsAndConditions.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir /app/Certificate
COPY DocuSign.MyClick/Certificate/RSA.pem /app/Certificate
ENTRYPOINT ["dotnet", "DocuSign.MyClickwrap.TermsAndConditions.dll"]