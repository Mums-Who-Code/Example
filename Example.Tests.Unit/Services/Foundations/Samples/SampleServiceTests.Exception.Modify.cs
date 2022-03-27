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
        public void ShouldThrowDependencyValidationExceptionOnModifyIfValidationErrorOccursAndLogIt()
        {
            // given
            Sample someSample = CreateRandomSample();
            var argumentNullException = new ArgumentNullException();

            var nullArgumentSampleException =
                new NullArgumentSampleException(argumentNullException);

            var expectedSampleDependencyValidationException =
                new SampleDependencyValidationException(nullArgumentSampleException);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateSample(It.IsAny<Sample>()))
                    .Throws(argumentNullException);

            // when
            Action modifySampleAction = () => this.sampleService.ModifySample(someSample);

            // then
            Assert.Throws<SampleDependencyValidationException>(modifySampleAction);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateSample(It.IsAny<Sample>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs
                    (expectedSampleDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();

        }
    }
}
