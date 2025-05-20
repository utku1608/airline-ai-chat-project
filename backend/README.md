# ✈️ Airline API – Midterm Project

This is a RESTful Web API for managing airline ticketing operations such as flight scheduling, ticket purchasing, passenger check-in, and querying passenger lists. Developed as part of SE4458 Software Architecture & Design of Modern Large Scale Systems – Midterm.

## ✅ Features

- Add new flights
- Query available flights (with paging support)
- Buy tickets (decrease capacity)
- Check-in passengers (assign seats)
- List passengers (with paging support)
- JWT-based authentication for protected endpoints

## 🚀 Technologies Used

- .NET 8 (ASP.NET Core Web API)
- Entity Framework Core (In-Memory)
- JWT Authentication
- Swagger (Swashbuckle)
- Visual Studio Code

## 🔐 Authentication

To use protected endpoints, first call:

```
POST /api/v1/auth/login
```

Request Body:
```json
{
  "username": "admin",
  "password": "1234"
}
```

A valid JWT token will be returned.  
Use this token with the **Authorize** button in Swagger UI:

```
Bearer eyJhbGciOi...
```

Protected endpoints:
- `POST /api/v1/flight/add`
- `POST /api/v1/flight/buy`
- `POST /api/v1/flight/passengerlist`

## 📄 API Endpoints Overview

| Endpoint                             | Method | Auth | Paging | Description                          |
|--------------------------------------|--------|------|--------|--------------------------------------|
| `/api/v1/flight/add`                 | POST   | ✔️   | ❌     | Add a new flight                     |
| `/api/v1/flight/query`               | POST   | ❌   | ✔️     | List available flights               |
| `/api/v1/flight/buy`                 | POST   | ✔️   | ❌     | Buy ticket and reduce seat count     |
| `/api/v1/flight/checkin`             | POST   | ❌   | ❌     | Check-in passenger and assign seat   |
| `/api/v1/flight/passengerlist`       | POST   | ✔️   | ✔️     | List checked-in passengers           |
| `/api/v1/auth/login`                 | POST   | ❌   | ❌     | Get a JWT token                      |

## 🧠 Assumptions & Decisions

- Seat numbers assigned as: `P1`, `P2`, `P3`, ...
- All data is stored in memory (no DB used)
- Only 1 static user for login (`admin / 1234`)
- No ticket cancellation or update for simplicity

## ✅ Example Test Sequence

1. Login and get token ✅  
2. Add a flight  
3. Query flight  
4. Buy ticket  
5. Check-in  
6. Query passenger list

## 🎥 Demo Video

> [📹 Click to Watch the Demo](https://drive.google.com/your-demo-link)

## 🔗 Project Repository

[👉 GitHub Repository](https://github.com/utku1608/AirlineApi-Midterm)

## 👨‍💻 Developed by

Utku Derici  
Group 1 – API Project for Airline Company  
SE4458 - Spring 2025
