using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Alpari.Models.Context;
using WebApi.Alpari.Models.Services;
using WebApi.Alpari.Models.Services.Validator;

namespace WebApi.Alpari
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();


            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(Configureoptions =>
            {
                Configureoptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = Configuration["JWtConfig:issuer"],
                    ValidAudience = Configuration["JWtConfig:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWtConfig:Key"])),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                };
                Configureoptions.SaveToken = true;    //HttpContext.GetTokenAsync();
                Configureoptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        //log 
                        //.........
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        // log
                        var tokenvalidatorService = context.HttpContext.RequestServices.GetRequiredService<ITokenValidator>();
                        return tokenvalidatorService.Execute(context);
                    },
                    OnChallenge = context =>
                    {
                        //log 
                        return Task.CompletedTask;
                    },

                    OnMessageReceived = context =>
                    {
                        return Task.CompletedTask;
                    },
                    OnForbidden = context =>
                    {
                       return Task.CompletedTask;
                    }
                };
            });


            string conStr = "Persist Security Info=False;User ID=sa;Password=Enssme@204;Initial Catalog=ERFAN;Data Source=10.45.56.200";
            services.AddEntityFrameworkSqlServer().AddDbContext<DataBaseContext>(option=>option.UseSqlServer(conStr));
            services.AddScoped<ITokenValidator, TokenValidate>();
            services.AddScoped<TodoRepository, TodoRepository>();
            services.AddScoped<UserRepository, UserRepository>();
            services.AddScoped<UserTokenRepository,UserTokenRepository>();
            services.AddScoped<CategoryRepository, CategoryRepository>();

            services.AddApiVersioning(options=>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            services.AddSwaggerGen(c =>
            { 
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi.Alpari", Version = "v1" });
            }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi.Alpari v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
