// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using Example.ConsoleApp.Models.Samples;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Example.Tests.Unit.Services.Foundations.Samples
{
    public partial class SampleServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllSamples()
        {
            // given
            List<Sample> randomSamples = CreateRandomSamples();
            List<Sample> storedSamples = randomSamples;
            List<Sample> expectedSamples = storedSamples.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllSamples())
                    .Returns(storedSamples);

            // when
            List<Sample> actualSamples = this.sampleService.RetrieveAllSamples();

            // then
            actualSamples.Should().BeEquivalentTo(expectedSamples);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllSamples(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
