// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

namespace Example.ConsoleApp.Brokers.Outputs
{
    public interface IOutputBroker
    {
        void Display(string message);
        string Read();
    }
}
