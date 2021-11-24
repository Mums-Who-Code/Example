// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using Xeptions;

namespace Example.ConsoleApp.Models.Samples.Exceptions
{
    public class InvalidSampleException : Xeption
    {
        public InvalidSampleException()
            : base(message: "Invalid sample input, fix the errors and try again.")
        { }
    }
}
