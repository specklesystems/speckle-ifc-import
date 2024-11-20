#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.


FROM mcr.microsoft.com/dotnet/sdk:8.0-noble AS build
COPY . .
WORKDIR .
RUN dotnet run --project src/build/build.csproj -- publish

FROM mcr.microsoft.com/dotnet/runtime:8.0-noble AS base
WORKDIR /app
COPY --link --from=build /publish .

ENTRYPOINT ["./Speckle.WebIfc.Importer"]