FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY *.sln ./
COPY Profile.Api/*.csproj ./Profile.Api/
COPY Profile.Application/*.csproj ./Profile.Application/
COPY Profile.Domain/*.csproj ./Profile.Domain/
COPY Profile.Persistence/*.csproj ./Profile.Persistence/

RUN dotnet restore

COPY Profile.Api/ ./Profile.Api/
COPY Profile.Application/ ./Profile.Application/
COPY Profile.Domain/ ./Profile.Domain/
COPY Profile.Persistence/ ./Profile.Persistence/

WORKDIR /src/Profile.Api
RUN dotnet publish -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

RUN useradd -m -u 1001 appuser && chown -R appuser:appuser /app
USER appuser

HEALTHCHECK --interval=30s --timeout=5s --start-period=10s --retries=3 \
CMD ["sh", "/app/healthcheck.sh"]