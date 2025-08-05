# FikraSparkCore Application README

## Overview
FikraSparkCore is a collaborative visual platform for teams to brainstorm, vote, and prioritize ideas using a sticky-note style board. The application combines an Angular frontend with a .NET backend, using SQL Server for data storage.

## Technologies Used
- Angular 18.2.x
- .NET Core
- SQL Server 2022
- TailwindCSS 3.4.x for styling
- Nunit, Karma and Jasmine for unit testing
- Docker and Docker Compose for containerization
## Prerequisites
- Docker and Docker Compose
- Node.js and npm
- .NET SDK

## Running with Docker Compose
1. Clone the repository and navigate to the root directory containing docker-compose.yml
2. Start the application stack:
```
docker-compose up --build
```
This will start:

- SQL Server database on port 5434
- Backend API on port 5000

## Frontend Development Setup
1. Navigate to the frontend directory:
```
cd src/Web/ClientApp
```
2. Install dependencies:
```
npm install
```
3. Start the development server:
```
npm start
```
The application will be available at https://localhost:44447

## Running Tests

### Backend Unit Tests
1. Navigate to the backend directory:
```
cd tests/Application.UnitTests
```
2. Then run the tests
 Nunit testing framework

### Frontend Unit Tests
1. Navigate to the frontend directory:
```
cd src/Web/ClientApp
```
2. 1.
   Run the test suite:
```
 Jasmine testing framework
- Chrome browser for test execution
- Coverage reporting enabled
npm test
```
This will start Karma test runner and execute all tests with the following setup:

- Jasmine testing framework
- Chrome browser for test execution
- Coverage reporting enabled

## Environment Variables

### Backend (API)
- ASPNETCORE_URLS : HTTP endpoint configuration
- ConnectionStrings__DefaultConnection : Database connection string

### Database
- Default port: 5434
- Default credentials:
  - User: sa
  - Password: Password123@KL@
## Project Structure
```
src/
├── Web/                 # Web API and Frontend
│   ├── ClientApp/       # Angular frontend application
│   └── Dockerfile       # API Dockerfile
├── Application/         # Application logic
├── Domain/             # Domain models and logic
└── Infrastructure/    
```

## Development Scripts
Available npm scripts in the frontend project:

- npm start : Start the development server with HTTPS
- npm run build : Build the production version
- npm run watch : Build and watch for changes
- npm test : Run unit tests

## Troubleshooting
If you encounter issues:

1. Ensure Docker is running
2. Check if ports 5000 and 5434 are available
3. For frontend development, ensure Node.js and npm are installed
4. Verify that the ASP.NET Core development certificate is trusted