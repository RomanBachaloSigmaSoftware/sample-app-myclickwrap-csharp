#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["DocuSign. NonDisclosureAgreement/DocuSign. NonDisclosureAgreement.csproj", "DocuSign. NonDisclosureAgreement/"]
RUN dotnet restore "DocuSign. NonDisclosureAgreement/DocuSign. NonDisclosureAgreement.csproj"
COPY . .
WORKDIR "/src/DocuSign. NonDisclosureAgreement"
RUN dotnet build "DocuSign. NonDisclosureAgreement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DocuSign. NonDisclosureAgreement.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DocuSign. NonDisclosureAgreement.dll"]