# JobsHund

## Overview

JobsHund  is a professional **ASP.NET MVC 5** web application developed to demonstrate a clean, scalable, and interview-ready **job portal system** using the Model–View–Controller (MVC) architectural pattern.

## Key Features

* Job listings management (Create, Read, Update, Delete)
* Job categories and job type management
* MVC-based architecture with clear separation of concerns
* Reusable Class Library for business and data access logic
* Strongly-typed Razor Views
* Server-side validation and model binding
* Responsive UI using Bootstrap

## Technology Stack

* **Framework:** ASP.NET MVC 5
* **Programming Language:** C#
* **Frontend:** Razor Views, HTML5, CSS3, Bootstrap
* **Backend:** ASP.NET MVC Controllers & Models
* **ORM:** Entity Framework
* **Database:** Microsoft SQL Server
* **IDE:** Visual Studio

---

## Project Structure

```
JobsHund MVC/
│
├── JobsHund/                    # Main MVC Web Application
│   ├── Controllers/             # Handles HTTP requests
│   ├── Models/                  # Domain and View Models
│   ├── Views/                   # Razor UI Views
│   ├── Content/                 # CSS and static assets
│   ├── Scripts/                 # JavaScript files
│   └── JobsHund.csproj
│
├── jobhundClassLibrary1/         # Business & Data Logic Layer
│   └── jobhundClassLibrary1.csproj
│
└── JobsHund MVC.sln              # Visual Studio Solution File
```

---

## Screenshots (Application Walkthrough)

The following screenshots demonstrate the core functionality and architecture of the application, as typically presented during technical interviews.

### 1. Job Listings (Home Page)

Shows the main landing page where job listings are dynamically loaded from the database.

![Home Page](screenshots/01-home-page.png)

---

### 2. Job Details View

Demonstrates MVC routing and controller-to-view data flow for a selected job.

![Job Details](screenshots/02-job-details.png)

---

### 3. Create New Job

Illustrates form handling, model binding, and server-side validation.

![Add Job](screenshots/03-add-job.png)

---

### 4. Manage Jobs & Categories

Shows administrative functionality including edit and delete operations.

![Manage Jobs](screenshots/04-manage-jobs.png)

---

### 5. Project Architecture

Highlights the solution structure and separation of concerns using MVC and a class library.

![Project Structure](screenshots/05-project-structure.png)

---

## Installation & Setup

### Prerequisites

* Visual Studio 2019 or later
* .NET Framework 4.7 or higher
* Microsoft SQL Server / SQL Server Express

### Steps

1. **Clone the repository**

   ```bash
   git clone https://github.com/your-username/JobsHund-MVC.git
   ```

2. **Open the solution**

   * Open `JobsHund MVC.sln` in Visual Studio

3. **Restore dependencies**

   * NuGet packages will restore automatically

4. **Configure the database**

   * Update the connection string in `Web.config`

   ```xml
   <connectionStrings>
     <add name="DefaultConnection" 
          connectionString="YOUR_SQL_SERVER_CONNECTION_STRING" 
          providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```

5. **Apply database migrations (if enabled)**

   ```bash
   Update-Database
   ```

6. **Run the application**

   * Press **F5** or click **Start** in Visual Studio

---


## License

This project is intended for **educational, learning, and interview demonstration purposes**. You are free to use and extend it.



