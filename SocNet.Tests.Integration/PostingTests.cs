using NUnit.Framework;

namespace SocNet.Tests.Integration
{
    [TestFixture]
    public class PostingTests : FeaturesTest
    {
        [SetUp]
        public void SetUp()
        {
            SocApp = SocAppFactory.Create();
        }

        [Test]
        public void AliceCanPostAMessage()
        {
            Assert.DoesNotThrow(() => Post(Alice, "I love the weather today"));
        }

        [Test]
        public void BobCanPostTwoMessages()
        {
            Assert.DoesNotThrow(() => Post(Bob, "Damn! We lost!"));
            Assert.DoesNotThrow(() => Post(Bob, "Good game though."));
        }
    }
}