FROM mcr.microsoft.com/dotnet/sdk:6.0 AS test

COPY . .
RUN dotnet restore

ENTRYPOINT [ "dotnet", "test" ]