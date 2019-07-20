Customer Inquiry service
=============================

Features:
- ASP.NET Core 2.2
- Swagger
- API Versioning 
- Logging (Serilog)
- MS SQL database (Entity Framework)
- In memory database (Entity Framework, for testing)
- Unit Tests (NUnit, Mock)

How to run:
1) Open the solution in Visual studio
2) Set as StartUp project the "API" project
3) Select an appropriate profile
	- "Development" - run the project using MS SQL database, this database is created automatically, make sure that you have a correct connection string in the appsettings.Development.json file.
	- "Testing" - run the project using "In memory database"
In both cases you will have some test data, which is created automatically when you run the application. You can see this data in swagger by making a request to /api/Customers/GetAll in Swagger.
5) Run the project
