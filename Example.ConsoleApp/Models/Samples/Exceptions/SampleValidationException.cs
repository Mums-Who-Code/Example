// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using Xeptions;

namespace Example.ConsoleApp.Models.Samples.Exceptions
{
    public class SampleValidationException : Xeption
    {
        public SampleValidationException(Xeption innerException)
            : base(message: "Sample validation error occurred, fix the errors and try again.", innerException)
        { }
    }
}
