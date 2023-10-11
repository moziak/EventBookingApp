#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EventBooking.Api/EventBooking.Api.csproj", "EventBooking.Api/"]
COPY ["EventBooking.Infrastructure/EventBooking.Infrastructure.csproj", "EventBooking.Infrastructure/"]
COPY ["EventBooking.Application/EventBooking.Application.csproj", "EventBooking.Application/"]
COPY ["EventBooking.Domain/EventBooking.Domain.csproj", "EventBooking.Domain/"]
RUN dotnet restore "EventBooking.Api/EventBooking.Api.csproj"
COPY . .
WORKDIR "/src/EventBooking.Api"
RUN dotnet build "EventBooking.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventBooking.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventBooking.Api.dll"]
