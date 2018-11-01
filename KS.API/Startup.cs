using AutoMapper;
using KS.Business.DataContract.Authorization;
using KS.Business.Managers.Authorization;
using KS.Database.Authorization.Commands;
using KS.Database.Authorization.Invokers;
using KS.Database.Authorization.Receivers;
using KS.Database.Contexts;
using KS.Database.DataContract.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace KS.API
{
    //TODO: 7 (See slack) add code
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
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value);
            services.AddDbContext<KSContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<IRegisterUserManager, RegisterUserManager>();
            services.AddScoped<IAuthorizationCommand, RegisterUserCreateCommand>();
            services.AddScoped<IUserRegisterInvoker, RegisterUserCreateInvoker>();
            services.AddScoped<IAuthorizationReceiver, RegisterUserCreateReceiver> ();
            services.AddScoped<IExistingUserCommand, ExistingUserCommand>();
            services.AddScoped<IExistingUserInvoker, ExistingUserInvoker>();
            services.AddScoped<IExistingUserReceiver, ExistingUserReceiver>();
            services.AddScoped<ILoginManager, LoginManager>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) //<----You'll need all of this, too
                    .AddJwtBearer(options => {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });//<--- Up to here
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        
    }
}
