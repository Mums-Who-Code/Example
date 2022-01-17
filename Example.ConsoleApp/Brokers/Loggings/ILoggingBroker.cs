// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using System;

namespace Example.ConsoleApp.Brokers.Loggings
{
    internal interface ILoggingBroker
    {
        void LogError(Exception exception);
    }
}
