using Authorization.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddUserSecrets<Program>();
builder.Services.AddIdentity<CustomIdentityUser, IdentityRole<int>>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
})
    .AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders();

var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
builder.Services.AddDbContext<Context>(options =>
    options
        .UseSnakeCaseNamingConvention()
        .UseMySql(builder.Configuration.GetConnectionString("Connection"), serverVersion)
);

builder.Services.AddScoped<UserService, UserService>();
builder.Services.AddScoped<LoginService, LoginService>();
builder.Services.AddScoped<TokenService, TokenService>();
builder.Services.AddScoped<EmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();