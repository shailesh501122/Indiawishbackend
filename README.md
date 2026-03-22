# IndiaWish Real Estate Platform

A modular monolith real estate platform scaffold built with .NET 8, React, Tailwind, PostgreSQL, MediatR, FluentValidation, AutoMapper, Redis, Serilog, JWT authentication, and CQRS-driven modules.

## Render deployment

This repository now includes a Render Blueprint (`render.yaml`) that provisions:

- `indiawish-api` as a Docker-based .NET 8 web service
- `indiawish-web` as a static Vite site
- `indiawish-admin` as a static Vite site
- `indiawish-db` as a managed PostgreSQL database
- `indiawish-cache` as a Key Value instance for Redis-compatible caching

### API service notes

- The API Docker image is defined in `src/API/RealEstatePlatform.API/Dockerfile`.
- The API binds to the Render-provided `PORT` environment variable.
- PostgreSQL is consumed via `ConnectionStrings__DefaultConnection`.
- Redis is consumed via `ConnectionStrings__Redis`, with local fallback to `ConnectionStrings:Redis`.

### Frontend environment variables

Set `VITE_API_BASE_URL` in Render to the public URL of the deployed API service (for example, `https://indiawish-api.onrender.com`). Static sites cannot use Render's private-service hostnames from the browser.

### SPA routing

Both frontends include a `_redirects` file so deep links such as `/properties/:slug` and `/dashboard` work correctly on Render static hosting.

### Migrations

Run migrations from a machine or CI runner with the .NET 8 SDK installed:

```bash
./database/migrate.sh
```
