// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using System;
using Xeptions;

namespace Example.ConsoleApp.Models.Samples.Exceptions
{
    public class NullArgumentSampleException : Xeption
    {
        public NullArgumentSampleException(Exception innerException)
            : base(message: "Null argument sample error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
