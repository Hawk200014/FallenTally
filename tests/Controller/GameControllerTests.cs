using Xunit;
using Moq;
using FallenTally.Controller;
using FallenTally.Database;
using FallenTally.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace FallenTally.Tests.Controller;
public class GameControllerTests
{
    private Mock<SQLiteDBContext> _mockContext;
    private Mock<DbSet<GameStatsModel>> _mockGameStats;
    private Mock<DbSet<DeathLocationModel>> _mockLocations;
    private Mock<DbSet<DeathModel>> _mockDeaths;
    private List<GameStatsModel> _gameStatsData;
    private List<DeathLocationModel> _locationsData;
    private List<DeathModel> _deathsData;

    public GameControllerTests()
    {
        _gameStatsData = new List<GameStatsModel>();
        _locationsData = new List<DeathLocationModel>();
        _deathsData = new List<DeathModel>();

        _mockGameStats = CreateMockDbSet(_gameStatsData);
        _mockLocations = CreateMockDbSet(_locationsData);
        _mockDeaths = CreateMockDbSet(_deathsData);

        _mockContext = new Mock<SQLiteDBContext>();
        _mockContext.Setup(c => c.GameStats).Returns(_mockGameStats.Object);
        _mockContext.Setup(c => c.Locations).Returns(_mockLocations.Object);
        _mockContext.Setup(c => c.Deaths).Returns(_mockDeaths.Object);
    }

    private static Mock<DbSet<T>> CreateMockDbSet<T>(List<T> data) where T : class
    {
        var queryable = data.AsQueryable();
        var mockSet = new Mock<DbSet<T>>();
        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
        mockSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(data.Add);
        mockSet.Setup(d => d.Remove(It.IsAny<T>())).Callback<T>(t => data.Remove(t));
        return mockSet;
    }

    [Fact]
    public void AddGame_ReturnsFalse_WhenNameOrPrefixIsNullOrEmpty()
    {
        var controller = new GameController(_mockContext.Object);
        Assert.False(controller.AddGame(null, "prefix"));
        Assert.False(controller.AddGame("name", null));
        Assert.False(controller.AddGame("", "prefix"));
        Assert.False(controller.AddGame("name", ""));
    }

    [Fact]
    public void AddGame_ReturnsFalse_WhenDuplicateName()
    {
        _gameStatsData.Add(new GameStatsModel { GameName = "Test", Prefix = "P" });
        var controller = new GameController(_mockContext.Object);
        Assert.False(controller.AddGame("Test", "P2"));
    }

    [Fact]
    public void AddGame_AddsGame_WhenValid()
    {
        var controller = new GameController(_mockContext.Object);
        var result = controller.AddGame("NewGame", "NP");
        Assert.True(result);
        Assert.Single(_gameStatsData);
        Assert.Equal("NewGame", _gameStatsData[0].GameName);
        Assert.Equal("NP", _gameStatsData[0].Prefix);
    }

    [Fact]
    public void EditName_ReturnsFalse_WhenDuplicateName()
    {
        _gameStatsData.Add(new GameStatsModel { GameName = "Test", Prefix = "P" });
        var controller = new GameController(_mockContext.Object);
        var oldGame = new GameStatsModel { GameName = "Test", Prefix = "P" };
        var newGame = new GameStatsModel { GameName = "Test", Prefix = "P2" };
        Assert.False(controller.EditName(oldGame, newGame));
    }

    [Fact]
    public void EditName_ReturnsFalse_WhenOldGameNotFound()
    {
        var controller = new GameController(_mockContext.Object);
        var oldGame = new GameStatsModel { GameName = "NotExist", Prefix = "P" };
        var newGame = new GameStatsModel { GameName = "New", Prefix = "P2" };
        Assert.False(controller.EditName(oldGame, newGame));
    }

    [Fact]
    public void EditName_UpdatesGame_WhenValid()
    {
        _gameStatsData.Add(new GameStatsModel { GameName = "Old", Prefix = "P" });
        var controller = new GameController(_mockContext.Object);
        var oldGame = new GameStatsModel { GameName = "Old", Prefix = "P" };
        var newGame = new GameStatsModel { GameName = "New", Prefix = "NP" };
        var result = controller.EditName(oldGame, newGame);
        Assert.True(result);
        Assert.Equal("New", _gameStatsData[0].GameName);
        Assert.Equal("NP", _gameStatsData[0].Prefix);
    }

    [Fact]
    public void GetGameStats_ReturnsAllGames()
    {
        _gameStatsData.Add(new GameStatsModel { GameName = "A", Prefix = "P" });
        _gameStatsData.Add(new GameStatsModel { GameName = "B", Prefix = "Q" });
        var controller = new GameController(_mockContext.Object);
        var result = controller.GetGameStats();
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetGame_ReturnsGame_WhenExists()
    {
        _gameStatsData.Add(new GameStatsModel { GameName = "A", Prefix = "P" });
        var controller = new GameController(_mockContext.Object);
        var result = controller.GetGame("A");
        Assert.NotNull(result);
        Assert.Equal("A", result.GameName);
    }

    [Fact]
    public void GetGame_ReturnsNull_WhenNotExists()
    {
        var controller = new GameController(_mockContext.Object);
        var result = controller.GetGame("NotExist");
        Assert.Null(result);
    }

    [Fact]
    public void IsDupeName_ReturnsTrue_WhenExists()
    {
        _gameStatsData.Add(new GameStatsModel { GameName = "A", Prefix = "P" });
        var controller = new GameController(_mockContext.Object);
        Assert.True(controller.IsDupeName("A"));
    }

    [Fact]
    public void IsDupeName_ReturnsFalse_WhenNotExists()
    {
        var controller = new GameController(_mockContext.Object);
        Assert.False(controller.IsDupeName("B"));
    }

    [Fact]
    public void GetAllDeaths_ReturnsCorrectCount()
    {
        var game = new GameStatsModel { GameId = 1, GameName = "A", Prefix = "P" };
        _locationsData.Add(new DeathLocationModel { LocationId = 10, GameID = 1, Name = "Loc1", Finish = false });
        _locationsData.Add(new DeathLocationModel { LocationId = 11, GameID = 1, Name = "Loc2", Finish = false });
        _deathsData.Add(new DeathModel { DeathId = 1, LocationId = 10 });
        _deathsData.Add(new DeathModel { DeathId = 2, LocationId = 10 });
        _deathsData.Add(new DeathModel { DeathId = 3, LocationId = 11 });
        var controller = new GameController(_mockContext.Object);
        var result = controller.GetAllDeaths(game);
        Assert.Equal(3, result);
    }

    [Fact]
    public void GetAllGameNames_ReturnsAllNames()
    {
        _gameStatsData.Add(new GameStatsModel { GameName = "A", Prefix = "P" });
        _gameStatsData.Add(new GameStatsModel { GameName = "B", Prefix = "Q" });
        var controller = new GameController(_mockContext.Object);
        var result = controller.GetAllGameNames();
        Assert.Contains("A", result);
        Assert.Contains("B", result);
    }

    [Fact]
    public void RemoveGame_DoesNothing_WhenNull()
    {
        var controller = new GameController(_mockContext.Object);
        controller.RemoveGame(null);
        Assert.Empty(_gameStatsData);
    }

    [Fact]
    public void RemoveGame_DoesNothing_WhenNotFound()
    {
        _gameStatsData.Add(new GameStatsModel { GameName = "A", Prefix = "P" });
        var controller = new GameController(_mockContext.Object);
        controller.RemoveGame(new GameStatsModel { GameName = "B", Prefix = "Q" });
        Assert.Single(_gameStatsData);
    }

    [Fact]
    public void RemoveGame_RemovesGame_WhenFound()
    {
        _gameStatsData.Add(new GameStatsModel { GameName = "A", Prefix = "P" });
        var controller = new GameController(_mockContext.Object);
        controller.RemoveGame(new GameStatsModel { GameName = "A", Prefix = "P" });
        Assert.Empty(_gameStatsData);
    }
}