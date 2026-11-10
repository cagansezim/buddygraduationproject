FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/buddy.Web.Host/buddy.Web.Host.csproj", "src/buddy.Web.Host/"]
COPY ["src/buddy.Web.Core/buddy.Web.Core.csproj", "src/buddy.Web.Core/"]
COPY ["src/buddy.Application/buddy.Application.csproj", "src/buddy.Application/"]
COPY ["src/buddy.Core/buddy.Core.csproj", "src/buddy.Core/"]
COPY ["src/buddy.EntityFrameworkCore/buddy.EntityFrameworkCore.csproj", "src/buddy.EntityFrameworkCore/"]
WORKDIR "/src/src/buddy.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/buddy.Web.Host", "src/buddy.Web.Host"]
COPY ["src/buddy.Web.Core", "src/buddy.Web.Core"]
COPY ["src/buddy.Application", "src/buddy.Application"]
COPY ["src/buddy.Core", "src/buddy.Core"]
COPY ["src/buddy.EntityFrameworkCore", "src/buddy.EntityFrameworkCore"]
WORKDIR "/src/src/buddy.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "buddy.Web.Host.dll"]
