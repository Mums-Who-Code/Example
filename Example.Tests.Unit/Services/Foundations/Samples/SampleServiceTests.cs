// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using Example.ConsoleApp.Brokers.Storages;
using Example.ConsoleApp.Models.Samples;
using Example.ConsoleApp.Services.Foundations.Samples;
using Moq;
using Tynamix.ObjectFiller;

namespace Example.Tests.Unit.Services.Foundations.Samples
{
    public partial class SampleServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly ISampleService sampleService;

        public SampleServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.sampleService = new SampleService(
                storageBroker: this.storageBrokerMock.Object);
        }

        private static Sample CreateRandomSample() =>
            CreateSampleFiller().Create();

        private static Filler<Sample> CreateSampleFiller() =>
            new Filler<Sample>();
    }
}
