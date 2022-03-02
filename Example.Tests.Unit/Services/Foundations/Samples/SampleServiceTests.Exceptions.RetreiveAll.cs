// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using System;
using Example.ConsoleApp.Models.Samples.Exceptions;
using Moq;
using Xunit;

namespace Example.Tests.Unit.Services.Foundations.Samples
{
    public partial class SampleServiceTests
    {
        [Fact]
        public void ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogIt()
        {
            // given
            var serviceException = new Exception();

            var failedSampleServiceException =
                new FailedSampleServiceException(serviceException);

            var expectedSampleServiceException =
                new SampleServiceException(failedSampleServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllSamples())
                    .Throws(serviceException);

            // when
            Action retrieveAllAction = () => this.sampleService.RetrieveAllSamples();

            // then
            Assert.Throws<SampleServiceException>(retrieveAllAction);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllSamples(),
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
