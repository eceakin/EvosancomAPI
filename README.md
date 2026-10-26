# EvosancomAPI

A **B2B dealer ordering API** built with **ASP.NET Core 8**. Dealers browse a product catalogue with dealer-specific prices, fill a basket and place orders; administrators manage products, categories, dealers, users and roles. New products and orders are pushed to connected clients in real time over **SignalR**.

The solution is organised as an **Onion Architecture** with **CQRS** (MediatR), the **Repository pattern** split into read and write sides, **ASP.NET Core Identity** with **JWT + refresh tokens**, and a **dynamic, database-driven role → endpoint authorization** system.

---

## Contents

- [Tech stack](#tech-stack)
- [Architecture](#architecture)
- [Key technical features](#key-technical-features)
- [Domain model](#domain-model)
- [API overview](#api-overview)
- [Getting started](#getting-started)
- [Project structure](#project-structure)

---

## Tech stack

| Area | Technology |
|---|---|
| Runtime | .NET 8, ASP.NET Core Web API |
| Data access | Entity Framework Core 8, Npgsql (PostgreSQL) |
| Identity | ASP.NET Core Identity (`ApplicationUser`, `ApplicationRole`) |
| Authentication | JWT Bearer, cryptographically random refresh tokens |
| Application layer | MediatR (CQRS), AutoMapper |
| Real-time | ASP.NET Core SignalR |
| Logging | Serilog: console, file and **PostgreSQL sink**; ASP.NET Core HTTP logging |
| API docs | Swagger / Swashbuckle |
| Database | PostgreSQL |

## Architecture

```
                      ┌──────────────────────────────┐
                      │        EvosancomAPI.API      │  Controllers, filters,
                      │  (presentation / composition)│  middleware, Program.cs
                      └──────────────┬───────────────┘
            ┌────────────────────────┼─────────────────────────┐
            ▼                        ▼                         ▼
 ┌────────────────────┐  ┌──────────────────────┐  ┌──────────────────────┐
 │    Persistence     │  │    Infrastructure    │  │       SignalR        │
 │ EF Core DbContext, │  │ JWT token handler,   │  │ Hubs and hub         │
 │ repositories,      │  │ storage, reflection- │  │ services             │
 │ Identity services  │  │ based endpoint scan  │  │                      │
 └─────────┬──────────┘  └──────────┬───────────┘  └──────────┬───────────┘
           └────────────────────────┼─────────────────────────┘
                                    ▼
                      ┌──────────────────────────────┐
                      │         Application          │  CQRS features, DTOs,
                      │  (use cases + abstractions)  │  repository & service
                      └──────────────┬───────────────┘  interfaces, mappings
                                     ▼
                      ┌──────────────────────────────┐
                      │            Domain            │  Entities, enums
                      └──────────────────────────────┘
```

Dependencies point inwards: **Application** defines interfaces (`IProductReadRepository`, `ITokenHandler`, `IOrderHubService`, …) and the outer layers implement them. Each outer layer exposes a `ServiceRegistration` extension (`AddPersistenceServices`, `AddInfrastructureServices`, `AddApplicationServices`, `AddSignalRServices`) so `Program.cs` only composes them.

## Key technical features

### CQRS with MediatR

Every use case lives in its own folder under `Application/Features` as a `Request` / `Handler` / `Response` triple, split into `Commands` and `Queries`:

```
Features/Products/
├── Commands/CreateProduct/
│   ├── CreateProductCommandRequest.cs
│   ├── CreateProductCommandHandler.cs
│   └── CreateProductCommandResponse.cs
└── Queries/GetProducts/
    ├── GetProductsQueryRequest.cs      # PageNumber, PageSize
    ├── GetProductsQueryHandler.cs
    └── GetProductsQueryResponse.cs
```

Controllers stay thin and only call `IMediator.Send(...)`. Handlers are registered automatically by assembly scanning.

### Generic read/write repositories

Repositories are split by responsibility:

- `IReadRepository<T>`: `GetAll`, `GetWhere`, `GetSingleAsync`, `GetByIdAsync`, each with a `tracking` flag that switches to `AsNoTracking()` for read-only queries
- `IWriteRepository<T>`: `AddAsync`, `AddRangeAsync`, `Remove`, `RemoveRange`, `UpdateAsync`, `SaveAsync`

Entity-specific interfaces (`IProductReadRepository`, `IOrderWriteRepository`, …) derive from these, so they can grow custom queries without touching the generic base.

### Automatic auditing

`ApplicationDbContext` overrides `SaveChangesAsync` and uses the change tracker to set `CreatedDate` on added entities and `UpdatedDate` on modified ones, so handlers never set timestamps by hand.

### JWT authentication with refresh tokens

- Login through ASP.NET Core Identity (`UserManager` / `SignInManager`) with username or email
- `TokenHandler` issues a signed JWT with `Name` and `NameIdentifier` claims (15-minute lifetime) and a refresh token generated with `RandomNumberGenerator`
- The refresh token is stored on the user with an expiry 15 minutes after the access token, and is **rotated** on every refresh
- Token validation checks issuer, audience, signing key and a custom lifetime validator

### Dynamic role-based endpoint authorization

Permissions are not hard-coded with `[Authorize(Roles = "...")]`. Instead, roles are mapped to endpoints **at runtime** and stored in the database.

1. **Annotate** an action with a custom attribute:

   ```csharp
   [HttpPost]
   [AuthorizeDefinition(Menu = "Products", ActionType = ActionType.Writing, Definition = "Create Product")]
   public async Task<IActionResult> Create(CreateProductCommandRequest request) { … }
   ```

2. **Discover**: `ApplicationService` scans the API assembly with reflection, finds every action carrying `[AuthorizeDefinition]`, groups them by menu and builds a unique code such as `POST.Writing.CreateProduct` from the HTTP verb, action type and definition. `GET /api/ApplicationServices` returns this list, so an admin UI can show all protectable endpoints.

3. **Assign**: `POST /api/AuthorizationEndpoints` links roles to an endpoint code. `Menu` and `Endpoint` entities are created on first use and kept in a many-to-many relation with `ApplicationRole`.

4. **Enforce**: `RolePermissionFilter` is registered as a global action filter. On each request it rebuilds the code for the current action and asks `IUserService.HasRolePermissionToEndpointAsync` whether any of the user's roles has access, returning `401` otherwise.

> **Status:** the attribute, discovery, assignment and filter are implemented. The `[Authorize]` and `[AuthorizeDefinition]` attributes on the controllers are currently commented out, so endpoints are open during development.

### Real-time notifications with SignalR

| Hub | Route | Client method | Triggered by |
|---|---|---|---|
| `ProductHub` | `/hubs/producthub` | `receiveProductAddedMessage` | `CreateProductCommandHandler` |
| `OrderHub` | `/hubs/orderHub` | `receiveOrderAddedMessage` | `CreateOrderCommandHandler` |

Handlers depend only on `IProductHubService` / `IOrderHubService` from the Application layer; the SignalR project implements them with `IHubContext<T>`. CORS is configured with credentials enabled, which SignalR requires.

### Structured logging

- **Serilog** writes to the console, `logs/log.txt` and a PostgreSQL `logs` table (created automatically)
- A custom `UsernameColumnWriter` stores the authenticated user's name with each log entry; a small middleware pushes `user_name` into Serilog's `LogContext` for every request
- `UseSerilogRequestLogging` and ASP.NET Core `HttpLogging` record requests and responses (bodies up to 4 KB)

### Global exception handling

`ConfigureExceptionHandler` registers `UseExceptionHandler`, logs the exception and returns a consistent JSON body with status code and message.

### Pluggable file storage

`IStorageService` wraps an `IStorage` implementation chosen with `AddStorage<T>(StorageType)`. `LocalStorage` handles multi-file upload, copy and delete under `wwwroot`; the `StorageType` enum leaves room for cloud providers.

## Domain model

| Entity | Notes |
|---|---|
| `ApplicationUser`, `ApplicationRole` | Identity entities; users hold the refresh token and its expiry |
| `Dealer` | Linked one-to-one to a user; company name, **discount rate**, sales quota, current period sales, contract date |
| `ProductCategory`, `Product` | Code, barcode, stock quantity, base price, customisable flag, image, active flag |
| `Basket`, `BasketItem` | One basket per user; becomes an order on checkout |
| `Order`, `OrderItem` | Totals, discount, final amount, shipping address, estimated and actual delivery dates; items support custom dimensions |
| `Menu`, `Endpoint` | Dynamic authorization metadata, many-to-many with roles |
| `File` | Uploaded file records |

**Order lifecycle** (`OrderStatus`): `Pending → Confirmed → InProduction → QualityControl → InWarehouse → Shipped → Delivered`, or `Cancelled`.

**Checkout flow** (`OrderService.CreateOrderAsync`): loads the basket with its items, rejects empty baskets, computes the total from product base prices, and creates an order that shares the basket's ID (one order per basket). The estimated delivery date defaults to 10 days if none is given. The handler then notifies `OrderHub`.

**Dealer pricing:** `GET /api/Dealers/products` returns the catalogue with each product's base price, the dealer's discount rate and the discounted price.

## API overview

All routes are under `/api`. Swagger UI is available at `/swagger` in the Development environment.

| Controller | Endpoints |
|---|---|
| `Auth` | `POST login`, `POST refresh-token` |
| `Users` | `POST` (register), `GET`, `POST assign-role-to-user`, `GET get-roles-to-user/{UserId}` |
| `Roles` | `GET`, `GET {Id}`, `POST`, `PUT {Id}`, `DELETE {Id}` |
| `ApplicationServices` | `GET`: all endpoints annotated with `[AuthorizeDefinition]` |
| `AuthorizationEndpoints` | `POST`: assign roles to an endpoint; `POST get-roles-to-endpoint` |
| `ProductCategories` | `GET`, `GET {Id}`, `POST`, `PUT`, `DELETE` |
| `Products` | `GET` (paginated), `GET {Id}`, `POST`, `PUT`, `DELETE` |
| `Dealers` | `GET`, `POST`, `PUT`, `DELETE {Id}`, `GET products` (catalogue with discounted prices) |
| `Baskets` | `GET`, `POST` (add item), `PUT` (update quantity), `DELETE {BasketItemId}` |
| `Orders` | `POST` (checkout), `GET`, `GET {Id}` |

## Getting started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL
- EF Core CLI: `dotnet tool install --global dotnet-ef`

### 1. Configure secrets

The database connection string and the JWT signing key are **not stored in the repository**; `appsettings.json` only holds empty placeholders. The API checks both at startup and stops with a clear message if either is missing or if the key is shorter than 32 characters.

**Local development: user secrets**

```bash
cd EvosancomAPI.API
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5433;Database=EvosancomAPI;Username=postgres;Password=<password>"
dotnet user-secrets set "Token:SecurityKey" "<at least 32 random characters>"
```

User secrets are stored outside the project folder and loaded automatically in the Development environment. To generate a strong key:

```bash
openssl rand -base64 48
```

**Other environments: environment variables** (`__` maps to nested keys)

```bash
export ConnectionStrings__DefaultConnection="Host=...;Password=..."
export Token__SecurityKey="..."
```

Non-secret settings (`Token:Issuer`, `Token:Audience`, Serilog levels) stay in `appsettings.json`.

### 2. Create the database

```bash
dotnet ef database update --project Persistence --startup-project EvosancomAPI.API
```

### 3. Run

```bash
dotnet run --project EvosancomAPI.API
```

The API listens on `http://localhost:5149` (and `https://localhost:7052` with the `https` profile). Open **http://localhost:5149/swagger** to explore it.

## Project structure

```
EvosancomAPI/
├── Domain/                     # Entities, enums
├── Application/
│   ├── Features/               # CQRS commands and queries per aggregate
│   ├── Abstractions/           # Service, hub, storage and token interfaces
│   ├── Repositories/           # Read/write repository interfaces
│   ├── CustomAttributes/       # [AuthorizeDefinition]
│   ├── DTOs/, Mappings/        # DTOs and AutoMapper profile
│   └── ServiceRegistration.cs
├── Persistence/
│   ├── Contexts/               # ApplicationDbContext (Identity + auditing)
│   ├── Repositories/           # Generic and entity repositories
│   ├── Services/               # Auth, user, role, basket, order, dealer services
│   └── Migrations/
├── Infrastructure/
│   └── Services/               # TokenHandler, storage, endpoint discovery
├── SignalR/
│   ├── Hubs/                   # ProductHub, OrderHub
│   └── HubServices/
└── EvosancomAPI.API/
    ├── Controllers/
    ├── Filters/                # RolePermissionFilter
    ├── Extensions/             # Global exception handler
    ├── Configurations/         # Serilog column writers
    └── Program.cs
```
