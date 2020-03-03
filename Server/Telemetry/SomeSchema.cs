namespace Server {
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Logging;
    public class SomeSchema {
        
        public string? SaintNick;
        public long? ElapsedSeconds;

        public void DoDaMetricLogging<T>(ILogger<T> _logger)
        {
            if(_logger.IsEnabled(LogLevel.Warning))
            {
                //_logger.LogCritical("neato! the metrics are being logged!");
                Console.WriteLine(string.Format("neato! the metrics are being logged! {0}: {1}, {2}: {3}", nameof(SomeSchema.ElapsedSeconds), ElapsedSeconds, nameof(SomeSchema.SaintNick), SaintNick));
            }
        }

        public void FromDictionary(IDictionary<string, object> dict) 
        {
            SaintNick = (string)dict.GetValueOrDefault(nameof(this.SaintNick), "And nobody cared OwO => UwU => 🐈");
            ElapsedSeconds = (long)dict.GetValueOrDefault(nameof(this.ElapsedSeconds), 0L);
        }

        public IDictionary<string, object> ToDictionary()
        {
            var dict = new Dictionary<string, object>();
            dict[nameof(this.SaintNick)] = SaintNick ?? "And nobody cared OwO => UwU => 🐈";
            dict[nameof(this.ElapsedSeconds)] = ElapsedSeconds ?? 0L;

            return dict;
        }
    }
}