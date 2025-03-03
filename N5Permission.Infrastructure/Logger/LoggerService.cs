



using Serilog;

namespace N5Permission.Infrastructure.Logger
{
    public sealed class LoggerService : ILoggerService
    {
        private readonly ILogger _logger;

        public LoggerService() => _logger = Log.ForContext<LoggerService>();
        public void LogError(string message, Exception ex = null)
        {
            if (ex is not null)
                _logger.Error(message, ex);
            else
                _logger.Error(message);

        }

        public void LogInformation(string message)
        {
            _logger.Information(message);
        }
    }
}
