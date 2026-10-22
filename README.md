# eShop Catalog on .NET Framework

A lean product catalog API sample built with ASP.NET Web API 2 on
.NET Framework 4.8, Entity Framework 6, and SQL Server LocalDB.

The sample provides product CRUD, ASP.NET Identity user management,
JWT bearer authentication, and Swagger UI. It represents a classic application for evaluating
.NET migration tooling.

## Prerequisites

- Visual Studio with the ASP.NET and web development workload.
- .NET Framework 4.8 targeting pack and SQL Server LocalDB.

## Getting Started

1. Open `eShopCatalogApi.sln` and restore NuGet packages.
2. In `eShopCatalogApi`, copy `AppSettings.local.config.example` to
   `AppSettings.local.config`. Set `SeedPassword` to a local sample password
   of at least six characters and `JwtSigningKey` to a Base64-encoded random
   key of at least 32 bytes.
   The local configuration file is ignored by Git. Generate a key with PowerShell:

   ```powershell
   $key = New-Object byte[] 32
   $rng = [System.Security.Cryptography.RandomNumberGenerator]::Create()
   $rng.GetBytes($key)
   $rng.Dispose()
   [Convert]::ToBase64String($key)
   ```

3. Set `eShopCatalogApi` as the startup project and press **F5**.
   EF6 creates the `eShopCatalogApi` LocalDB database and seeds three products
   and one user through ASP.NET Identity's `UserManager` on first startup.
   Identity stores the hashed password in its standard EF-backed user store.
   Subsequent startups preserve the data; seed credentials apply only when
   the database is created.
   Missing authentication settings or an invalid seed password stop startup
   with a configuration error before database initialization.
4. Open `http://localhost:61461/swagger`. Call `POST /api/auth/login` with
   `{"UserName":"merchant","Password":"<your local sample password>"}`.
   In Swagger UI's API-key field, enter the word `Bearer`, a space, and the
   returned `access_token` to call the protected operations.

The initializer does not migrate or drop existing databases. If a database
has a different schema or was left without a user by an earlier failed seed,
choose a fresh database name in the `CatalogContext` connection string in
`Web.config` after correcting the settings. Restarting does not rerun seeding
for an existing database.

## API

Products have `Id`, `Sku`, `Name`, and `Price` fields.

| Method | Route | Operation |
|--------|-------|-----------|
| POST | `/api/auth/login` | Sign in anonymously; receive a JWT valid for 30 minutes |
| GET | `/api/products` | List products |
| GET | `/api/products/{id}` | Get a product |
| POST | `/api/products` | Create a product |
| PUT | `/api/products/{id}` | Update a product |
| DELETE | `/api/products/{id}` | Delete a product |

All product operations require a JWT. Create returns `201` with the product
and its location; update and delete return `204`. Invalid input returns `400`,
missing products return `404`, and missing or invalid tokens return `401`.
Prices must be nonnegative, fit the database's `decimal(18,2)` column,
and not require rounding to two decimal places. Swagger documentation is public.

This is a local development sample, not a production deployment template.
It has no frontend, external identity service, registration, refresh tokens,
roles, or integrations. See [Glossary](GLOSSARY.md) for project terms.
