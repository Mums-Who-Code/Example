// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using Example.ConsoleApp.Models.Samples;
using Example.ConsoleApp.Models.Samples.Exceptions;
using Moq;
using System;
using Xunit;

namespace Example.Tests.Unit.Services.Foundations.Samples
{
    public partial class SampleServiceTests
    {        
        [Fact]
        public void ShouldThrowValidationExceptionOnRetrieveByIdIfSampleIsInvalidAndLogIt()
        {
            // given
            var invalidSample = new Sample();

            var invalidSampleException = new InvalidSampleException();

            invalidSampleException.AddData(
                key: nameof(Sample.Id),
                values: "Id is required");

            var expectedSampleValidationException = new SampleValidationException(invalidSampleException);

            // when
            Action retrieveSampleById = () => this.sampleService.RetrieveSampleById(invalidSample.Id);

            // then
            Assert.Throws<SampleValidationException>(retrieveSampleById);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSampleValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectSampleById(It.IsAny<int>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
