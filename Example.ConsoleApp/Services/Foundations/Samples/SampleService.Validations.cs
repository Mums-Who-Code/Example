// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using Example.ConsoleApp.Models.Samples;
using Example.ConsoleApp.Models.Samples.Exceptions;

namespace Example.ConsoleApp.Services.Foundations.Samples
{
    public partial class SampleService
    {
        private static void ValidateSample(Sample sample)
        {
            if (sample == null)
            {
                throw new NullSampleException();
            }
        }
    }
}
