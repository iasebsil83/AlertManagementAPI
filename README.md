# Alert Management API

&nbsp;
&nbsp;




## Overview

### Description

This project is a web API for exchanging custom items that we call **alerts**.\
Alerts are specific data structures, stored and retrieved from a local database.

This project is made exclusively using [.NET](https://dotnet.microsoft.com/en-us) in C# and rely on an *InMemory* Database.

&nbsp;

### Download

This project produces no downloadable binaries yet.

&nbsp;

### Project Structure

For more simplicity, this project has been re-organized into a simpler structure then the original ".NET" one.\
You should find your way easily with directories `app`, `cfg`, `src` and `tst` to configure, edit, execute and test the project.

&nbsp;
&nbsp;




## Use

### Execution requirements

Here are the requirements to **run** the project:
- [.NET](https://dotnet.microsoft.com/en-us), version 10.0

&nbsp;

### Execution process

To run the project, you first have to **build** it (next section).\
Then, just run:
```bash
app/run
```

&nbsp;

### Usage

For precise usage of the API in itself, see document(s) in the `doc/` directory.

&nbsp;
&nbsp;




## Build

### Build requirements

Here are all the requirements to build the project:
- Bash
- DotNet SDK, version 10.0

&nbsp;

### Build process

To build the project, just run:
```bash
bld/build
```

&nbsp;

### Clean

To clean build generation and temporary runtime files, run:
```bash
bld/clean
```

&nbsp;
&nbsp;




*By Sebastien SILVANO*
