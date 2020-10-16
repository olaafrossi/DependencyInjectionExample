using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DependencyInjectionExampleWPFUI
{

    public class MessageService : IMessageService
    {
        private readonly ILogger<MessageService> _log;
        private readonly IConfiguration _config;

        public MessageService(ILogger<MessageService> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }
        public void Run()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Hello {i}");
                _log.LogInformation("run number {run number}", i);
            }
        }
    }
}