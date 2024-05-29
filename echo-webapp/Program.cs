using Microsoft.Extensions.Logging.Console;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(opts =>
    {
        opts.IncludeScopes = false;
        opts.TimestampFormat = "[yyyy-MM-dd HH:mm:ss] ";
        opts.ColorBehavior = LoggerColorBehavior.Disabled;
    });

builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy",
            builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
    });
// Add Azure App Configuration middleware to the container of services.
builder.Services.AddAzureAppConfiguration();
// Add feature management to the container of services.
builder.Services.AddFeatureManagement();

// Load configuration from Azure App Configuration
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(builder.Configuration["AppConfigConnString"]);
    // Load all feature flags with no label
    options.UseFeatureFlags();
});
var app = builder.Build();

app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
