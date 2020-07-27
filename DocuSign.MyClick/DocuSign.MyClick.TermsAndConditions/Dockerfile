#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["DocuSign.TermsAndConditions/DocuSign.TermsAndConditions.csproj", "DocuSign.TermsAndConditions/"]
RUN dotnet restore "DocuSign.TermsAndConditions/DocuSign.TermsAndConditions.csproj"
COPY . .
WORKDIR "/src/DocuSign.TermsAndConditions"
RUN dotnet build "DocuSign.TermsAndConditions.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DocuSign.TermsAndConditions.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DocuSign.TermsAndConditions.dll"]