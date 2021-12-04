// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using Example.ConsoleApp.Models.Samples;
using FluentAssertions;
using Moq;
using Xunit;

namespace Example.Tests.Unit.Services.Foundations.Samples
{
    public partial class SampleServiceTests
    {
        [Fact]
        public void ShouldAddSample()
        {
            // given
            Sample randomSample = CreateRandomSample();
            Sample inputSample = randomSample;
            Sample persistedSample = inputSample;
            Sample expectedSample = persistedSample;

            this.storageBrokerMock.Setup(broker =>
                broker.InsertSample(inputSample))
                    .Returns(persistedSample);

            // when
            Sample actualSample = this.sampleService.AddSample(inputSample);

            // then
            actualSample.Should().BeEquivalentTo(expectedSample);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSample(inputSample),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
