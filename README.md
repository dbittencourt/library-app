# library-app
library app for monash

This project was developed using .net core, aspnet core and sqlite.

Instructions:

$ cd LibraryApp
$ dotnet restore
$ dotnet ef migrations add InitialCreate  
$ dotnet ef database update
$ dotnet run
