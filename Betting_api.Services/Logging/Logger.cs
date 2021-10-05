using Microsoft.Extensions.Logging;
using System;

namespace evona_hackathon.Services.Logging
{
    //this logger doesnt do anything yet
    public class Logger:ILogger
    {
        public Logger()
        {

        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            throw new NotImplementedException();
        }
    }
}
