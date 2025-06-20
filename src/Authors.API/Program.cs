using Books;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure HttpClient for Books.API
builder.Services.AddHttpClient("BooksAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:5051/");
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    // Only for development
    ServerCertificateCustomValidationCallback = 
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
});

// Register a singleton HttpClient that will be injected into AuthorsController
builder.Services.AddSingleton(sp => 
{
    return sp.GetRequiredService<IHttpClientFactory>().CreateClient("BooksAPI");
});

// Add gRPC client
builder.Services.AddGrpcClient<BookService.BookServiceClient>(options =>
{
    options.Address = new Uri("http://localhost:5851");
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    // Only for development
    ServerCertificateCustomValidationCallback = 
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
});

// Add OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// Map controller endpoints
app.MapControllers();

app.Run();
