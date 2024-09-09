# Farm Management System
The Farm Management System is a web API designed to streamline and manage the various aspects of a farming operation. Built with .NET 8, the system allows you to manage crops, fields, harvests, worker assignments, and workers through a simple API, making it easier to automate and track the farm's day-to-day activities.

## Features
•	Manage crops and fields

•	Record and track harvest data

•	Assign workers to specific fields and tasks

•	Manage worker information and assignments

•	REST API endpoints with full CRUD functionality

•	Use of Swagger for API documentation and testing



## Technologies Used
•	.NET 8: The main framework for building the API.

•	Entity Framework Core: To handle database interactions using a code-first approach.

•	SQL Server: The database system for storing farm management data.

•	Swagger: For API documentation and testing.


## Setup Instructions
Prerequisites:

•	Visual Studio (with .NET 8 SDK or later)

•	SQL Server (installed locally or accessible remotely)


Steps to Set Up

1. Download the Project:
   * Go to the repository's GitHub page.
   * Click the green Code button, then select Download ZIP.
   *  Extract the ZIP file to a folder on your local machine.

2. Open the Project in Visual Studio:
   * Launch Visual Studio. Click on File > Open > Project/Solution.
   * Navigate to the folder where you extracted the ZIP file.
   * Open the FarmManagementSystem.sln file located in the root directory.

3. Set Up the Database Connection String:
   * In Visual Studio, open the appsettings.json file.
   * Update the connection string with your local or remote SQL Server instance.
      Example:
      ```json
     "ConnectionStrings": {
       "ApiDbConnectionString": "Server=your_server_name;Database=FarmDb;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
     ```
      Note: Ensure that your SQL Server instance is running and that you have created the FarmDb database if needed.

4. Apply Migrations:
   * Open Package Manager Console in Visual Studio (Tools > NuGet Package Manager > Package Manager Console).
   * Run the following command to apply the database migrations: Update-Database


## Test the Project

1. Run the Project:
   * Press F5 or click the Run button in Visual Studio to build and run the project.
   * The API will start, and Visual Studio will open a browser window with the Swagger UI.

2. Test the API with Swagger:
   * Once the project is running, Swagger should automatically open in your default browser, where you can test the API endpoints. Swagger provides interactive documentation, allowing you to test each endpoint directly in the browser.



![swagger 1](<FarmManagementSystem/Images/Swagger_1.png>)
![swagger 2](<FarmManagementSystem/Images/Swagger_2.png>)
