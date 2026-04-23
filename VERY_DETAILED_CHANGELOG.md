### [2026-04-23-14-10]
- Added a build process and removed some cfg files.\
  No more mess in project structure.


### [2026-04-23-14-10] NEWBRANCH new-prj-arch
- Made a new architecture for the project.


### [2026-04-23-13-44]
- Added a few tests commands in a `tst` directory.

- Tested if DELETE causes problem with ID: **it does**
  Primary keys are never reset, but our instances do reset every time...
  It is **very annoying**!!! How are we supposed to work with such an environment!
  Anyway, I also figured out that PUT requests were having the same kind of "ID" issue.
  => Too much pain for getting information about this **mysterious** `InMemory` database => stopping project here.


### [2026-04-23-11-50]
- Note that the swagger doesn't allow us to send specific ID requests (form issue), but otherwise regular CURL works.

- **Found more information.**\
  The previous `_context.Alerts.Add()` don't want an Alert of ID 0, so it sets it to 1 anyway.\
  **BUT, it doesn't affect our ID otherwise!!!**\
  Which means, we <ins>**still**</ins> have to secure the ID attribution process, because in the current situation:
  - POSTing 2 times the same ID causes an internal memory error (same primary key for 2 elements).
  - We can POST whatever ID we want... can quickly become messy.\
  **HOWEVER**, we also saw another issue.\
  `lastID` is reset at each **POST** request (=> new instanciation of controller at each request...).\
  So, **we can't rely on controller attributes**.\
  Same thing for the **Model** instance (tried to set lastID in it), which means even the `_context` is reloaded every time.\
  => Therefore, let's rely on the number of stored Alerts to get the lastID.
  => **Pretty sure we will have problem when deleting something!**

- **Found where the issue with the ID occurs.**\
  There is **no** default ID management in InMemory database.\
  The first attribute we define is used as primary key (I couldn't find any information about it in .NET documentations, but anyway).\
  Our ID attribution mechanism works, but it oppose to the InMemory <ins>addition</ins> in the **POST** request.\
  Here is the POST content concerned:
  ```C#
  //set initial attributes
  Console.WriteLine($"ALERT_ID:{alert.ID}");
  Console.WriteLine($"LAST_ID:{lastID}");
  Console.WriteLine("OPERATE");
  alert.ID        = lastID++;
  Console.WriteLine($"ALERT_ID:{alert.ID}");
  Console.WriteLine($"LAST_ID:{lastID}");
  alert.Status    = STATUS.DRAFT;
  alert.CreatedAt = DateOnly.FromDateTime(DateTime.Now);

  //add a new alert
  Console.WriteLine("ADD");
  _context.Alerts.Add(alert);
  Console.WriteLine($"ALERT_ID:{alert.ID}");
  Console.WriteLine($"LAST_ID:{lastID}");
  Console.WriteLine("SAVE");
  await _context.SaveChangesAsync();
  Console.WriteLine($"ALERT_ID:{alert.ID}");
  Console.WriteLine($"LAST_ID:{lastID}");
  ```
  And here the output:
  ```
  ALERT_ID:8  <--- here it can be anything (user input)
  LAST_ID:0   <--- lastId init at 0
  OPERATE
  ALERT_ID:0  <--- lastId set in Alert object (overwrite)
  LAST_ID:1   <--- and then increased (until here, everything is OK)
  ADD
  ALERT_ID:1  <--- MODIFICATION IN THE Alert OBJECT !!!!
  LAST_ID:1   <--- and our custom lastId is not updated!
  SAVE
  info: Microsoft.EntityFrameworkCore.Update[30100]
        Saved 1 entities to in-memory store.
  ALERT_ID:1  <--- No modification after save: good
  LAST_ID:1
  ```
  The issue is that `_context.Alerts.Add()` **modifies** the ID we give him.


### [2026-04-23-11-21]
- Wrote the modification rules in PUT.\
  Wrote overriding statements in POST.\
  Tested POST: OK\
  Tested PUT: **Something is wrong with IDs.**\
  When making specific requests, our custom *Alert* attribute makes interference with internal IDs provided as primary key in our InMemory database.\
  => **Maybe remove our custom ID attribute and only rely on the internal one**

- Fixed the README for real (I forgot actually).\
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
