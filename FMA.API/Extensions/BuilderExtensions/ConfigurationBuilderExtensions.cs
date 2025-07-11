using FMA.Common.Settings;

namespace FMA.API.Extensions.BuilderExtensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static WebApplicationBuilder AddAppConfiguration(this WebApplicationBuilder builder)
        {
            //EmailSettings
            builder.Services.Configure<EmailSettingModel>(
                builder.Configuration.GetSection("Email"));

            // Các config khác 

            // Middleware Performance
            builder.Services.Configure<PerformanceSetting>(
                builder.Configuration.GetSection("Middleware:Performance"));



            return builder;
        }
    }
}
