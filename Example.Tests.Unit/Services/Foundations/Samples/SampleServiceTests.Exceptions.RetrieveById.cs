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
        public void ShouldThrowDependencyValidationExceptionOnRetrieveByIdIfValidationErrorOccursAndLogIt()
        {
            // given
            int someSampleId = GetRandomNumber();
            var argumentNullException = new ArgumentNullException();

            var nullArgumentSampleException =
                new NullArgumentSampleException(argumentNullException);

            var expectedSampleDependencyValidationException =
                new SampleDependencyValidationException(
                    nullArgumentSampleException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectSampleById(It.IsAny<int>()))
                    .Throws(argumentNullException);

            // when
            Action retrieveSampleByIdAction = () =>
                this.sampleService.RetrieveSampleById(someSampleId);

            // then
            Assert.Throws<SampleDependencyValidationException>(retrieveSampleByIdAction);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectSampleById(It.IsAny<int>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSampleDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowServiceExceptionOnRetrieveByIdIfServiceErrorOccursAndLogIt()
        {
            // given
            int someSampleId = GetRandomNumber();
            var serviceException = new Exception();

            var failedSampleServiceException =
                new FailedSampleServiceException(serviceException);

            var expectedSampleServiceException =
                new SampleServiceException(failedSampleServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectSampleById(It.IsAny<int>()))
                    .Throws(serviceException);

            // when
            Action retrieveSampleByIdAction = () => this.sampleService.RetrieveSampleById(someSampleId);

            // then
            Assert.Throws<SampleServiceException>(retrieveSampleByIdAction);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectSampleById(It.IsAny<int>()),
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
