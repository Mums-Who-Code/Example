// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using System.Collections.Generic;
using Example.ConsoleApp.Brokers.Loggings;
using Example.ConsoleApp.Brokers.Storages;
using Example.ConsoleApp.Models.Samples;

namespace Example.ConsoleApp.Services.Foundations.Samples
{
    public partial class SampleService : ISampleService
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
        TryCatch(() =>
        {
            ValidateSample(sample);

            return this.storageBroker.InsertSample(sample);
        });

        public List<Sample> RetrieveAllSamples() =>
        TryCatch(() =>
        {
            return this.storageBroker.SelectAllSamples();
        });

        public Sample RetrieveSampleById(int id) =>
        TryCatch(() =>
        {
            ValidateInput(id);

            return this.storageBroker.SelectSampleById(id);
        });

        public Sample ModifySample(Sample sample) =>
            throw new System.NotImplementedException();
    }
}
