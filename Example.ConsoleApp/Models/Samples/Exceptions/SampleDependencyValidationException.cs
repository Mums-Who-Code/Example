// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using Xeptions;

namespace Example.ConsoleApp.Models.Samples.Exceptions
{
    public class SampleDependencyValidationException : Xeption
    {
        public SampleDependencyValidationException(Xeption innerException)
            : base(message: "Sample dependency validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
