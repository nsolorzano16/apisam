namespace apisam.web
{
    using apisam.entities;
    using apisam.interfaces;
    using apisam.repos;
    using apisam.repositories;
    using apisam.web.Data;
    using apisam.web.Mapping;
    using AutoMapper;
    using iText.Kernel.Geom;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.ResponseCompression;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using System;
    using System.IO.Compression;

   
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container test.
        public void ConfigureServices(IServiceCollection services)
        {
            
          //  var _conn = Configuration.GetConnectionString("local");
            var _conn = Configuration.GetConnectionString("azure_dbcon");


            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_conn));

            IdentityBuilder builder = services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 6;
                opt.User.RequireUniqueEmail = true;
                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5.0);


            }
            );

            builder = new IdentityBuilder(builder.UserType, typeof(UserRole), builder.Services);
            builder.AddEntityFrameworkStores<ApplicationDbContext>();
            builder.AddSignInManager<SignInManager<User>>();
            builder.AddRoleManager<RoleManager<UserRole>>();
            builder.AddDefaultTokenProviders();

         


            services.AddCors(options =>
            {
                options.AddPolicy("Todos",
                builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

       

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
           AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   IssuerSigningKey =
                   new SymmetricSecurityKey(
                       System.Text.Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                   ValidateIssuerSigningKey = true,
                   
               };
           });

            //services.AddAuthorization(options => options.AddPolicy("AppPolicy", policy => policy.RequireRole("1", "2", "3")));


            services.AddControllers();


            var _license = @"6543-e1JlZjo2NTQzLE5hbWU6Sm9zZSBCb25pbGxhLFR5cG
                            U6SW5kaWUsTWV0YTowLEhhc2g6YUhKaXAzVk01V0JuVXZpa
                            GsxdnU0NVlwMWpHcSswVkpybFU5dUI4Tk85V3VWWkdIcWtu
                            Y2JqaFBCNHRXMWtobVoyem1wS3ZoWi9lYmVmYlV5WEF4S3d
                            2clEwSzE2eGRISjlzT0tqSHl1dkg0NkxUbkdEYTV5SUdNMj
                            FyY1NpalB0MmRRRUovTWtZeUJnc2xpZXRrRW5XQUNKMEZWd
                            TdyK3pXWTRkd3RrSEQwPSxFeHBpcnk6MjAxOS0wOS0yNH0=";
            ServiceStack.Licensing.RegisterLicense(_license);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SAM-API", Version = "v1" });
        

            });



            services.AddTransient<IUsuario, UsuariosRepo>();
            services.AddTransient<IPaciente, PacientesRepo>();
            services.AddTransient<IAntecedentesFamiliaresPersonales, AntecedentesRepo>();
            services.AddTransient<IDiagnosticos, DiagnosticosRepo>();
            services.AddTransient<IExamenFisico, ExamenFisicoRepo>();
            services.AddTransient<IExamenFisicoGinecologico, ExamenFisicoGinecologicoRepo>();
            services.AddTransient<IFarmacosUsoActual, FarmacosRepo>();
            services.AddTransient<IHabitos, HabitosRepo>();
            services.AddTransient<IHistorialGinecoObstetra, HistorialGinecoObstetraRepo>();
            services.AddTransient<IPreclinica, PreclinicaRepo>();
            services.AddTransient<INotas, NotasRepo>();
            services.AddTransient<IDepartamento, DepartamentosRepo>();
            services.AddTransient<IEscolaridad, EscolaridadRepo>();
            services.AddTransient<IGrupoEtnico, GrupoEtnicoRepo>();
            services.AddTransient<IGrupoSanguineo, GrupoSanguineoRepo>();
            services.AddTransient<IMunicipio, MunicipioRepo>();
            services.AddTransient<IProfesion, ProfesionRepo>();
            services.AddTransient<IReligion, ReligionRepo>();
            services.AddTransient<IPais, PaisRepo>();
            services.AddTransient<IConsulta, ConsultaRepo>();
            services.AddTransient<IExamenCategoria, ExamenCategoriaRepo>();
            services.AddTransient<IExamenDetalle, ExamenDetalleRepo>();
            services.AddTransient<IExamenTipo, ExamenTipoRepo>();
            services.AddTransient<IExamenIndicado, ExamenIndicadoRepo>();
            services.AddTransient<IPlanTerapeutico, PlanTerapeuticoRepo>();
            services.AddTransient<IViaAdministracion, ViaAdministracionRepo>();
            services.AddTransient<IAuditoria, AuditoriaRepo>();
            services.AddTransient<IDevices, DevicesRepo>();
            services.AddTransient<ICalendarioFecha, CalendarioFechaRepo>();
            services.AddTransient<IAnticonceptivos, AnticonceptivosRepo>();
            services.AddTransient<ICie, CieRepo>();
            services.AddTransient<IFotosPaciente, FotosPacienteRepo>();
            services.AddTransient<IPlanes, PlanesRepo>();
            services.AddTransient<IDashboard, DashboardRepo>();
            services.AddSingleton<IEmail, EmailRepo>();



            services.Configure<MailJet>(Configuration.GetSection("MailJet"));

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddAutoMapper(typeof(MappinProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();
            // Handles exceptions and generates a custom response body
          app.UseExceptionHandler("/errors/500");
            // Handles non-success status codes with empty body
           app.UseStatusCodePagesWithReExecute("/errors/{0}");


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            app.UseRouting();
            app.UseCors("Todos");
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapHub<PreclinicaHub>("/preclinicaHub");
            });
        }
    }
}
