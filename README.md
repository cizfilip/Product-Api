# Product-Api

## How to run
Run it with dotnet tool using
`dotnet run --project ProductAPI/ProductAPI.csproj --configuration Development`
You can then navigate to swagger page [http://localhost:5298/swagger/index.html](http://localhost:5298/swagger/index.html)

Tests can be run using
`dotnet test ProductAPI.Tests/ProductAPI.Tests.csproj`

## Technologies used
- CRUD API is backed by InMemory provider from Entity Framework
- Uses Bogus library to seed database
- Uses xUnit for testing
- Uses simple repository pattern with common CRUD repository implementation
- Supports paging for products
- Uses DTO objects to prevent over posting




