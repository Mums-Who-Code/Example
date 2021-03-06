// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using System.Collections.Generic;
using Example.ConsoleApp.Models.Samples;

namespace Example.ConsoleApp.Brokers.Storages
{
    public partial class StorageBroker : IStorageBroker
    {
        List<Sample> Samples = new List<Sample>();

        public Sample InsertSample(Sample sample)
        {
            Samples.Add(sample);

            return sample;
        }

        public List<Sample> SelectAllSamples() => Samples;

        public Sample SelectSampleById(int id) =>
            Samples.Find(sample => sample.Id == id);

        public Sample UpdateSample(Sample inputSample)
        {
            Samples.RemoveAll(sample => sample.Id == inputSample.Id);
            Samples.Add(inputSample);

            return inputSample;
        }

        public Sample DeleteSample(Sample sample)
        {
            Samples.Remove(sample);

            return sample;
        }
    }
}
