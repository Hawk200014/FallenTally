using Xunit;
using Moq;
using DeathCounterHotkey.Database;
using DeathCounterHotkey.Database.Models;
using FallenTally.Controller.Model;
using FallenTally.Utility.ResultSets;
using FallenTally.Utility.Singletons;
using System.Collections.Generic;
using System.Linq;

namespace FallenTallyTest.Controller.Model
{
    public class DeathModelControllerTests
    {
        private readonly Mock<SQLiteDBContext> _mockContext;
        private readonly Mock<Singleton> _mockSingleton;
        private readonly DeathModelController _controller;

        public DeathModelControllerTests()
        {
            _mockContext = new Mock<SQLiteDBContext>();
            _mockSingleton = new Mock<Singleton>();
            _mockSingleton.Setup(s => s.GetValue("DBContext")).Returns(_mockContext.Object);
            _controller = new DeathModelController();
        }

        [Fact]
        public void GetItem_ById_ReturnsSuccess()
        {
            // Arrange
            var death = new DeathModel { DeathId = 1 };
            _mockContext.Setup(c => c.Deaths.Find(1)).Returns(death);

            // Act
            var result = _controller.GetItem(1);

            // Assert
            Assert.Equal(RESULT.SUCCESS, result.GetResult());
            Assert.Equal(death, result.GetData());
        }

        [Fact]
        public void GetItem_ById_ReturnsFailure()
        {
            // Arrange
            _mockContext.Setup(c => c.Deaths.Find(1)).Returns((DeathModel)null);

            // Act
            var result = _controller.GetItem(1);

            // Assert
            Assert.Equal(RESULT.FAILURE, result.GetResult());
            Assert.Null(result.GetData());
        }

        [Fact]
        public void GetItems_ReturnsSuccess()
        {
            // Arrange
            var deaths = new List<DeathModel> { new DeathModel { DeathId = 1 } };
            _mockContext.Setup(c => c.Deaths.ToList()).Returns(deaths);

            // Act
            var result = _controller.GetItems();

            // Assert
            Assert.Equal(RESULT.SUCCESS, result.GetResult());
            Assert.Equal(deaths, result.GetData());
        }

        [Fact]
        public void GetItems_ReturnsFailure()
        {
            // Arrange
            _mockContext.Setup(c => c.Deaths.ToList()).Returns((List<DeathModel>)null);

            // Act
            var result = _controller.GetItems();

            // Assert
            Assert.Equal(RESULT.FAILURE, result.GetResult());
            Assert.Null(result.GetData());
        }

        [Fact]
        public void AddItem_ReturnsSuccess()
        {
            // Arrange
            var death = new DeathModel { DeathId = 1 };

            // Act
            var result = _controller.AddItem(death);

            // Assert
            _mockContext.Verify(c => c.Deaths.Add(death), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
            Assert.Equal(RESULT.SUCCESS, result.GetResult());
            Assert.Equal(death, result.GetData());
        }

        [Fact]
        public void RemoveItem_ById_ReturnsSuccess()
        {
            // Arrange
            var death = new DeathModel { DeathId = 1 };
            _mockContext.Setup(c => c.Deaths.Find(1)).Returns(death);

            // Act
            var result = _controller.RemoveItem(1);

            // Assert
            _mockContext.Verify(c => c.Deaths.Remove(death), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
            Assert.Equal(RESULT.SUCCESS, result.GetResult());
        }

        [Fact]
        public void RemoveItem_ById_ReturnsFailure()
        {
            // Arrange
            _mockContext.Setup(c => c.Deaths.Find(1)).Returns((DeathModel)null);

            // Act
            var result = _controller.RemoveItem(1);

            // Assert
            Assert.Equal(RESULT.FAILURE, result.GetResult());
        }

        [Fact]
        public void UpdateItem_ReturnsSuccess()
        {
            // Arrange
            var death = new DeathModel { DeathId = 1 };
            _mockContext.Setup(c => c.Deaths.Find(1)).Returns(death);

            // Act
            var result = _controller.UpdateItem(death);

            // Assert
            _mockContext.Verify(c => c.Deaths.Update(death), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
            Assert.Equal(RESULT.SUCCESS, result.GetResult());
            Assert.Equal(death, result.GetData());
        }

        [Fact]
        public void UpdateItem_ReturnsFailure()
        {
            // Arrange
            var death = new DeathModel { DeathId = 1 };
            _mockContext.Setup(c => c.Deaths.Find(1)).Returns((DeathModel)null);

            // Act
            var result = _controller.UpdateItem(death);

            // Assert
            Assert.Equal(RESULT.FAILURE, result.GetResult());
        }
    }
}
