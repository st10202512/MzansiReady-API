# MzansiReady API

Backend REST API for the MzansiReady Android application.

## Endpoints

| Method | Endpoint | Purpose |
|--------|----------|---------|
| POST | /api/auth/register | Register |
| POST | /api/auth/login | Login |
| GET | /api/users/settings | Get settings |
| PUT | /api/users/settings | Update settings |
| GET | /api/locations | List locations |
| POST | /api/locations | Add location |
| DELETE | /api/locations/{id} | Delete location |
| GET | /api/alerts | List alerts |

## Tech Stack
ASP.NET Core 8, Entity Framework Core, PostgreSQL, JWT, BCrypt

## How to Run
1. Update `appsettings.json` with your Supabase connection string
2. `dotnet ef database update`
3. `dotnet run`
4. Swagger: `http://localhost:5250/swagger`

## Android App
[MzansiReady](https://github.com/st10202512/MzansiReady)

## Author
[Hlulani Hope Mashaba] — st10202512
