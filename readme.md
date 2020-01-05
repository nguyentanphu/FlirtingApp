# Initial release of clean architecture sample app

## Prerequisite software requirements

- Visual Studio 2019 or other IDE that supports dotnet core 3.1.
- Local SQL Server with integrated security (or edit FlirtingApp.WebApi/appsettings.json ConnectionStrings.DefaultConnectionString to match your setup)
- MongoDb with port 27017 without authentication (or edit FlirtingApp.WebApi/appsettings.json MongoOptions to match your setup). Mongo can be run with docker with the command: **docker container run -d -p 27017:27017 --name my-mongo mongo**

<img src="https://imgur.com/download/zpTqpYs/"
     alt="Clean architecture" />

## Features

- ASP.NET Core 3.1 LTS, ASP.NET Core 3.1 Identity
- Angular 8 with Angular material component, Angular flex-layout

- CQRS with Mediator: Commands will write to both SQL Server and MongoDb, Queries will read from MongoDb for optimized performance.
- 2 DbContext: 1 for Application entities, 1 for IdentityDbContext.
- Location searching for users and find users based on X distances using mongodb geospatial

<img src="https://imgur.com/download/zwI5AlQ/"
     alt="Output port" />
     
- Jwt Authentication with refresh token.
- Heavily unit tested with separates test projects.
- Validation with FluentValidation.
- Remove usage of default ASP.NET Core validation with ModelState and move validation to Application layer as it's part of business logic.
- Implement Presenter and IOutputPort to separate presentation logic from controllers

<img src="https://s5.gifyu.com/images/FlirtingAppAngularNew-Google-Chrome-2020-01-05-22-18-00.gif"
     alt="Output port" />

- Cloudinary integration for uploading images
- Custom exceptions for each layers.
- Unify exceptions errors message with ExceptionMiddleware
- Simple, focus on intention of the code.
- Design with SOLID principals in mind.
- Separation of concerns.

## How to run

- In visual studio hit Ctrl + F5, check the port to see if it's 44367
- Open FlirtingApp\FlirtingAppAngularNew folder, run command ng serve -o
- Sample login: username: Page, password: password

## Work in progress

- Integration searching and logging with Elastic search.
- Real-time private chat and group chat with SignalR 3 and Apache Hbase
- Dockerizing the app.
