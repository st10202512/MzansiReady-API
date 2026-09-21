# MzansiReady API

Backend REST API for the MzansiReady Android application. Built with ASP.NET Core 8, Entity Framework Core, and PostgreSQL (hosted on Supabase).

---

## 📖 Overview

The MzansiReady API provides backend services for the MzansiReady Android app, including user authentication, saved locations, community alerts, and per-user settings. It uses JWT-based authentication with BCrypt password hashing and persists data in a PostgreSQL database hosted on Supabase.

---

## ✨ Features

- **User Authentication**
  - Registration with full name, email, and password
  - Login returning a signed JWT token
  - BCrypt password hashing (never stores plain text)
  - Duplicate email prevention

- **User Settings**
  - Language preference (English / isiZulu)
  - Theme preference (Light / Dark)
  - Notification toggle
  - Synced per user

- **Saved Locations**
  - Create, list, and delete saved locations
  - Per-user isolation
  - Latitude / longitude validation

- **Community Alerts**
  - Seeded with 3 real-world South African scenarios
  - Retreievable by authenticated users

- **Database**
  - PostgreSQL on Supabase
  - EF Core migrations
  - Automatic migration on startup
  - Data seeding on first run

- **Documentation**
  - Swagger UI enabled for testing

---

## 🛠️ Tech Stack

| Component | Technology |
|-----------|-----------|
| Framework | ASP.NET Core 8 |
| Language | C# 12 |
| ORM | Entity Framework Core 8 |
| Database | PostgreSQL (Supabase) |
| Authentication | JWT Bearer tokens |
| Password Hashing | BCrypt.Net-Next |
| API Documentation | Swagger (Swashbuckle) |
| Configuration | appsettings.json + environment variables |

---

## 📡 API Endpoints

### Authentication

| Method | Endpoint | Purpose | Auth Required |
|--------|----------|---------|---------------|
| POST | `/api/auth/register` | Register a new user | ❌ No |
| POST | `/api/auth/login` | Login and receive a JWT | ❌ No |

### User Settings

| Method | Endpoint | Purpose | Auth Required |
|--------|----------|---------|---------------|
| GET | `/api/users/settings` | Retrieve current user's settings | ✅ Yes |
| PUT | `/api/users/settings` | Update user settings | ✅ Yes |

### Saved Locations

| Method | Endpoint | Purpose | Auth Required |
|--------|----------|---------|---------------|
| GET | `/api/locations` | List all saved locations | ✅ Yes |
| POST | `/api/locations` | Add a new saved location | ✅ Yes |
| DELETE | `/api/locations/{id}` | Delete a saved location | ✅ Yes |

### Community Alerts

| Method | Endpoint | Purpose | Auth Required |
|--------|----------|---------|---------------|
| GET | `/api/alerts` | Retrieve community alerts | ✅ Yes |

---

## 🗄️ Database Schema

### Users Table

| Column | Type | Notes |
|--------|------|-------|
| UserId | int (PK) | Auto-increment |
| FullName | string | Required |
| Email | string (unique) | Lowercase, unique index |
| PasswordHash | string | BCrypt hash (never plain text) |
| PreferredLanguage | string | "en" or "zu" |
| Theme | string | "light" or "dark" |
| NotificationsEnabled | bool | Default true |
| CreatedAt | datetime | UTC |

### SavedLocations Table

| Column | Type | Notes |
|--------|------|-------|
| LocationId | int (PK) | Auto-increment |
| UserId | int (FK) | References Users |
| Name | string | e.g., "Home", "Work" |
| Latitude | double | -90 to 90 |
| Longitude | double | -180 to 180 |
| CreatedAt | datetime | UTC |

### Alerts Table

| Column | Type | Notes |
|--------|------|-------|
| AlertId | int (PK) | Auto-increment |
| Title | string | Required |
| Description | string | Required |
| Category | string | Water, Electricity, Weather, etc. |
| Area | string | Johannesburg, National, Gauteng |
| CreatedAt | datetime | UTC |

### Seed Data

Three alerts are seeded on first run:
1. **Planned water interruption** — Water / Johannesburg
2. **Stage 3 load shedding** — Electricity / National
3. **Severe thunderstorm warning** — Weather / Gauteng

---

## 🚀 How to Run

### Prerequisites

- .NET 8 SDK
- PostgreSQL database (or Supabase account)
- `dotnet-ef` tool installed:
  ```bash
  dotnet tool install --global dotnet-ef
