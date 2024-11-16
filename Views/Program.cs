using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using DotNetEnv;
using Services;
using Services.ChatService;
using Services.FeedbackService;
using Services.MembershipService;
using Services.OrderService;
using Services.PayOSService;
using Services.ReportService;
using Services.SubscriptionService;
using Services.TicketService;
using Services.UserService;

namespace Views
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Env.Load();

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddRazorPages();
            builder.Services.AddSession();
            builder.Services.AddSignalR();
            builder.Services.AddSingleton<IPayOSService>(PayOSService.Instance);
            builder.Services.AddSingleton<IChatService>(ChatService.Instance);
            builder.Services.AddSingleton<IFeedbackService>(FeedbackService.Instance);
            builder.Services.AddSingleton<IMembershipService>(MembershipService.Instance);
            builder.Services.AddSingleton<IOrderService>(OrderService.Instance);
            builder.Services.AddSingleton<IReportService>(ReportService.Instance);
            builder.Services.AddSingleton<ISubscriptionService>(SubscriptionService.Instance);
            builder.Services.AddSingleton<ITicketService>(TicketService.Instance);
            builder.Services.AddSingleton<IUserService>(UserService.Instance);
            builder.Services.AddNotyf(config =>
            {
                config.DurationInSeconds = 10;
                config.IsDismissable = true;
                config.Position = NotyfPosition.TopRight;
            });
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseNotyf();
            app.UseSession();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            app.MapHub<SignalRHub>("/signalrhub");
            app.Run();
        }
    }
}
