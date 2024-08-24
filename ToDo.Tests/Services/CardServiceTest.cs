using Domain.Repositories;
using FluentValidation;
using Moq;
using OnKanBan.Domain.Entities;
using Services;
using Shared.Requests;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Tests.Fixture;

namespace ToDo.Tests.Services
{
    public class CardServiceTest
    {
        [Fact]
        public async Task CreateAsync_ShouldReturnCardResponse() 
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.CardRepository().CreateAsync(It.IsAny<Card>())).ReturnsAsync(CardFixture.GetCard());

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var result = await _serviceManager.CardService()
                .CreateAsync(CardFixture.GetCardRequest());

            //Assert
            _repositoryManagerMock.Verify(
                x => x.CardRepository().CreateAsync(It.IsAny<Card>()), 
                Times.Once);

            Assert.NotNull(result);
            Assert.IsType<CardResponse>(result);
            Assert.Equal("1", result.Id);
            Assert.Equal("Test Card", result.Title);
            Assert.Equal("Test Description", result.Description);
            Assert.Equal(new DateTime(2024, 1, 1), result.CreatedAt);
            Assert.Equal(new DateTime(2024, 1, 1), result.LastUpdatedAt);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnValidationException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _serviceManager.CardService()
                .CreateAsync(new CardRequest()));

            //Assert
            _repositoryManagerMock.Verify(
                x => x.CardRepository().CreateAsync(It.IsAny<Card>()), 
                Times.Never);

            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
            Assert.Contains("Validation failed:", exception.Message);
        }

        [Fact]
        public async Task GetById_ShouldReturnCardResponse()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.CardRepository().ExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _repositoryManagerMock.Setup(x => x.CardRepository().GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(CardFixture.GetCard());

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var result = await _serviceManager.CardService().GetByIdAsync("1");

            //Assert
            _repositoryManagerMock.Verify(x => x.CardRepository().ExistsAsync(It.IsAny<string>()), Times.Once);
            
            _repositoryManagerMock.Verify(
                x => x.CardRepository().GetByIdAsync(It.IsAny<string>()), 
                Times.Once);
            

            Assert.NotNull(result);
            Assert.IsType<CardResponse>(result);
            Assert.Equal("1", result.Id);
            Assert.Equal("Test Card", result.Title);
            Assert.Equal("Test Description", result.Description);
            Assert.Equal(new DateTime(2024, 1, 1), result.CreatedAt);
            Assert.Equal(new DateTime(2024, 1, 1), result.LastUpdatedAt);
        }

        [Fact]
        public async Task GetById_ShouldReturnKeyNotFoundException() 
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.CardRepository().ExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _serviceManager.CardService()
                .GetByIdAsync("0"));

            //Assert
            _repositoryManagerMock.Verify(
               x => x.CardRepository().ExistsAsync(It.IsAny<string>()),
               Times.Once);

            Assert.IsType<KeyNotFoundException>(exception);
            Assert.Equal("Card not found", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnCardResponse() 
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();

            _repositoryManagerMock.Setup(x=> x.CardRepository().ExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            _repositoryManagerMock.Setup(x => x.CardRepository().UpdateAsync(It.IsAny<string>(), It.IsAny<Card>()))
                .ReturnsAsync(CardFixture.PutCard());

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var result = await _serviceManager.CardService()
                .UpdateAsync("1", CardFixture.GetCardRequest());

            //Assert
            _repositoryManagerMock.Verify(x => x.CardRepository().ExistsAsync(It.IsAny<string>()), Times.Once);
            _repositoryManagerMock.Verify(
                x => x.CardRepository().UpdateAsync(It.IsAny<string>(), It.IsAny<Card>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<CardResponse>(result);
            Assert.Equal("1", result.Id);
            Assert.Equal("Test Card Update", result.Title);
            Assert.Equal("Test Description Update", result.Description);
            Assert.Equal(new DateTime(2024, 2, 2), result.CreatedAt);
            Assert.Equal(new DateTime(2024, 2, 2), result.LastUpdatedAt);
            Assert.Equal(2,result.Position);
            Assert.Equal(StatusEnum.Done.ToString(), result.Status.ToString());
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnKeyNotFoundException() 
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();

            _repositoryManagerMock.Setup(x=> x.CardRepository().ExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _serviceManager.CardService()
                .UpdateAsync("0", CardFixture.GetCardRequest()));

            //Assert
            _repositoryManagerMock.Verify(x => x.CardRepository().ExistsAsync(It.IsAny<string>()), Times.Once);

            Assert.IsType<KeyNotFoundException>(exception);
            Assert.Equal("Card not found", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnValidationException() 
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _serviceManager.CardService()
                .UpdateAsync("1", new CardRequest()));

            //Assert
            _repositoryManagerMock.Verify(x => x.CardRepository().ExistsAsync(It.IsAny<string>()), Times.Never);

            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
            Assert.Contains("Validation failed:", exception.Message);

        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteCard_WhenIdExists()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.CardRepository().ExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            _repositoryManagerMock.Setup(x => x.CardRepository().DeleteAsync(It.IsAny<string>()));

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            await _serviceManager.CardService().DeleteAsync("1");

            //Assert
            _repositoryManagerMock.Verify(
                x => x.CardRepository().ExistsAsync(It.IsAny<string>()), 
                Times.Once);

            _repositoryManagerMock.Verify(
                x => x.CardRepository().DeleteAsync(It.IsAny<string>()), Times.Once);          
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnKeyNotFoundException() 
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.CardRepository().ExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _serviceManager.CardService()
                .DeleteAsync("0"));

            //Assert
            _repositoryManagerMock.Verify(
                x => x.CardRepository().ExistsAsync(It.IsAny<string>()), 
                Times.Once);

            Assert.IsType<KeyNotFoundException>(exception);
            Assert.True(exception.Message == "Card not found");
        }

    }
}
