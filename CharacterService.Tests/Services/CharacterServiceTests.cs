using CharacterService.Application.DTOs.Character.Requests;
using CharacterService.Application.DTOs.Character.Responses;
using CharacterService.Application.DTOs.Paginations;
using CharacterService.Application.Interfaces;
using CharacterService.Application.Services;
using CharacterService.Domain.Entities;
using CharacterService.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;

namespace CharacterService.Tests.Services;

public class CharacterServiceTests
{
    #region getbyid
    [Fact]
    public async Task GetByIdAsync_ShouldReturnCharacter_WhenCharacterExists()
    {
        //arrange
        var character = new Character
        {
            Id = Guid.NewGuid(),
            Name = "Zero",
        };

        //mock
        var repositoryMock = new Mock<ICharacterRepository>();
        var loggerMock = new Mock<ILogger<Infrastructure.Services.CharacterService>>();

        repositoryMock.Setup(x => x.GetByIdAsync(character.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(character);

        //act
        var service = new Infrastructure.Services.CharacterService(
            repositoryMock.Object,loggerMock.Object);

        var result = await service.GetByIdAsync(character.Id, CancellationToken.None);

        //assert
        Assert.NotNull(result);

        repositoryMock.Verify(x => x.GetByIdAsync(character.Id, It.IsAny<CancellationToken>()),
           Times.Once);


        Assert.Equal(character.Id, result.Id);
        Assert.Equal(character.Name, result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFoundException_WhenCharacterDoesNotExist()
    {
        //arrange


        //mock
        var repositoryMock = new Mock<ICharacterRepository>();
        var loggerMock = new Mock<ILogger<Infrastructure.Services.CharacterService>>();

        repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Character?)null);

        var service = new Infrastructure.Services.CharacterService(
            repositoryMock.Object,
            loggerMock.Object);

        //act + assert

        await Assert.ThrowsAsync<NotFoundException>(()=>service.GetByIdAsync(Guid.NewGuid(), CancellationToken.None));

        repositoryMock.Verify(
            x => x.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

    }
    #endregion

    #region getall
    [Fact]
    public async Task GetAllAsync_ShouldReturnPaginatedCharacters_WhenCharactersExist()
    {
        // arrange
        var page = new PaginationRequest
        {
            Page = 1,
            PageSize = 10
        };

        var characters = new List<Character>
    {
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Zero"
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Alice"
        }
    };

        var repositoryMock = new Mock<ICharacterRepository>();
        var loggerMock = new Mock<ILogger<Infrastructure.Services.CharacterService>>();

        repositoryMock
            .Setup(x => x.GetAllAsync(
                page.Page,
                page.PageSize,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(characters);

        repositoryMock
            .Setup(x => x.CountAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(characters.Count);

        var service = new Infrastructure.Services.CharacterService(
            repositoryMock.Object,
            loggerMock.Object);

        // act
        var result = await service.GetAllAsync(
            page,
            CancellationToken.None);

        // assert
        Assert.NotNull(result);
        Assert.Equal(2, result.TotalCount);
        Assert.Equal(1, result.Page);
        Assert.Equal(10, result.PageSize);
        Assert.Equal(1, result.TotalPages);
        Assert.Equal(2, result.Items.Count);
        Assert.Contains(result.Items, x => x.Name == "Zero");
        Assert.Contains(result.Items, x => x.Name == "Alice");

        repositoryMock.Verify(
            x => x.GetAllAsync(
                page.Page,
                page.PageSize,
                It.IsAny<CancellationToken>()),
            Times.Once);

        repositoryMock.Verify(
            x => x.CountAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
    [Fact]
    public async Task GetAllByUserIdAsync_ShouldReturnPaginatedCharacters_WhenUserHasCharacters()
    {
        //arrange
        var userId = Guid.NewGuid();
        var page = new PaginationRequest
        {
            Page = 1,
            PageSize = 10
        };

        var characters = new List<Character>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Zero",
                UserId= userId
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Alice",
                UserId= userId
            }
        };

        //mock
        var repositoryMock = new Mock<ICharacterRepository>();
        var loggerMock = new Mock<ILogger<Infrastructure.Services.CharacterService>>();

        repositoryMock.Setup(
            x => x.GetByUserIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(characters);

        repositoryMock
            .Setup(x => x.CountByUserIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(2);

        //act
        var service = new Infrastructure.Services.CharacterService(
            repositoryMock.Object, loggerMock.Object);

        var result = await service.GetAllByUserIdAsync(userId, page, CancellationToken.None);

        Assert.Equal(2, result.TotalCount);
        Assert.Equal(2, result.Items.Count);
        Assert.Contains(result.Items, x => x.UserId == userId);

        repositoryMock.Verify(
            x => x.GetByUserIdAsync(
                userId,
                page.Page,
                page.PageSize,
                It.IsAny<CancellationToken>()),
            Times.Once);

        repositoryMock.Verify(
            x => x.CountByUserIdAsync(
                userId,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
    #endregion

    #region createcharacter
    [Fact]
    public async Task CreateCharacterAsync_ShouldReturnCharacter_WhenCharacterCreate()
    {
        //arrange

        var userId = Guid.NewGuid();
        var newCharacter = new CreateCharacterRequest
        {
            Name = "Test",
            SystemPrompt = "Test",
        };

        var createdCharacter = new Character
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = newCharacter.Name,
            SystemPrompt = newCharacter.SystemPrompt
        };

        //mock
        var repositoryMock = new Mock<ICharacterRepository>();
        var loggerMock = new Mock<ILogger<Infrastructure.Services.CharacterService>>();

        repositoryMock
            .Setup(x => x.AddAsync(
                It.IsAny<Character>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdCharacter);

        //act
        var service = new Infrastructure.Services.CharacterService(
            repositoryMock.Object,loggerMock.Object);

        var result = await service.CreateAsync(userId,newCharacter, CancellationToken.None);

        //assert
        Assert.NotNull(result);
        repositoryMock.Verify(
            x => x.AddAsync(
                It.Is<Character>(c =>
                    c.UserId == userId &&
                    c.Name == newCharacter.Name &&
                    c.SystemPrompt == newCharacter.SystemPrompt),
                It.IsAny<CancellationToken>()),
            Times.Once);
        Assert.Equal(newCharacter.Name, result.Name);
        Assert.Equal(userId, createdCharacter.UserId);
    }
    #endregion

    #region Delete
    [Fact]
    public async Task DeleteAsync_ShouldDeleteCharacter_WhenCharacterExists()
    {
        //arrange
        Guid characterId = Guid.NewGuid();

        //mock
        var repositoryMock = new Mock<ICharacterRepository>();
        var loggerMock = new Mock<ILogger<Infrastructure.Services.CharacterService>>();

        repositoryMock
            .Setup(x => x.DeleteAsync(
                characterId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        //act
        var service = new Infrastructure.Services.CharacterService(
            repositoryMock.Object, loggerMock.Object);

        await service.DeleteAsync(characterId, CancellationToken.None);

        repositoryMock.Verify(
            x => x.DeleteAsync(
                characterId,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFoundException_WhenCharacterDoesNotExist()
    {
        //arrange
        Guid characterId = Guid.NewGuid();

        var repositoryMock = new Mock<ICharacterRepository>();
        var loggerMock = new Mock<ILogger<Infrastructure.Services.CharacterService>>();

        repositoryMock
            .Setup(x => x.DeleteAsync(
                characterId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var service = new Infrastructure.Services.CharacterService(
            repositoryMock.Object,
            loggerMock.Object);

        //act + assert

        await Assert.ThrowsAsync<NotFoundException>(
            () => service.DeleteAsync(
                characterId,
                CancellationToken.None));

        repositoryMock.Verify(
            x => x.DeleteAsync(
                characterId,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task DeleteByUserIdAsync_ShouldReturnDeletedRowsCount_WhenCharactersExist()
    {
        //arrange
        var userId = Guid.NewGuid();

        //mock
        var repositoryMock = new Mock<ICharacterRepository>();
        var loggerMock = new Mock<ILogger<Infrastructure.Services.CharacterService>>();

        repositoryMock.Setup(x => x.DeleteByUserIdAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync(4);

        //act
        var service = new Infrastructure.Services.CharacterService(
            repositoryMock.Object, loggerMock.Object
            );

        var result = await service.DeleteByUserIdAsync(userId, CancellationToken.None);

        //assert

        Assert.Equal(4, result);

        repositoryMock.Verify(x=>x.DeleteByUserIdAsync(It.IsAny<Guid>(),It.IsAny<CancellationToken>()), Times.Once);
    }
    #endregion

    #region Update
    [Fact]
    public async Task UpdateAsync_ShouldUpdateCharacter_WhenCharacterExists()
    {
        //arrange
        Guid characterId = Guid.NewGuid();
        var updateCharacter = new UpdateCharacterRequest
        {
            Name = "name",
            Description = "description",
            SystemPrompt = "system prompt test",
        };

        var character = new Character
        {
            Id = characterId,
            Name = "old"
        };

        //mock
        var repositoryMock = new Mock<ICharacterRepository>();
        var loggerMock = new Mock<ILogger<Infrastructure.Services.CharacterService>>();

        repositoryMock
            .Setup(x => x.GetTrackedByIdAsync(
                characterId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(character);

        //act
        var service = new Infrastructure.Services.CharacterService(repositoryMock.Object, loggerMock.Object);

        var result = await service.UpdateAsync(characterId, updateCharacter, CancellationToken.None);

        //assert
        Assert.Equal(updateCharacter.Name, result.Name);
        Assert.Equal(updateCharacter.Description, result.Description);
        Assert.Equal(updateCharacter.SystemPrompt, character.SystemPrompt);

        repositoryMock.Verify(x => x.GetTrackedByIdAsync(characterId,It.IsAny<CancellationToken>()), Times.Once);
        repositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFoundException_WhenCharacterNotFound()
    {
        //arrange
        var characterId = Guid.NewGuid();

        var request = new UpdateCharacterRequest
        {
            Name = "Test"
        };

        //mock
        var repositoryMock = new Mock<ICharacterRepository>();
        var loggerMock = new Mock<ILogger<Infrastructure.Services.CharacterService>>();

        repositoryMock
            .Setup(x => x.GetTrackedByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Character?)null);

        //act
        var service = new Infrastructure.Services.CharacterService(repositoryMock.Object, loggerMock.Object);

        await Assert.ThrowsAsync<NotFoundException>(()=> service.UpdateAsync(characterId, request,CancellationToken.None));

        //assert

        repositoryMock.Verify(x => x.GetTrackedByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        repositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
    #endregion
}
