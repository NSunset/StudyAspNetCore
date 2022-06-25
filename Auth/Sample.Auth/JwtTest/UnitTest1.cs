using NUnit.Framework;
using Sample.Common;
using System.IO;

namespace JwtTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            RSAHelper.GenerateAndSaveKey(Directory.GetCurrentDirectory(), false);

            Assert.Pass();
        }
    }
}