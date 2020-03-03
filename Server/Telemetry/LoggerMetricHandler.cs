namespace Server 
{
    using System;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    public class LoggerMetricHandler<T> : ILogger<T> {
        private readonly ILogger<T>  _logger;

        public LoggerMetricHandler(ILogger<T> logger) 
        {
            this._logger = logger;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return _logger.BeginScope(state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _logger.IsEnabled(logLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            SomeSchema? someSchema = (state as IEnumerable<KeyValuePair<string, object>>)?.ToSomeSchema(i => i.Key, i => i.Value);
            if (someSchema != null)
            {
                someSchema.DoDaMetricLogging(_logger);
            }

            _logger.Log(logLevel, eventId, state, exception, formatter);
        }
    }
}