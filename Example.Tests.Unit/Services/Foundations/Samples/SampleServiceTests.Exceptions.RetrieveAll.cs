// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using Example.ConsoleApp.Models.Samples.Exceptions;
using Moq;
using System;
using Xunit;

namespace Example.Tests.Unit.Services.Foundations.Samples
{
    public partial class SampleServiceTests
    {
        [Fact]
        public void ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogIt()
        {
            // given
            var exception = new Exception();

            var failedSampleServiceException = new FailedSampleServiceException(exception);
            var expectedSampleServiceException = new SampleServiceException(failedSampleServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllSamples())
                    .Throws(exception);

            // when
            Action retrieveAllSamplesAction = () => this.sampleService.RetrieveAllSamples();

            // then
            Assert.Throws<SampleServiceException>(retrieveAllSamplesAction);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllSamples(),
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
