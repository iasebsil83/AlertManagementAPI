- Struggled a bit with **DateOnly**  documentation.
  Tried the classical **DateTime**... but finally returned to DateOnly anyway.
  Using a null value for the moment.
  **Anyway**, it works now with our `/alert` termination.
  => commiting & pushing

- Renamed and changed `Controllers/WeatherForecastController.cs`: "WeatherForecast" => "Alert"
  Renamed and changed `WeatherForecast.cs` into `Alert.cs` with the following attributes:
  - `int      ID`
  - `string   Message`
  - `STATUS   Status` (enum created for the occasion)
  - `string   Area`
  - `DateTime CreatedAt`

- Checked tree structure of the project to identify source files:
  Sources are:
  - `Program.cs`
  - `WeatherForecast.cs`
  - `Controllers/WeatherForecastController.cs`
  Some `Debug` directories have been addded to the project under different directories.
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

- Identified project configuration inside `Properties/launchSettings.json`.
  => **UNCOMMON... Maybe to change into a cfg/ directory later...**

- Checking dot net documentation to make a [WebAPI](https://learn.microsoft.com/en-us/training/modules/build-web-api-aspnet-core/3-exercise-create-web-api)
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
