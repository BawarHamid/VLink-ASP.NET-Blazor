using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System.Text;
using Vikarlink.ApiInterface.Database;
using Vikarlink.ApiInterface.Repositories.AdminContract;
using Vikarlink.ApiInterface.Repositories.ElevContract;
using Vikarlink.ApiInterface.Repositories.KlasseVaerelseContract;
using Vikarlink.ApiInterface.Repositories.VagtContract;
using Vikarlink.ApiInterface.Repositories.VikarContract;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddSwaggerGen(swagger =>
{
    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter ‘Bearer’ [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = "Bearer"
        }
      },
      new string[] {}
    }
  });
});

builder.Services.AddDbContext<VikarlinkContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("VikarDBConnection")));

builder.Services.AddIdentity<UserEntity, IdentityRole>(options =>
{
    options.Password = new PasswordOptions()
    {
        RequiredLength = 6,
        RequireDigit = true,
        RequireLowercase = true,
        RequireNonAlphanumeric = false,
        RequireUppercase = true,
        RequiredUniqueChars = 0
    };
})
 .AddEntityFrameworkStores<VikarlinkContext>()
  .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IVikarRepository, VikarRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IVagtRepository, VagtRepository>();
builder.Services.AddScoped<IElevRepository, ElevRepository>();
builder.Services.AddScoped<IKlasseVaerelseRepository, KlasseVaerelseRepository>();

builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:7219", "https://localhost:7219")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin();
    }));



// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

//Adding Jwt Bearer
  .AddJwtBearer(options =>
  {
      options.SaveToken = true;
      options.RequireHttpsMetadata = false;
      options.TokenValidationParameters = new TokenValidationParameters()
      {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidAudience = builder.Configuration["JWT:ValidAudience"],
          ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
      };
  });




var app = builder.Build();





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
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor API V1");
});

//app.UseCors(policy =>
//    policy.WithOrigins("http://localhost:7219", "https://localhost:7219")
//    .AllowAnyMethod()
//    .WithHeaders(HeaderNames.ContentType)
//);




app.UseRouting();
app.UseCors("NgOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
























//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddDbContext<VikarlinkContext>(options => 
//options.UseSqlServer(builder.Configuration.GetConnectionString("VikarDBConnection")));

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddScoped<IVikarRepository, VikarRepository>();
//builder.Services.AddScoped<IAdminRepository, AdminRepository>();
//builder.Services.AddScoped<IVagtRepository, VagtRepository>();
//builder.Services.AddScoped<IElevRepository, ElevRepository>();
//builder.Services.AddScoped<IKlasseVaerelseRepository, KlasseVaerelseRepository>();

//var app = builder.Build();

//app.UseCors(x => x
//                    .AllowAnyMethod()
//                    .AllowAnyHeader()
//                    .SetIsOriginAllowed(origin => true) // allow any origin
//                    .AllowCredentials()); // allow credentials

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
