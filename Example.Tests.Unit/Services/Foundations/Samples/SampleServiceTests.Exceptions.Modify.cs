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
        public void ShouldThrowServiceExceptionOnModifyIfServiceErrorOccursAndLogIt()
        {
            // given
            Sample someSample = CreateRandomSample();
            var exception = new Exception();
            var failedSampleServiceException = new FailedSampleServiceException(exception);
            var expectedSampleServiceException = new SampleServiceException(failedSampleServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateSample(It.IsAny<Sample>()))
                    .Throws(exception);

            // when
            Action modifySampleAction = () => this.sampleService.ModifySample(someSample);

            // then
            Assert.Throws<SampleServiceException>(modifySampleAction);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateSample(It.IsAny<Sample>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSampleServiceException))),
                        Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
