# ASP.NET project with DB

This is a sample project to learn and test ASP.NET DB 

## Features


## Getting Started

Clone the repo:

```bash
git clone https://github.com/your-username/your-repo.git
```

Set mysql db:
1. Configure DB connection in ./appsettings.json
2. Create a table `Products`

```bash
CREATE TABLE Products (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL
);
```

To run locally:

```bash
dotnet run
```
    