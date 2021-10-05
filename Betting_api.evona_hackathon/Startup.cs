using Betting_api.DB_Models;
using evona_hackathon.Data;
using evona_hackathon.Infrastructure;
using evona_hackathon.Models.V1_Models;
using evona_hackathon.Services.Auth;
using evona_hackathon.Services.Filters;
using evona_hackathon.Services.IRepos;
using evona_hackathon.Services.Mapping;
using evona_hackathon.Services.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public void ConfigureServices(IServiceCollection services)
        {
            //cors doesnt work...i dont know why (must use chrome cors blocker plugin for endpoint comsumption)
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200");
                    });
            });
            //mapper
            services.AddAutoMapper(typeof(BasicProfile));

            //dependency injection for IConfiguring
            services.AddSingleton(Configuration);
          
            //authentication
            var key = Configuration.GetValue<string>("Token"); //getting toke from app configuration

            var token = Encoding.ASCII.GetBytes(key); //encoding the key with ASCII

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt => {
                jwt.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey=new SymmetricSecurityKey(token), //using simple symetric key for signing
                    ValidateIssuerSigningKey=true, //requesting validation of the issuer key
                    ValidateIssuer=true, //requesting issuer validation
                    ValidateAudience=true, //requesting audience validation
                    ValidIssuer= "https://localhost:44398/",
                    ValidAudiences = new List<string> { "https://localhost:44398/", "https://localhost:4200/" }//swagger and angular localhost addresses 
                };
            });


            //default controllers adding + filter registering (globaly available to all controllers)
            //current filter implementation only supports synchronous execution
            services.AddControllers(x=> {
                 x.Filters.Add<ErrorHandelingFilter>();
                 x.Filters.Add<BaseResourcesFilter>();
                 x.Filters.Add<BaseActionFilter>();
              }
            );

            //swagger documentation specification
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Betting_api.evona_hackathon", Version = "v1" });
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "bearerAuth"
                                }
                            },
                            new string[] {}
                    }
                });
            });

            //sql server + EF db context service
            //services.AddDbContext<DB_Context>();

            //services registration

            services.AddInfrastructure();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //swagger development 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Betting_api.evona_hackathon v1"));
            }

            //cors blocker removed

            //default routing and redirecting options
            app.UseHttpsRedirection();
            app.UseRouting();

            //cors doesnt work...i dont know why (must use chrome cors blocker plugin for endpoint comsumption)
            app.UseCors(MyAllowSpecificOrigins);

            //auth
            app.UseAuthentication();
            app.UseAuthorization();

            //controller mapping
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
