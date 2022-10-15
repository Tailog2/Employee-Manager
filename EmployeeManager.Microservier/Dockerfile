#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EmployeeManager.Microservier/EmployeeManager/EmployeeManager.csproj", "EmployeeManager.Microservier/EmployeeManager/"]
COPY ["EmployeeManager.Microservier/EmployeeManager.Infrastructure.Business/EmployeeManager.Infrastructure.Business.csproj", "EmployeeManager.Microservier/EmployeeManager.Infrastructure.Business/"]
COPY ["EmployeeManager.Microservier/EmployeeManager.Domain.Interfaces/EmployeeManager.Domain.Interfaces.csproj", "EmployeeManager.Microservier/EmployeeManager.Domain.Interfaces/"]
COPY ["EmployeeManager.Microservier/EmployeeManager.Domain.Core/EmployeeManager.Domain.Core.csproj", "EmployeeManager.Microservier/EmployeeManager.Domain.Core/"]
COPY ["EmployeeManager.Microservier/EmployeeManager.Services.Interfaces/EmployeeManager.Services.Interfaces.csproj", "EmployeeManager.Microservier/EmployeeManager.Services.Interfaces/"]
COPY ["EmployeeManager.Microservier/EmployeeManager.Infrastructure.Data/EmployeeManager.Infrastructure.Data.csproj", "EmployeeManager.Microservier/EmployeeManager.Infrastructure.Data/"]
RUN dotnet restore "EmployeeManager.Microservier/EmployeeManager/EmployeeManager.csproj"
COPY . .
WORKDIR "/src/EmployeeManager.Microservier/EmployeeManager"
RUN dotnet build "EmployeeManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmployeeManager.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeeManager.dll"]