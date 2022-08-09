using ChatPlatformProject.Server.Hubs;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Default
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//Add service of SignalR and add ResponseCompression
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(options =>
    options.MimeTypes = ResponseCompressionDefaults
    .MimeTypes.Concat(new[] { "application/octet-stream" })   // Determines format of file sent??
); ;

var app = builder.Build();

//Add Middleware :
app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
//Enables connection to chathub from the client
app.MapHub<ChatHub>("/chathub");
app.MapFallbackToFile("index.html");

app.Run();
