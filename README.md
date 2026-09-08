\# CatFacts Recruitment Task



Simple ASP.NET Core Web API created as a recruitment task.



The application retrieves a random cat fact from:



https://catfact.ninja/fact



Each retrieved response is appended as a new line to a local `.txt` file.



\## Technologies



\- .NET 10

\- ASP.NET Core Web API

\- Dependency Injection

\- HttpClientFactory

\- xUnit

\- Moq



\## Endpoint



```http

GET /api/catfacts

```



Example response:



```json

{

&#x20; "fact": "Cats sleep for around 16 hours a day.",

&#x20; "length": 41

}

```



Each response is also appended to:



```text

CatFacts.Api/data/catfacts.txt

```



\## Configuration



The external API URL, timeout and file path are stored in `appsettings.json`.



Example:



```json

{

&#x20; "CatFactApi": {

&#x20;   "BaseUrl": "https://catfact.ninja/",

&#x20;   "TimeoutSeconds": 5

&#x20; },

&#x20; "FileStorage": {

&#x20;   "Path": "data/catfacts.txt"

&#x20; }

}

```



\## Running the project



```bash

dotnet restore

dotnet build

dotnet run --project CatFacts.Api

```



Then call:



```http

GET /api/catfacts

```



\## Tests



Run tests with:



```bash

dotnet test

```



The project contains a unit test for `CatFactService` using Moq.



\## Technical decisions



\- `HttpClientFactory` is used for communication with the external API.

\- `CancellationToken` is propagated through the request flow.

\- `SemaphoreSlim` protects concurrent writes to the local file.

\- `ILogger<T>` is used for structured logging.

\- `IExceptionHandler` and `ProblemDetails` provide centralized error handling.

\- Configuration values are stored in `appsettings.json`.



The project intentionally does not use CQRS, MediatR, a database or additional architectural layers because they are not required for this small use case.

