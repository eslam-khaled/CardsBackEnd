using BusinessLogic;
using BusinessLogic.BusinessInterfaces;
using Cards.DomainServices.Services;
using EPLHouse.Cards.DataAccess.CustomRepository;
using EPLHouse.Cards.DataAccess.Models;
using EPLHouse.Test.DataAccess.Models;
using EPLHouse.Test.DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.Text;


namespace EPLHouse.Cards.Api
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

            //Add My DbContext
            services.AddDbContext<AppDBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("AppDBContextConnection")));
            services.AddControllers();

            services.AddScoped<IBaseRepository<AppUser>, BaseRepository<AppUser>>();
            services.AddScoped<IBaseRepository<Client>, BaseRepository<Client>>();
            services.AddScoped<IBaseRepository<Account>, BaseRepository<Account>>();
            services.AddScoped<IBaseRepository<CardsLevel>, BaseRepository<CardsLevel>>();
            services.AddScoped<IBaseRepository<Card>, BaseRepository<Card>>();
            services.AddScoped<IBaseRepository<PrintRequests>, BaseRepository<PrintRequests>>();

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IPrintRepository, PrintRepository>();

            services.AddScoped<IReportPrint, ReportPrintBusiness>();
            services.AddScoped<ICardBusiness, CardBusiness>();
            services.AddScoped<IClientBusiness, ClientBusiness>();

            // ===== Add Identity ========

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDBContext>()
                .AddDefaultTokenProviders();

            //===== Add Jwt Authentication ========


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyHeader().Build());
            });
            services.AddControllers();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(cfg =>
            {
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = Configuration["Jwt:JwtIssuer"],
                    ValidAudience = Configuration["Jwt:JwtIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:JwtKey"])),
                    ClockSkew = TimeSpan.Zero // remove delay of token when expire
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            // global cors policy
            app.UseCors("CorsPolicy");


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
