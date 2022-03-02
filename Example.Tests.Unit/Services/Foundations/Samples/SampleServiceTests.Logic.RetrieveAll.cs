// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using System.Collections.Generic;
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
        public void ShouldRetrieveAllSamples()
        {
            // given
            List<Sample> randomSamples = CreateRandomSamples();
            List<Sample> persistedSamples = randomSamples;
            List<Sample> expectedSamples = persistedSamples.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllSamples())
                    .Returns(persistedSamples);

            // when
            List<Sample> actualSamples =
                this.sampleService.RetrieveAllSamples();

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
