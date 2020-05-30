using CliMed.Api.Utils;
using Xunit;

namespace CliMed.Api.Tests.Utils
{
    public class CryptoTests
    {
        private readonly Crypto sut;

        public CryptoTests()
        {
            sut = new Crypto();
        }

        [Fact]
        public void HashPassword()
        {
            var plainTextPassword = "test1234";

            var hashedPassword = sut.HashPassword(plainTextPassword);

            Assert.IsType<string>(hashedPassword);
            Assert.False(string.IsNullOrEmpty(hashedPassword));
            Assert.False(plainTextPassword.Equals(hashedPassword));
        }

        [Fact]
        public void VerifyMatchingPassword()
        {
            var plainTextPassword = "test1234";

            var hashedPassword = sut.HashPassword(plainTextPassword);

            Assert.True(sut.IsMatchPassword(plainTextPassword, hashedPassword));
        }

        [Fact]
        public void SetStandardHashWorkFactor()
        {
            var sut = new Crypto();

            Assert.Equal(Crypto.StandardHashWorkFactor, sut.HashWorkFactor);
        }

        [Fact]
        public void AllowCustomHashWorkFactor()
        {
            var sut = new Crypto();
            ushort customFactor = 13;

            sut.HashWorkFactor = customFactor;

            Assert.Equal(customFactor, sut.HashWorkFactor);
        }

        [Fact]
        public void IgnoreZeroHashWorkFactor()
        {
            var sut = new Crypto();

            sut.HashWorkFactor = 0;

            Assert.Equal(Crypto.StandardHashWorkFactor, sut.HashWorkFactor);
        }
    }
}
