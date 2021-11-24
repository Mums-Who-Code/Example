// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using Example.ConsoleApp.Models.Samples;
using System.Collections.Generic;

namespace Example.ConsoleApp.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        Sample InsertSample(Sample sample);
        List<Sample> SelectAllSamples();
        Sample SelectSampleById(int sampleId);
        Sample UpdateSample(Sample sample);
        Sample DeleteSample(Sample sample);
    }
}
