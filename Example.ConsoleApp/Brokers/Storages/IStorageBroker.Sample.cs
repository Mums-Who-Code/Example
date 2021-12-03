// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using Example.ConsoleApp.Models.Samples;

namespace Example.ConsoleApp.Brokers.Storages
{
    internal partial interface IStorageBroker
    {
        Sample InsertSample(Sample sample);
    }
}
