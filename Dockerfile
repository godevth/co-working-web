#FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-core-env
#WORKDIR /app

# Copy csproj and restore as distinct layers
#COPY ./src/ .
#RUN dotnet restore
#RUN dotnet publish ./CoreCMS/CoreCMS.csproj -c Release -o out

FROM node:10 AS build-node-env
WORKDIR /app

# Install app dependencies
# A wildcard is used to ensure both package.json AND package-lock.json are copied
# where available (npm@5+)
COPY ./src/CoreCMS/clientapp/package*.json .
RUN npm install


COPY ./src/CoreCMS/clientapp/.eslintignore .
COPY ./src/CoreCMS/clientapp/.eslintrc.js .
COPY ./src/CoreCMS/clientapp/ .
RUN npm run build

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /usr/lib/ssl/openssl.cnf
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /usr/lib/ssl/openssl.cnf

WORKDIR /app

#COPY --from=build-core-env /app/out .

COPY out .

RUN mkdir ClientApp
COPY --from=build-node-env /app/dist ./ClientApp

RUN mkdir App_Data
COPY ./src/CoreCMS/App_Data ./App_Data

RUN apt-get update -yq && apt-get upgrade -yq && apt-get install -yq curl git nano
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash - && apt-get install -yq nodejs build-essential
RUN npm install -g npm

ENTRYPOINT ["dotnet", "CoreCMS.dll"]