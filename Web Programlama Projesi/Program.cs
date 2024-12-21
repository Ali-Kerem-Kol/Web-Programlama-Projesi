using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Web_Programlama_Projesi.Data;

var builder = WebApplication.CreateBuilder(args);

// IHttpContextAccessor'� servis olarak ekleyin
builder.Services.AddHttpContextAccessor();

// PostgreSQL veritaban� ba�lant�s� i�in DbContext yap�land�rmas�
builder.Services.AddDbContext<KuaferContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Session'� yap�land�r�yoruz
builder.Services.AddDistributedMemoryCache(); // Session i�in bellek tabanl� cache
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum s�resi (30 dakika)
    options.Cookie.HttpOnly = true; // Sadece HTTP �zerinden eri�ilebilir
    options.Cookie.IsEssential = true; // �erez gerekli
});

// Cookie tabanl� kimlik do�rulama yap�land�rmas�
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Kullan�c� giri� yapmam��sa buraya y�nlendirilecek
        options.AccessDeniedPath = "/Account/AccessDenied"; // Yetkisiz eri�im i�in y�nlendirme
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Oturum s�resi
        options.SlidingExpiration = true; // Oturum s�resi her istekle yenilenecek
    });

// HttpClient ekliyoruz (gerekliyse)
builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// HTTPS y�nlendirmesi ve statik dosyalar
app.UseHttpsRedirection();
app.UseStaticFiles();

// Kimlik do�rulama middleware'ini ekliyoruz
app.UseAuthentication(); // Kimlik do�rulama i�lemi

app.UseRouting();

// Session middleware'ini kullan�yoruz
app.UseSession();

// Authorization middleware'ini ekliyoruz
app.UseAuthorization();

// MVC route yap�land�rmas�
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
