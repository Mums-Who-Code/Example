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
        public void ShouldThrowServiceExceptionOnRetrieveByIdIfServiceErrorOccursAndLogIt()
        {
            // given
            int someId = GetRandomNumber(); 
            var exception = new Exception();
            var failedSampleServiceException = new FailedSampleServiceException(exception);
            var expectedSampleServiceException = new SampleServiceException(failedSampleServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectSampleById(It.IsAny<int>()))
                    .Throws(exception);

            // when
            Action retrieveSampleByIdAction = () => this.sampleService.RetrieveSampleById(someId);

            // then
            Assert.Throws<SampleServiceException>(retrieveSampleByIdAction);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectSampleById(It.IsAny<int>()),
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
