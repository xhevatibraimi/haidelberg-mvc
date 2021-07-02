using System;
using TestingDemo;
using Xunit;

namespace XUnitDemo
{
    public class AddMethodShould
    {
        [Fact]
        public void Return_3_when_1_and_2_are_added()
        {
            // Arrange
            var a = 1;
            var b = 2;
            // Act
            var result = Program.Add(a, b);
            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void Return_point_3_when_point_1_and_point_2_are_added()
        {
            // Arrange
            var a = 0.1;
            var b = 0.2;
            // Act
            var result = Program.Add(a, b);
            // Assert
            Assert.Equal(0.3, result);
        }
    }
}
