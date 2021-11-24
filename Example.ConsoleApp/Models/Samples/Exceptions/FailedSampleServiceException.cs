// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using System;
using Xeptions;

namespace Example.ConsoleApp.Models.Samples.Exceptions
{
    public class FailedSampleServiceException : Xeption
    {
        public FailedSampleServiceException(Exception innerException)
            : base(message: "Failed sample service error occurred, contact support.", innerException)
        { }
    }
}
