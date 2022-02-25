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
        public void ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogIt()
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
                broker.InsertSample(It.IsAny<Sample>()))
                    .Throws(serviceException);

            // when
            Action addSampleAction = () => this.sampleService.AddSample(someSample);

            // then
            Assert.Throws<SampleServiceException>(addSampleAction);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSample(It.IsAny<Sample>()),
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
