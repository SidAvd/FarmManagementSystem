# Farm Management System
The Farm Management System is a web API designed to streamline and manage the various aspects of a farming operation. Built with .NET 6, the system allows you to manage crops, fields, harvests, worker assignments, and workers through a simple API, making it easier to automate and track the farm's day-to-day activities.

## Features
•	Manage crops and fields

•	Record and track harvest data

•	Assign workers to specific fields and tasks

•	Manage worker information and assignments

•	REST API endpoints with full CRUD functionality

•	Use of Swagger for API documentation and testing



## Technologies Used
•	.NET 6: The main framework for building the API.

•	Entity Framework Core: To handle database interactions using a code-first approach.

•	SQL Server: The database system for storing farm management data.

•	Swagger: For API documentation and testing.


## Setup Instructions
Prerequisites:
•	Visual Studio (with .NET 6 SDK or later)

•	SQL Server (installed locally or accessible remotely)

Steps to Set Up

1.	Download the Project
o	Go to the repository's GitHub page.
o	Click the green Code button, then select Download ZIP.
o	Extract the ZIP file to a folder on your local machine.
2.	Open the Project in Visual Studio
o	Launch Visual Studio.
o	Click on File > Open > Project/Solution.
o	Navigate to the folder where you extracted the ZIP file.
o	Open the FarmManagementSystem.sln file located in the root directory.
3.	Set Up the Database Connection String
o	In Visual Studio, open the appsettings.json file.
o	Update the connection string with your local or remote SQL Server instance.
Example:
"ConnectionStrings": {
  "ApiDbConnectionString": "Server=your_server_name;Database=FarmDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}

Apply Migrations
•	Open Package Manager Console in Visual Studio (Tools > NuGet Package Manager > Package Manager Console).
•	Run the following command to apply the database migrations:
Update-Database
1.	Run the Project
o	Press F5 or click the Run button in Visual Studio to build and run the project.
o	The API will start, and Visual Studio will open a browser window with the Swagger UI.
2.	Test the API with Swagger
o	Once the project is running, Swagger should automatically open in your default browser.
o	You can access the API documentation and test endpoints from the Swagger UI at https://localhost:5001/swagger (or http://localhost:5000 for HTTP).



## License
This project is licensed under the MIT License.


![search form 1](<Yugioh_MVC/Images/Form_1.png>)
