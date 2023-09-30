# Movie API

Welcome to the Movie API repository! This API allows you to search for movies based on genre, year, or name, providing a comprehensive database of movie information at your fingertips.

# Features

- **Search by Genre:** Find movies by specifying a genre.
- **Search by Year:** Discover movies released in a particular year.
- **Search by Name:** Search for movies by title or keywords.
- **Detailed Movie Information:** Get detailed information about each movie, including title, year, genres, and thumbnails.

# Built with

- **Swagger:** Interactive API documentation for easy exploration.
- **.NET:** A versatile framework for building robust applications.
- **C#:** A powerful and versatile programming language for building software.
- **Azure Cognitive Search:** Enhances search capabilities and provides relevant results.
- **Azure App Service:** Reliable and scalable hosting for web applications.
- **GitHub build:** Streamlined CI/CD pipelines for automated builds and deployments.


## Table of Contents

- [Introduction](#introduction)
- [API Documentation](#api-documentation)
- [Azure Cognitive Search](#azure-cognitive-search)
- [GitHub Actions](#github-actions)

## Introduction

The Movie API is designed to provide information about movies. We have implemented various best practices to ensure its functionality and maintainability.

## API Documentation

### Swagger UI

Our API is documented using Swagger UI. You can access it here https://movie-app.azurewebsites.net/swagger/index.html. Swagger UI provides an interactive interface for exploring API endpoints and testing them.

### API Endpoints

#### Search Movies

- **Endpoint**: `/api/movies/search`
- **Method**: POST
- **Parameters**:
  - `top` (number): Number of movies to retrieve (default is 20).
  - `filterExpression` (string): Filter expression for searching movies.

**Example Request:**

```json
{
  "top": 10,
  "filterExpression": "Action"
}
```

**Example Response:**
```
[
  {
    "title": "Die Hard",
    "year": 1988,
    "genres": ["Action", "Thriller"],
    "thumbnail": "https://example.com/die-hard.jpg"
  },
  // Other movies...
]
```

## Tech Stack

Azure, C#, .NET 7.0, Cogntivie Search, Swagger

### Azure App Service
The API is hosted in the app service.

### Azure Cognitive Search
Our API leverages Azure Cognitive Search to fetch related movie data. This integration enhances search capabilities and ensures relevant results.

### GitHub Actions
We have implemented GitHub Actions for automating the build process. The workflow triggers on Pull Request events, ensuring that changes are built and tested before merging.
