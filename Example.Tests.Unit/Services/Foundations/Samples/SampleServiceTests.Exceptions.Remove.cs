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
        public void ShouldThrowServiceExceptionOnRemoveIfServiceErrorOccursAndLogIt()
        {
            // given
            Sample someSample = CreateRandomSample();
            var serviceException = new Exception();

            var failedSampleServiceException =
                new FailedSampleServiceException(serviceException);

            var expectedSampleServiceException =
                new SampleServiceException(
                    failedSampleServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteSample(It.IsAny<Sample>()))
                    .Throws(serviceException);

            // when
            Action removeSampleAction = () => this.sampleService.RemoveSample(someSample);

            // then
            Assert.Throws<SampleServiceException>(removeSampleAction);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteSample(It.IsAny<Sample>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSampleServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
