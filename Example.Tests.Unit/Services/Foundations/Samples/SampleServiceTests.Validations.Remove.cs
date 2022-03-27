// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using System;
using Example.ConsoleApp.Models.Samples;
using Example.ConsoleApp.Models.Samples.Exceptions;
using Moq;
using Xunit;

namespace Example.Tests.Unit.Services.Foundations.Samples
{
    public partial class SampleServiceTests
    {
        [Fact]
        public void ShouldThrowValidationExceptionOnRemoveIfSampleIsNullAndLogIt()
        {
            // given
            Sample nullSample = null;
            var nullSampleException = new NullSampleException();

            var expectedSampleValidationException =
                new SampleValidationException(nullSampleException);

            // when
            Action removeSampleAction = () => this.sampleService.RemoveSample(nullSample);

            // then
            Assert.Throws<SampleValidationException>(removeSampleAction);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSampleValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteSample(It.IsAny<Sample>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
