using DotNetEnv;
using Services.ChatboxService;
using Services.ChatService;
using Services.FeedbackService;
using Services.MembershipService;
using Services.OrderService;
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

            builder.Services.AddRazorPages();
            builder.Services.AddSession();
            builder.Services.AddSignalR();
            builder.Services.AddSingleton<IChatService>(ChatService.Instance);
            builder.Services.AddSingleton<IFeedbackService>(FeedbackService.Instance);
            builder.Services.AddSingleton<IMembershipService>(MembershipService.Instance);
            builder.Services.AddSingleton<IOrderService>(OrderService.Instance);
            builder.Services.AddSingleton<IReportService>(ReportService.Instance);
            builder.Services.AddSingleton<ISubscriptionService>(SubscriptionService.Instance);
            builder.Services.AddSingleton<ITicketService>(TicketService.Instance);
            builder.Services.AddSingleton<IUserService>(UserService.Instance);

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

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
