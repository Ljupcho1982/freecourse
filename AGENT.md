# AGENT.md

Guidance for AI agents and developers working in this repository.

## Architecture

This project uses **Vertical Slice Architecture** with **CQRS** and a **custom mediator** implementation.

### Core principles

- **Vertical slices over layers.** Organize code by feature, not by technical concern. Each feature/use case is self-contained — its command/query, handler, validation, and related types live together in one slice.
- **CQRS.** Separate the write side (Commands) from the read side (Queries). Commands change state; queries return data. Do not mix the two.
- **Custom mediator.** Use our own mediator implementation to dispatch commands and queries to their handlers. Do **not** pull in MediatR or another third-party mediator library.
- **No services unless necessary.** Do not create service classes by default. Put logic in the slice's handler. Only introduce a service when behavior is genuinely shared across slices or has a real reason to be abstracted — and justify it.
- **No repositories.** Do not introduce the repository pattern. Handlers talk to the data source (e.g. EF Core `DbContext`) directly.
- **Best practices always.** Follow current .NET/C# best practices: async/await throughout, cancellation tokens, nullable reference types, immutable request types where sensible, input validation, and clear error handling.
- **Dependency injection.** Use the built-in DI container. When a service *is* necessary, register it and inject it via the constructor rather than instantiating it directly.
- **One type per file.** Every class, record, interface, and enum goes in its own file. No combining multiple types into a single file.

## Database

When persistence is required:

- **SQL Server Express** as the database engine.
- **Entity Framework Core** as the ORM.
- **Code-first** with **EF Core migrations**. Define the model in code, then generate and apply migrations — do not hand-edit the database schema.

### Migration commands

```bash
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

## Conventions summary

| Concern            | Decision                                              |
|--------------------|-------------------------------------------------------|
| Structure          | Vertical slices, grouped by feature                   |
| Read/write split   | CQRS (Commands vs. Queries)                           |
| Dispatch           | Custom mediator (no MediatR)                          |
| Services           | Only when necessary; otherwise logic lives in handler |
| Repositories       | None — use `DbContext` directly                       |
| DI                 | Built-in container, constructor injection             |
| File organization  | One type per file                                     |
| Database           | SQL Server Express                                    |
| Data access        | EF Core, code-first with migrations                   |
