using Microsoft.Extensions.Logging;
using ToDo.Pages;
using TodoSQLite.Data;

namespace ToDo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Pattaya-Regular.ttf", "Pattaya-Regular");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<Home>();
            builder.Services.AddTransient<Edit>();

            builder.Services.AddSingleton<TasksService>();
            return builder.Build();
        }
    }
}
