﻿// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using Example.ConsoleApp.Brokers.Loggings;
using Example.ConsoleApp.Brokers.Storages;
using Example.ConsoleApp.Models.Samples;
using Example.ConsoleApp.Services.Foundations.Samples;
using Microsoft.Extensions.Logging;

namespace Example.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var storageBroker = new StorageBroker();
            var loggerFactory = new LoggerFactory();
            var logger = new Logger<LoggingBroker>(loggerFactory);
            var loggingBroker = new LoggingBroker(logger);
            var sampleService = new SampleService(storageBroker, loggingBroker);

            var inputSample = new Sample
            {
                Id = 24,
                Text = "Example"
            };

            sampleService.AddSample(inputSample);
        }
    }
}
