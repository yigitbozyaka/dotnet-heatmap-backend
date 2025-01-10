# Heatmap Backend

This repository contains the backend code for the Heatmap website [Frontend](https://github.com/yigitbozyaka/angular-heatmap). The backend is developed using .NET and MongoDB for database management.

## Features

- Handles user feedback data for the heatmap visualization.
- Connects to MongoDB for storing and retrieving feedback data.

## Setup Instructions

### Prerequisites

- .NET SDK installed on your system.
- A MongoDB instance (local or cloud) to store data.

### Configuration Steps

1. **Clone the Repository**

   ```bash
   git clone <repository-url>
   cd <repository-folder>

2. **Update FeedbackRepository.cs**

In the FeedbackRepository.cs file, locate the following line:

```csharp
var database = client.GetDatabase("dbName");
```
^ Replace "dbName" with the actual name of your MongoDB database:

3. **Configure appsettings.json**

Open the appsettings.json file and locate the "mongodb_url" placeholder. Replace it with your actual MongoDB connection string:

```json
{
  "ConnectionStrings": {
    "MongoDb": "mongodb_url"
  }
}
```
4. **Restore Dependencies**

Run the following command to restore the required .NET dependencies:

```bash
dotnet restore
```

5. **Run the Application**

Use the following command to start the backend:

```bash
dotnet run
````

6. **Test the Application**

Ensure that the backend is running properly and connected to your MongoDB instance.

### Contributions
Feel free to fork this repository and create pull requests for any improvements or bug fixes.
