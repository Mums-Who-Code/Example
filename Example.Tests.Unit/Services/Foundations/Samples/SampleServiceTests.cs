// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using Example.ConsoleApp.Brokers.Loggings;
using Example.ConsoleApp.Brokers.Storages;
using Example.ConsoleApp.Models.Samples;
using Example.ConsoleApp.Services.Foundations.Samples;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Example.Tests.Unit.Services.Foundations.Samples
{
    public partial class SampleServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ISampleService sampleService;

        public SampleServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.sampleService = new SampleService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.Data);
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static List<Sample> CreateRandomSamplesWithSample(Sample sample)
        {
            List<Sample> samples = CreateSampleFiller()
                .Create(count: GetRandomNumber())
                    .ToList();

            samples.Add(sample);

            return samples;
        }

        private static List<Sample> CreateRandomSamples() =>
            CreateSampleFiller().Create(count: GetRandomNumber()).ToList();

        private static Sample CreateRandomSample() =>
            CreateSampleFiller().Create();

        private static Filler<Sample> CreateSampleFiller() =>
            new Filler<Sample>();
    }
}
