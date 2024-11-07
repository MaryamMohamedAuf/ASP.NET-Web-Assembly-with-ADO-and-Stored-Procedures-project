using Microsoft.AspNetCore.ResponseCompression;
using SOFCOproject.Server.Services;
using SOFCOproject.Server.Interface;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<UserService>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<Iuser, UserRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseDeveloperExceptionPage();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //HTTP Strict Transport Security (HSTS), which is a security feature that helps to protect websites against protocol downgrade attacks and cookie hijacking.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
