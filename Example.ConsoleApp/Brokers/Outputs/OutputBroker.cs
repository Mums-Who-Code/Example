// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using System;

namespace Example.ConsoleApp.Brokers.Outputs
{
    public class OutputBroker : IOutputBroker
    {
        public void Display(string message) =>
            Console.WriteLine(message);

        public string Read() =>
            Console.ReadLine();
    }
}
