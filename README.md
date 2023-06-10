# PashaTask

### An api project written in .net 6.0
### Added swagger and its xml documentation
### Entityframework used for db operations
### Fluent validation used for validate models
### Automapper used for mapping models
### Log4net used for loging error etc.

## Installation
### Clone from github, the link showed below
### https://github.com/eminismayilov91/PashaTask.git

## Usage
### Open project in Visual Studio which support .net 6.0
### Modify ApiUI >> appsettings.json >> ConnectionString >> MSSQLDBContextConnectionString file according to your db connection
### Modifiy ApiUI >> log4net.config >> file tag's value, add project's full"<project's full path>\Logs\log.txt"
### Open Package Manager Console Select default project DataAccess and run commands below
	#### add-migration "<Migration name>"
	#### update-database
### Run the project, you can use it on swagger, postman etc