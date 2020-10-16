using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DependencyInjectionExampleWPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console().CreateLogger();

            Log.Logger.Information("hello world");

            var host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
                {
                    services.AddTransient<IMessageService, MessageService>();
                })
                .UseSerilog().Build();

            var svc = ActivatorUtilities.CreateInstance<IMessageService>(host.Services);
            svc.Run();



            SetupApp();
        }

        private static void SetupApp()
        {
            // Console.WriteLine("grgr");
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIORNMENT") ?? "Production"}.json",
                    optional: true)
                .AddEnvironmentVariables();
        }
    }
}
