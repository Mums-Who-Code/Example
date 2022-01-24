// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using Example.ConsoleApp.Brokers.Loggings;
using Example.ConsoleApp.Brokers.Storages;
using Example.ConsoleApp.Models.Samples;

namespace Example.ConsoleApp.Services.Foundations.Samples
{
    public class SampleService : ISampleService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public SampleService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public Sample AddSample(Sample sample) =>

            this.storageBroker.InsertSample(sample);
    }
}
