// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using Example.ConsoleApp.Models.Samples;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace Example.Tests.Unit.Services.Foundations.Samples
{
    public partial class SampleServiceTests
    {
        [Fact]
        public void ShouldRemoveSample()
        {
            // given
            Sample randomSample = CreateRandomSample();
            Sample inputSample = randomSample;
            Sample deletedSample = inputSample;
            Sample expectedSample = deletedSample.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteSample(inputSample))
                    .Returns(deletedSample);

            // when
            Sample actualSample = this.sampleService.RemoveSample(inputSample);

            // then
            actualSample.Should().BeEquivalentTo(expectedSample);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteSample(inputSample),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
