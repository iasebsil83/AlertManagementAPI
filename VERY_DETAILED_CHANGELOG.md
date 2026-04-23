### [2026-04-22-11-21]
- Wrote the modification rules in PUT.
  Wrote overriding statements in POST.
  Tested POST: OK
  Tested PUT: **Something is wrong with IDs.**
  When making specific requests, our custom *Alert* attribute makes interference with internal IDs provided as primary key in our InMemory database.
  => **Maybe remove our custom ID attribute and only rely on the internal one**

- Fixed the README for real (I forgot actually).
  Made FULL DOCUMENTATION about the Alerts API: `doc/Alerts.md`

- Reading generated code to see how it works.\
  Commented some parts, reformated some others.


### [2026-04-22-18-47]
- Updated README to mention *InMemory* database management instead of the initial PostgreSQL \
  I initially though required.

- Processed the "scaffholding" technique to build other API related code.\
  Checked controler code, everything has been generated as desired: ***success***\
  Tried requests using the Swagger interface: we can create, remove, get one, or get all alerts.

- Reformatted `Program.cs` (trying to understand what it does precisely).\
  Added Alert model.\
  Added the use of an *InMemory* database `AlertBase`.


### [2026-04-22-18-17]
- Added the Swagger package, following the documentation.\
  Checked it at `http://localhost:5000/swagger` to see project current state.\
  **Finally some real accurate information about our API!!**\
  We only have the **GET /Alert** request available for the moment.\
  I still don't know what is the criteria in the code that allows this request...\
  (Is it the "Get" function in our controller? Does it take the name from the return type, \
  the controller name or the file name?)\
  Anyway, let's continue.

- Added ASP.Net directory to SSL certifications:
  ```bash
  dotnet dev-certs https --trust #gave me the path to the dev certs directory of ASP.Net
  export SSL_CERT_DIR="$HOME/.aspnet/dev-certs/trust:/usr/lib/ssl/certs"
  dotnet dev-certs https --trust
   ```

- Removed useless files (`.http` & dev `appsettings.json`)\
  Checked the differences between the new tutorial template and our result: same thing.\
  Added nugget InMemory:
  ```
  dotnet add package Microsoft.EntityFrameworkCore.InMemory
  ```

- I couldn't find that much info so I switched on [this DotNet Guide](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api).\
  It should enable us to do the following requests:
  ```
  GET    /api/item      Get all items
  GET    /api/item/{id} Get an item by ID
  POST   /api/item      Add a new item
  PUT    /api/item/{id} Update an existing item
  DELETE /api/item/{id} Delete an item
  ```
  Let's GO!


### [2026-04-22-17-45]
- Struggled a bit with **DateOnly**  documentation.\
  Tried the classical **DateTime**... but finally returned to DateOnly anyway.\
  Using a null value for the moment.\
  **Anyway**, it works now with our `/alert` termination.\
  => commiting & pushing

- Renamed and changed `Controllers/WeatherForecastController.cs`: "WeatherForecast" => "Alert"\
  Renamed and changed `WeatherForecast.cs` into `Alert.cs` with the following attributes:
  - `int      ID`
  - `string   Message`
  - `STATUS   Status` (enum created for the occasion)
  - `string   Area`
  - `DateTime CreatedAt`

- Checked tree structure of the project to identify source files:\
  Sources are:
  - `Program.cs`
  - `WeatherForecast.cs`
  - `Controllers/WeatherForecastController.cs`
  Some `Debug` directories have been addded to the project under different directories.\
  They contain a **lot** of messy things => cleaning them using:
  ```
  rm $(find -type d -name Debug) -rf
  ```
  Adding this command in a `clean` executable file in project + updating README to get that new cleaning feature.

- Tested project (default template) using `dotnet run` and opening web browser at:
  - `http://localhost:5000`: got nothing (normal)
  - `http://localhost:5000/weatherforecast`: got expected JSON result
  - `https://localhost:7086`: got nothing
  - `https://localhost:7086/weatherforecast`: got nothing + **<ins>WARNING</ins>** in app console:
    ```
    warn: Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionMiddleware[3]
      Failed to determine the https port for redirect.
    ```

- Identified project configuration inside `Properties/launchSettings.json`.\
  => **UNCOMMON... Maybe to change into a cfg/ directory later...**

- Checking dot net documentation to make a [WebAPI](https://learn.microsoft.com/en-us/training/modules/build-web-api-aspnet-core/3-exercise-create-web-api)\
  Started with the default template:
  ```bash
  dotnet new webapi -controllers -f net10.0
  ```
  The following structure has been created:
  ```
  ├── AlertManagementAPI.csproj
  ├── AlertManagementAPI.http
  ├── appsettings.Development.json
  ├── appsettings.json
  ├── Controllers
  │   └── WeatherForecastController.cs
  ├── obj
  │   ├── AlertManagementAPI.csproj.nuget.dgspec.json
  │   ├── AlertManagementAPI.csproj.nuget.g.props
  │   ├── AlertManagementAPI.csproj.nuget.g.targets
  │   ├── project.assets.json
  │   └── project.nuget.cache
  ├── Program.cs
  ├── Properties
  │   └── launchSettings.json
  ├── README.md
  ├── VERY_DETAILED_CHANGELOG.md
  └── WeatherForecast.cs
  ```
