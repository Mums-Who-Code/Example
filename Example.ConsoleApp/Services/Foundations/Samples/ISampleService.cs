// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using Example.ConsoleApp.Models.Samples;
using System.Collections.Generic;

namespace Example.ConsoleApp.Services.Foundations.Samples
{
    public interface ISampleService
    {
        Sample AddSample(Sample sample);
        List<Sample> RetrieveAllSamples();
        Sample RetrieveSampleById(int id);
        Sample ModifySample(Sample sample);
        Sample RemoveSample(Sample sample);
    }
}
