// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using Example.ConsoleApp.Models.Samples;
using Example.ConsoleApp.Models.Samples.Exceptions;
using Xeptions;

namespace Example.ConsoleApp.Services.Foundations.Samples
{
    public partial class SampleService
    {
        private delegate Sample ReturningSampleFunction();

        private Sample TryCatch(ReturningSampleFunction returningSampleFunction)
        {
            try
            {
                return returningSampleFunction();
            }
            catch (NullSampleException nullSampleException)
            {
                throw CreateAndLogValidationException(nullSampleException);
            }
        }

        private SampleValidationException CreateAndLogValidationException(Xeption exception)
        {
            var sampleValidationException = new SampleValidationException(exception);
            this.loggingBroker.LogError(sampleValidationException);

            throw sampleValidationException;
        }
    }
}
