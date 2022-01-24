// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using System;
using Microsoft.Extensions.Logging;

namespace Example.ConsoleApp.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        public ILogger logger { get; set; }

        public LoggingBroker(ILogger<LoggingBroker> logger) =>
            this.logger = logger;

        public void LogError(Exception exception) =>
            this.logger.LogError(exception, exception.Message);
    }
}
