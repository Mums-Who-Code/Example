// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using Example.ConsoleApp.Brokers.Loggings;
using Example.ConsoleApp.Brokers.Storages;
using Example.ConsoleApp.Models.Samples;
using System.Collections.Generic;

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
        TryCatch(() => this.storageBroker.SelectAllSamples());

        public Sample RetrieveSampleById(int id) =>
        TryCatch(() =>
        {
            ValidateInput(id);
            
            return this.storageBroker.SelectSampleById(id);
        });

        public Sample ModifySample(Sample sample) =>
        TryCatch(() =>
        {
            ValidateSample(sample);
            
            return this.storageBroker.UpdateSample(sample);
        });

        public Sample RemoveSample(Sample sample) =>
        TryCatch(() =>
        {
            ValidateSample(sample);

            return this.storageBroker.DeleteSample(sample);
        });
    }
}
