
using Day_3.Models;
using Day_3.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Day_3
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			builder.Services.AddDbContext<ITI_DBContext>(
				options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
			builder.Services.AddScoped<IUserRepo, UserRepo>();
			builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
			builder.Services.AddScoped<UnitOfWork>();
			builder.Services.AddScoped<IStudentRepo, StudentRepo>();
			builder.Services.AddScoped<ICourseRepo, CourseRepo>();

			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					string secret = builder.Configuration["JWT:Secret"];
					var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
					options.TokenValidationParameters = new TokenValidationParameters
					{
						IssuerSigningKey = key,
						ValidateIssuer = false,
						ValidateAudience = false
					};

				});

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme.",
					Type = SecuritySchemeType.Http,
					Scheme = "bearer"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
							new string[] { }
						}
				});
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseCors(options =>
			{
				options.AllowAnyOrigin()
					   .AllowAnyMethod()
					   .AllowAnyHeader();
			});

			app.UseAuthentication();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
