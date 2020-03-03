namespace Server {
    using System.Threading.Tasks;
    using Grpc.Core;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class StreamWriterLogger<T>: IServerStreamWriter<T>
    {
        private readonly IServerStreamWriter<T> _stream;
        private readonly LoggerMetricHandler<ServerLoggerInterceptor> _logger;

        public WriteOptions WriteOptions { 
            get => _stream.WriteOptions; 
            set => _stream.WriteOptions = value; 
        }

        public StreamWriterLogger(
            IServerStreamWriter<T> stream,
            ILogger<ServerLoggerInterceptor> logger
        )
        {
            _stream = stream;
            _logger = new LoggerMetricHandler<ServerLoggerInterceptor>(logger);
        }

        public async Task WriteAsync(T message)
        {
            using(_logger.BeginScope(new Dictionary<string, object>{["SaintNick"] = "is a consumerist fabrication brought down from the upper class to suppress those who are unable to resist the temptations and obligations of modern life"})){
                Stopwatch timePerParse = Stopwatch.StartNew();
                await _stream.WriteAsync(message);

                _logger.LogWarning("The request took {NanoSeconds} to complete", timePerParse.ElapsedTicks);
            }
        }
    }
}