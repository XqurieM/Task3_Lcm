var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});
var app = builder.Build();
app.UseCors("AllowAll");
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Use(async (context, next) =>
{
    var request = context.Request;

    var startTime = DateTime.UtcNow;

    var ip = context.Connection.RemoteIpAddress?.ToString();

    Console.WriteLine("---- REQUEST ----");
    Console.WriteLine($"Time: {startTime:yyyy-MM-dd HH:mm:ss} UTC");
    Console.WriteLine($"IP: {ip}");
    Console.WriteLine($"Method: {request.Method}");
    Console.WriteLine($"Path: {request.Path}");
    Console.WriteLine($"Query: {request.QueryString}");

    foreach (var header in request.Headers)
    {
        Console.WriteLine($"Header: {header.Key} = {header.Value}");
    }

    // =========================
    // RESPONSE CAPTURE START
    // =========================
    var originalBodyStream = context.Response.Body;
    using var responseBody = new MemoryStream();

    context.Response.Body = responseBody;

    await next();

    // Response body oku
    context.Response.Body.Seek(0, SeekOrigin.Begin);
    var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
    context.Response.Body.Seek(0, SeekOrigin.Begin);

    // Log
    var endTime = DateTime.UtcNow;
    var duration = (endTime - startTime).TotalMilliseconds;

    Console.WriteLine($"Response Status: {context.Response.StatusCode}");
    Console.WriteLine($"Response Body: {responseText}");
    Console.WriteLine($"Duration: {duration} ms");
    Console.WriteLine("------------------");

    // Response'u geri yaz (CRITICAL)
    await responseBody.CopyToAsync(originalBodyStream);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
