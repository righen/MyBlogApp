FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MyBlogApp.API/MyBlogApp.API.csproj", "MyBlogApp.API/"]
COPY ["MyBlogApp.Application/MyBlogApp.Application.csproj", "MyBlogApp.Application/"]
COPY ["MyBlogApp.Domain/MyBlogApp.Domain.csproj", "MyBlogApp.Domain/"]
COPY ["MyBlogApp.Infrastructure/MyBlogApp.Infrastructure.csproj", "MyBlogApp.Infrastructure/"]
RUN dotnet restore "MyBlogApp.API/MyBlogApp.API.csproj"
COPY . .
WORKDIR "/src/MyBlogApp.API"
RUN dotnet build "MyBlogApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyBlogApp.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyBlogApp.API.dll"]