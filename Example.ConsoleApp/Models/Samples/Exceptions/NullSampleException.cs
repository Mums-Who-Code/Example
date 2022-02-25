// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using Xeptions;

namespace Example.ConsoleApp.Models.Samples.Exceptions
{
    public class NullSampleException : Xeption
    {
        public NullSampleException()
            : base(message: "Sample is null.")
        { }
    }
}
