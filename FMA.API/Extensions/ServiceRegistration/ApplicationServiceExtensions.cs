using FMA.BLL.Mappers;
using FMA.BLL.Services.Implementations;
using FMA.BLL.Services.Interfaces;
using FMA.BLL.Utilities;
using FMA.DAL.Context;
using FMA.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace FMA.API.Extensions.ServiceRegistration
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddDbContext<FootballMatchAppContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //Service
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ITeamMemberService, TeamMemberService>();
            services.AddScoped<IMatchPostService, MatchPostService>();
            services.AddScoped<IPitchService, PitchService>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<UserUtility>();

            return services;
        }
    }
}
