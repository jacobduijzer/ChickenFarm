FROM mcr.microsoft.com/dotnet/sdk:latest AS build
WORKDIR /src
COPY . .

FROM build AS publish
ARG PROJECT_FILE
RUN dotnet restore
RUN dotnet publish ${PROJECT_FILE} -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:latest
WORKDIR /app
COPY --from=publish /out .

ARG PROJECT_DLL
RUN echo "dotnet ${PROJECT_DLL}" > ./entrypoint.sh
ENTRYPOINT ["/bin/sh", "entrypoint.sh"]