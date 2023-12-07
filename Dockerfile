# FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
# WORKDIR /src
# COPY ["presentation/Presentation.csproj", "presentation/"]
# RUN dotnet restore "presentation/Presentation.csproj"
# COPY . .
# WORKDIR "/src/presentation"
# # RUN dotnet build "Presentation.csproj" -c Release -o /src/build

# FROM build AS publish
# RUN dotnet publish "Presentation.csproj" -c Release -o /src

# FROM publish AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "Presentation.dll"]



FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY ["presentation/Presentation.csproj", "Presentation/"]
COPY . .
RUN dotnet publish "Presentation/Presentation.csproj" -c Release -o /app
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 26927
ENTRYPOINT ["dotnet", "Presentation.dll"]