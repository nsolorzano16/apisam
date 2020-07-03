using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using apisam.entities;
//using apisam.entities.ViewModels.UsuariosTable;
using apisam.interfaces;
using apisam.repos;
using apisam.repositories;
using apisam.web.Hubs;
using apisam.web.Mapping;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;



namespace apisam.web
{
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

            var _conn = Configuration.GetConnectionString("azure_dbcon");
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(_conn)
            );

            


            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("Todos",
                builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

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



            #region
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




            #endregion

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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey =
                        new SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddSignalR();


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
            app.UseAuthentication();

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<PreclinicaHub>("/preclinicaHub");
            });
        }
    }
}
