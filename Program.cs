using Microsoft.EntityFrameworkCore;
using AlertManagementAPI.Models;




// ---------------- INITIALIZATION ----------------

//create API (from CLI args if given)
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AlertContext>(opt =>
    opt.UseInMemoryDatabase("AlertBase")
);

//prepare API run instance
var app = builder.Build();

//dev status => enable OpenAPI + Swagger
if( app.Environment.IsDevelopment() ){
    app.MapOpenApi();

    //Swagger
    app.UseSwaggerUi(options => {
        options.DocumentPath = "/openapi/v1.json";
    });
}

//enable https <<<<<<<<<<<<<<<<<<<<<<<<<<< DOESN'T WORK!
app.UseHttpsRedirection();
app.UseAuthorization(); // <<<<<<<<<<<<<<< check for certificates?

//create API handlers <<<<<<<<<<<<<<<<<<<<<<<<<<<<< not sure
app.MapControllers();




// ---------------- EXECUTION ----------------

//run
app.Run();
