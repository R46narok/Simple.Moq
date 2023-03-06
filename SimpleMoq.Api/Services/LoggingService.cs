namespace SimpleMoq.Api.Services;

public interface ILoggingService
{
    public void LogInformation(string message, params object[] parameters);
}

public class LoggingService : ILoggingService
{
    private readonly ILogger<LoggingService> _logger;

    public LoggingService(ILogger<LoggingService> logger)
    {
        _logger = logger;
    }
    
    public void LogInformation(string message, params object[] parameters)
    {
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        _logger.LogInformation(message, parameters);
    }
    
}