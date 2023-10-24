using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// This line adds support for controllers, which are part of ASP.NET Core's MVC framework.

builder.Services.AddEndpointsApiExplorer();
// This line configures API Explorer for generating documentation for your API endpoints.
// API Explorer provides information about your API, making it easier to understand and use.

builder.Services.AddSwaggerGen();
// This line configures Swagger, a popular tool for API documentation. SwaggerGen generates Swagger/OpenAPI documentation for your API.

builder.Services.AddEndpointsApiExplorer();
// This is another configuration for API Explorer, specifically for use with MVC.

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    // This line sets the default API version when none is specified.
    options.DefaultApiVersion = new ApiVersion(1, 0);
    // It defines the default API version as 1.0.
    options.ReportApiVersions = true;
    // This option enables reporting of available API versions.
    options.ApiVersionReader = new HeaderApiVersionReader();
    // It specifies that API version information should be read from HTTP headers.
}).AddApiExplorer(options => {
    options.GroupNameFormat = "'v'VVV";
    // This line defines the format for grouping API versions in the documentation.
    options.SubstituteApiVersionInUrl = true;
    // It specifies that API versions should be substituted in the URL.
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // When the application is in development mode, Swagger UI is enabled.
    app.UseSwaggerUI();
    // This line adds a user-friendly interface for Swagger documentation.
}

app.UseHttpsRedirection();
// Redirect HTTP requests to HTTPS for security.

app.UseAuthorization();
// Apply authorization logic to your application.

app.MapControllers();
// Map and route HTTP requests to the appropriate controllers and actions.

app.Run();
// Start the application and listen for incoming HTTP requests.
