using Microsoft.EntityFrameworkCore;
using Web_Programlama_Projesi.Data;

var builder = WebApplication.CreateBuilder(args);

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Session middleware'ini kullan�yoruz
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
