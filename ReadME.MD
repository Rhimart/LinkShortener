# Link Shortener Project
 This is a sample test by Rhimart Bravo
## Overview

This project consists of a link shortener service built with a C# backend and an Angular frontend. The backend provides APIs to generate shortened links and retrieve the list of shortened links, while the frontend offers a user interface for interacting with these APIs.

## Project Structure

link-shortener/ ├── build/ │ ├── link-shortener-ui.dockerfile │ └── link-shortener-test.dockerfile └── src/ ├── link-shortener-ui/ │ └── LinkShortener.sln └── link-shortener-test/

## Prerequisites

- .NET SDK (version 6.0 or higher)
- Node.js (version 14 or higher)
- Angular CLI
- Docker and Docker Compose

## Setup Instructions

### Backend Setup (`link-shortener-ui`)

1. Navigate to the `link-shortener-ui` directory:

   cd src/link-shortener-ui


2. dotnet restore

### Frontend Setup (link-shortener-test)

1. Navigate to the link-shortener-test directory:

cd src/link-shortener-test
Install the required packages:
npm install

### Docker Setup
Ensure that Docker and Docker Compose are installed on your machine.

Navigate to the root directory containing the docker-compose.yml file.

Run the following command to build and start the containers:
docker-compose up --build

Access the frontend application at http://localhost:4200 and the backend API at http://localhost:5002.

### Start the Angular application:
ng serve
Access the application at http://localhost:4200.

### API Endpoints
Shorten Link
POST /api/LinkShortener
Request Body:
json
Copy code
{
  "url": "http://example.com"
}
Get All Shortened Links
GET /api/LinkShortener
Response:
json
Copy code
{
  "Message": "Success",
  "Data": [
    {
      "OriginalUrl": "http://example.com",
      "ShortUrl": "http://short.ly/abc123"
    }
  ]
}
