FROM node:12.14.1-alpine as node-builder

WORKDIR /app

COPY ./src/FlirtingAppAngularNew .

RUN npm install && npm rebuild node-sass && npm run build -- --configuration=docker --outputPath=./dist


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS builder

COPY ./src ./src
COPY *.docker-build.sln .

RUN dotnet restore

WORKDIR /src/FlirtingApp.WebApi
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime

WORKDIR /

COPY --from=builder /src/FlirtingApp.WebApi/out .
COPY --from=node-builder /app/dist ./wwwroot

RUN apk add icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false ASPNETCORE_URLS=http://+:13000

ENTRYPOINT ["dotnet", "FlirtingApp.WebApi.dll"]
