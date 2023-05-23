using _3_Asp.Net_MVC.IServices;
using _3_Asp.Net_MVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IProductServices, ProductServices>();
builder.Services.AddSession(option=>
{
    option.IOTimeout = TimeSpan.FromMinutes(10);
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();// Ứng dụng tạo ra chỉ mặc định truy cập với https
app.UseStaticFiles();


app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute( // Định tuyến 
    name: "default", // Tên Route
    pattern: "{controller=Home}/{action=AllProduct}/{id?}"); // Mẫu URL
app.Run();
