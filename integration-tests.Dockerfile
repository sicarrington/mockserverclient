FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS test
# COPY *.sln .
# COPY *.csproj ./
COPY . .
RUN dotnet restore

ENTRYPOINT [ "dotnet", "test" ]