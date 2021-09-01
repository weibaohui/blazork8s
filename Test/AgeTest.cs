using System;
using Extension;
using Extensions;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class AgeTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public AgeTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void AgeTest_s()
        {
            var start = DateTime.Parse("2021-06-20 20:00:00");
            var end   = start.AddSeconds(15);
            var age   = end.Age(start);
            Assert.Equal("15s",age);
        }
        [Fact]
        public void AgeTest_m()
        {
            var start = DateTime.Parse("2021-06-20 20:00:00");
            var end   = start.AddMinutes(15);
            var age   = end.Age(start);
            Assert.Equal("15m",age);
        }
        [Fact]
        public void AgeTest_h()
        {
            var start = DateTime.Parse("2021-06-20 20:00:00");
            var end   = start.AddHours(15);
            var age   = end.Age(start);
            Assert.Equal("15h",age);
        }
        [Fact]
        public void AgeTest_d()
        {
            var start = DateTime.Parse("2021-06-20 20:00:00");
            var end   = start.AddDays(15);
            var age   = end.Age(start);
            Assert.Equal("15d",age);
        }
        [Fact]
        public void AgeTest_y()
        {
            var start = DateTime.Parse("2021-06-20 20:00:00");
            var end   = start.AddYears(1);
            var age   = end.Age(start);
            Assert.Equal("1y",age);
        }
        [Fact]
        public void AgeTest_ms()
        {
            var start = DateTime.Parse("2021-06-20 20:00:00");
            var end   = start.AddMinutes(15).AddSeconds(6);
            var age   = end.Age(start);
            Assert.Equal("15m6s",age);
        }
        [Fact]
        public void AgeTest_hm()
        {
            var start = DateTime.Parse("2021-06-20 20:00:00");
            var end = start.AddMinutes(15).AddHours(6);
            var age = end.Age(start);
            Assert.Equal("6h15m", age);
        }

        [Fact]
        public void AgeTest_dh()
        {
            var start = DateTime.Parse("2021-06-20 20:00:00");
            var end = start.AddDays(15).AddHours(6);
            var age = end.Age(start);
            Assert.Equal("15d6h", age);
        }
        [Fact]
        public void AgeTest_yd()
        {
            var start = DateTime.Parse("2021-06-1 00:00:00");
            var end = start.AddDays(15).AddYears(1);
            var age = end.Age(start);
            Assert.Equal("1y15d", age);
        }
    }
}
