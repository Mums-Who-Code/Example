// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using System;
using System.Threading.Tasks;
using Example.ConsoleApp.Models.Samples;
using Example.ConsoleApp.Models.Samples.Exceptions;
using Moq;
using Xunit;

namespace Example.Tests.Unit.Services.Foundations.Samples
{
    public partial class SampleServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfSampleIsNullAndLogIt()
        {
            // given
            Sample nullSample = null;
            var nullSampleException = new NullSampleException();

            var expectedSampleValidationException =
                new SampleValidationException(nullSampleException);

            // when
            Action addSampleAction = () => this.sampleService.AddSample(nullSample);

            // then
            Assert.Throws<SampleValidationException>(addSampleAction);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSampleValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSample(It.IsAny<Sample>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task ShouldThrowValidationExceptionOnAddIfSampleIsInvalidAndLogIt(
            string invalidText)
        {
            // given
            Sample invalidSample = new Sample
            {
                Text = invalidText
            };

            var invalidSampleException = new InvalidSampleException();

            invalidSampleException.AddData(
                key: nameof(Sample.Id),
                values: "Id is required.");

            invalidSampleException.AddData(
                key: nameof(Sample.Text),
                values: "Text is required.");

            var expectedSampleValidationException = new SampleValidationException(invalidSampleException);

            // when
            Action addSampleAction = () => this.sampleService.AddSample(invalidSample);

            // then
            Assert.Throws<SampleValidationException>(addSampleAction);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSampleValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSample(It.IsAny<Sample>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
