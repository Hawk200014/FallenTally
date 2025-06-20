using FallenTally.Database;
using FallenTally.Database.Models;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using FallenTally.Controller.Timers;
using FallenTally.Controller;

namespace FallenTally.Tests.Controller
{
    public class DeathControllerTests
    {
        private DbContextOptions<SQLiteDBContext> CreateInMemoryOptions()
        {
            return new DbContextOptionsBuilder<SQLiteDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public void AddDeath_NullLocationModel_DoesNothing()
        {
            // Arrange
            var options = CreateInMemoryOptions();
            using var context = new SQLiteDBContext(options);
            var controller = new DeathController(context);

            // Act
            controller.AddDeath(null);

            // Assert
            Assert.Empty(context.Deaths);
        }

        [Fact]
        public void AddDeath_ValidLocation_AddsDeathWithCorrectFields()
        {
            // Arrange
            var options = CreateInMemoryOptions();
            using var context = new SQLiteDBContext(options);
            var location = new DeathLocationModel { LocationId = 1, Name = "Test", GameID = 1, Finish = false };
            context.Locations.Add(location);
            context.SaveChanges();

            StreamingController streamingcontroller = new StreamingController(context);
            streamingcontroller.Init(42);

            RecordingController recordingController = new RecordingController(context);
            recordingController.Init(99);


            var controller = new DeathController(context);

            // Act
            controller.AddDeath(location, streamingcontroller, recordingController);

            // Assert
            var death = context.Deaths.FirstOrDefault();
            Assert.NotNull(death);
            Assert.Equal(location.LocationId, death.LocationId);
            Assert.Equal(42, death.StreamTime);
            Assert.Equal(99, death.RecordingTime);
            Assert.True((DateTime.Now - death.TimeStamp).TotalSeconds < 5);
        }

        [Fact]
        public void RemoveDeath_NoDeaths_DoesNothing()
        {
            // Arrange
            var options = CreateInMemoryOptions();
            using var context = new SQLiteDBContext(options);
            var controller = new DeathController(context);

            // Act
            controller.RemoveDeath();

            // Assert
            Assert.Empty(context.Deaths);
        }

        [Fact]
        public void RemoveDeath_RemovesLatestDeath()
        {
            // Arrange
            var options = CreateInMemoryOptions();
            using var context = new SQLiteDBContext(options);
            var death1 = new DeathModel { DeathId = 1, LocationId = 1, StreamTime = 0, RecordingTime = 0, TimeStamp = DateTime.Now.AddMinutes(-1) };
            var death2 = new DeathModel { DeathId = 2, LocationId = 1, StreamTime = 0, RecordingTime = 0, TimeStamp = DateTime.Now };
            context.Deaths.AddRange(death1, death2);
            context.SaveChanges();

            var controller = new DeathController(context);

            // Act
            controller.RemoveDeath();

            // Assert
            Assert.Single(context.Deaths);
            Assert.Equal(1, context.Deaths.First().DeathId);
        }

        [Fact]
        public void GetDeaths_NullLocation_ReturnsZero()
        {
            // Arrange
            var options = CreateInMemoryOptions();
            using var context = new SQLiteDBContext(options);
            var controller = new DeathController(context);

            // Act
            var result = controller.GetDeaths(null);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void GetDeaths_ReturnsCorrectCount()
        {
            // Arrange
            var options = CreateInMemoryOptions();
            using var context = new SQLiteDBContext(options);
            var location1 = new DeathLocationModel { LocationId = 1, Name = "A", GameID = 1, Finish = false };
            var location2 = new DeathLocationModel { LocationId = 2, Name = "B", GameID = 1, Finish = false };
            context.Locations.AddRange(location1, location2);
            context.Deaths.AddRange(
                new DeathModel { LocationId = 1, StreamTime = 0, RecordingTime = 0, TimeStamp = DateTime.Now },
                new DeathModel { LocationId = 1, StreamTime = 0, RecordingTime = 0, TimeStamp = DateTime.Now },
                new DeathModel { LocationId = 2, StreamTime = 0, RecordingTime = 0, TimeStamp = DateTime.Now }
            );
            context.SaveChanges();

            var controller = new DeathController(context);

            // Act
            var count1 = controller.GetDeaths(location1);
            var count2 = controller.GetDeaths(location2);

            // Assert
            Assert.Equal(2, count1);
            Assert.Equal(1, count2);
        }
    }
}