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
        public void ShouldRetrieveSampleById()
        {
            // given
            Sample randomSample = CreateRandomSample();
            Sample inputSample = randomSample;
            Sample storageSample = inputSample;
            Sample expectedSample = storageSample.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectSampleById(inputSample.Id))
                    .Returns(storageSample);

            // when
            Sample actualSample =
                this.sampleService.RetrieveSampleById(inputSample.Id);

            // then
            actualSample.Should().BeEquivalentTo(expectedSample);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectSampleById(inputSample.Id),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
