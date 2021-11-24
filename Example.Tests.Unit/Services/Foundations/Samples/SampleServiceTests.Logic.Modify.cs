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
        public void ShouldModifySample()
        {
            // given
            Sample randomSample = CreateRandomSample();
            Sample inputSample = randomSample;
            Sample storedSample = inputSample;
            Sample expectedSample = storedSample.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateSample(inputSample))
                    .Returns(storedSample);

            // when
            Sample actualSample = this.sampleService.ModifySample(inputSample);

            // then
            actualSample.Should().BeEquivalentTo(expectedSample);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateSample(inputSample),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
