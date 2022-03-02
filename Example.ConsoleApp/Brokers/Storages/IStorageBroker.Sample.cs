﻿// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using System.Collections.Generic;
using Example.ConsoleApp.Models.Samples;

namespace Example.ConsoleApp.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        Sample InsertSample(Sample sample);
        List<Sample> SelectAllSamples();
    }
}
