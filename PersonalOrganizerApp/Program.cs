using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace PersonalOrganizerApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //SmsController.SendTextMessage(SmsController.CommandListToString());
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
