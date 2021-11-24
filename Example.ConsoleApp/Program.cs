// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using Example.ConsoleApp.Brokers.Loggings;
using Example.ConsoleApp.Brokers.Outputs;
using Example.ConsoleApp.Brokers.Storages;
using Example.ConsoleApp.Models.Samples;
using Example.ConsoleApp.Models.Samples.Exceptions;
using Example.ConsoleApp.Services.Foundations.Samples;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Example.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var outputBroker = new OutputBroker();

            try
            {
                var storageBroker = new StorageBroker();
                var loggerFactory = new LoggerFactory();
                var logger = new Logger<LoggingBroker>(loggerFactory);
                var loggingBroker = new LoggingBroker(logger);
                var sampleService = new SampleService(storageBroker, loggingBroker);

                //Sample nullSample = null;
                //sampleService.AddSample(nullSample);

                //Sample invalidSample = new Sample();
                //sampleService.AddSample(invalidSample);

                Sample sample = new Sample { Id = 1, Text = "One" };
                sampleService.AddSample(sample);
                List<Sample> storedSamples = sampleService.RetrieveAllSamples();
                PrintOutput(storedSamples, outputBroker);
                sample.Text = "Modified";
                sampleService.ModifySample(sample);
                storedSamples = sampleService.RetrieveAllSamples();
                PrintOutput(storedSamples, outputBroker);
                sample = new Sample { Id = 2, Text = "Two" };
                sampleService.AddSample(sample);
                storedSamples = sampleService.RetrieveAllSamples();
                PrintOutput(storedSamples, outputBroker);
                sampleService.RemoveSample(sample);
                PrintOutput(storedSamples, outputBroker);
            }
            catch(SampleValidationException sampleValidationException)
            {
                outputBroker.Display(sampleValidationException.InnerException.Message);
            }
            catch (SampleServiceException exception)
            {
                outputBroker.Display(exception.Message);
            }
        }

        static void PrintOutput(List<Sample> samples, OutputBroker outputBroker)
        {
            foreach (var sample in samples)
            {
                outputBroker.Display($"Id: {sample.Id}, Text: {sample.Text}");
            }
        }
    }
}
