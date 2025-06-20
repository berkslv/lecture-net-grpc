using Books.API.Grpc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddGrpc();
builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapGrpcReflectionService();
}

// Map gRPC service endpoints
app.MapGrpcService<BooksService>();

// Map controller endpoints
app.MapControllers();

app.Run();
