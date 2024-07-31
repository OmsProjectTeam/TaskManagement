


using static Infarstuructre.BL.IIRolsInformation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ViewmMODeElMASTER>();

// إضافة خدمات إلى الحاوية
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MasterDbcontext>(options => {
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("MasterConnection"),
		sqlOptions => sqlOptions.CommandTimeout(180) // تحديد مهلة الاتصال بـ 180 ثانية
	);
	options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
	};
});

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

	var securityScheme = new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme.",
		Reference = new OpenApiReference
		{
			Type = ReferenceType.SecurityScheme,
			Id = "Bearer"
		}
	};

	c.AddSecurityDefinition("Bearer", securityScheme);

	var securityRequirement = new OpenApiSecurityRequirement
	{
		{ securityScheme, new[] { "Bearer" } }
	};

	c.AddSecurityRequirement(securityRequirement);
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredUniqueChars = 0;
	options.Password.RequiredLength = 5;
	options.Password.RequireNonAlphanumeric = false;
	options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<MasterDbcontext>();

builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/Admin/Accounts/Login";
	options.AccessDeniedPath = "/Admin/Home/Denied";
	options.Cookie.Name = "Cookie";
	options.Cookie.HttpOnly = true;
	options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
	options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
	options.SlidingExpiration = true;
});

builder.Services.AddScoped<IIUserInformation, CLSUserInformation>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IIRolsInformation, CLSRolsInformation>();









builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}
else
{
	app.UseDeveloperExceptionPage();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();

app.UseSession();

// Middleware للتحقق من تسجيل الدخول قبل الوصول إلى Swagger
app.Use(async (context, next) =>
{
	if (context.Request.Path.StartsWithSegments("/api-docs") || context.Request.Path.StartsWithSegments("/swagger"))
	{
		if (!context.User.Identity.IsAuthenticated)
		{
			context.Response.Redirect("/Admin/Accounts/Login");
			return;
		}
	}
	await next.Invoke();
});

app.MapControllerRoute(
	name: "areas",
	pattern: "{area:exists}/{controller=Accounts}/{action=Login}/{id?}"
);

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.UseSwagger();

app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Shipping System V1");
	c.RoutePrefix = "api-docs";
});

app.Run();
