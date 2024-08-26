using Domain.Repositories;
using FluentValidation;
using Moq;
using Newtonsoft.Json;
using OnKanBan.Domain.Entities;
using Services;
using Services.Abstractions;
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

            var resultSrl = JsonConvert.SerializeObject(result);
            var expectedSrl = JsonConvert.SerializeObject(CardFixture.GetCardResponse());

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
            

            var resultSrl = JsonConvert.SerializeObject(result);
            var expectedSrl = JsonConvert.SerializeObject(CardFixture.GetCardResponse());

            Assert.Equal(expectedSrl, resultSrl);
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

            var resultSrl = JsonConvert.SerializeObject(result);
            var expectedSrl = JsonConvert.SerializeObject(CardFixture.PutCardResponse());

            Assert.Equal(expectedSrl, resultSrl);

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

        [Fact]
        public async Task UpdateTitleAsync_ShouldChangeTitle()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.CardRepository().ExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);
            _repositoryManagerMock.Setup(x => x.CardRepository().UpdateNameAsync(It.IsAny<string>(), It.IsAny<Card>()));

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            await _serviceManager.CardService().UpdateNameAsync("1", CardFixture.PutCardNameRequest());

            //Assert
            _repositoryManagerMock.Verify(
                x => x.CardRepository().ExistsAsync(It.IsAny<string>()), 
                Times.Once);
            _repositoryManagerMock.Verify(x=> x.CardRepository().UpdateNameAsync(It.IsAny<string>(), It.IsAny<Card>()), 
                Times.Once);
        }

        [Fact]
        public async Task UpdateTitleAsync_ShouldValidationException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _serviceManager.CardService()
                .UpdateNameAsync("1", new CardPutNameRequest()));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
            Assert.Contains("Validation failed:", exception.Message);
        }

        [Fact]
        public async Task UpdateTitleAsync_ShouldKeyNotFoundException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.CardRepository().ExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _serviceManager.CardService()
                .UpdateNameAsync("0", CardFixture.PutCardNameRequest()));

            //Assert
            _repositoryManagerMock.Verify(
                x => x.CardRepository().ExistsAsync(It.IsAny<string>()), 
                Times.Once);

            Assert.NotNull(exception);
            Assert.IsType<KeyNotFoundException>(exception);
            Assert.Equal("Card not found", exception.Message);
        }

        [Fact]
        public async Task UpdateDescriptionAsync_ShouldChangeDescription()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.CardRepository().ExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);
            _repositoryManagerMock.Setup(x => x.CardRepository().UpdateDescriptionAsync(It.IsAny<string>(), It.IsAny<Card>()));

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act 
            await _serviceManager.CardService().UpdateDescriptionAsync("1", CardFixture.PutCardDescriptionRequest());

            //Assert
            _repositoryManagerMock.Verify(
                x => x.CardRepository().ExistsAsync(It.IsAny<string>()), 
                Times.Once);
            _repositoryManagerMock.Verify(x=> x.CardRepository().UpdateDescriptionAsync(It.IsAny<string>(), It.IsAny<Card>()), 
                Times.Once);
        }

        [Fact]
        public async Task UpdateDescriptionAsync_ShouldValidationException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _serviceManager.CardService()
                .UpdateDescriptionAsync("1", new CardPutDescriptionRequest()));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
            Assert.Contains("Validation failed:", exception.Message);
        }

        [Fact]
        public async Task UpdateDescriptionAsync_ShouldKeyNotFoundException() 
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.CardRepository().ExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _serviceManager.CardService()
                .UpdateDescriptionAsync("0", CardFixture.PutCardDescriptionRequest()));

            //Assert
            _repositoryManagerMock.Verify(
                x => x.CardRepository().ExistsAsync(It.IsAny<string>()), 
                Times.Once);

            Assert.NotNull(exception);
            Assert.IsType<KeyNotFoundException>(exception);
            Assert.Equal("Card not found", exception.Message);
        }

        [Fact]
        public async Task UpdatePositionAsync_ShouldChangePosition()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.CardRepository().ExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);
            _repositoryManagerMock.Setup(x => x.CardRepository().UpdatePositionAsync(It.IsAny<string>(), It.IsAny<Card>()));

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            await _serviceManager.CardService().UpdatePositionAsync("1", CardFixture.PutCardPositionRequest());

            //Assert
            _repositoryManagerMock.Verify(
                x => x.CardRepository().ExistsAsync(It.IsAny<string>()), 
                Times.Once);
            _repositoryManagerMock.Verify(x => x.CardRepository().UpdatePositionAsync(It.IsAny<string>(), It.IsAny<Card>()), Times.Once);
        }

        [Fact]
        public async Task UpdatePositionAsync_ShouldValidationException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _serviceManager.CardService()
                .UpdatePositionAsync("1", new CardPutPositionRequest()));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
            Assert.Contains("Validation failed:", exception.Message);
        }

        [Fact]
        public async Task UpdatePositionAsync_ShouldKeyNotFoundException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.CardRepository().ExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _serviceManager.CardService()
                .UpdatePositionAsync("0", CardFixture.PutCardPositionRequest()));

            //Assert
            _repositoryManagerMock.Verify(
                x => x.CardRepository().ExistsAsync(It.IsAny<string>()), 
                Times.Once);

            Assert.NotNull(exception);
            Assert.IsType<KeyNotFoundException>(exception);
            Assert.Equal("Card not found", exception.Message);
        }

        [Fact]
        public async Task UpdateDateInit_ShouldChangeDateInit()
        {
            //Arrange 
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.CardRepository().ExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);
            _repositoryManagerMock.Setup(x => x.CardRepository().GetByIdAsync(It.IsAny<string>())).ReturnsAsync(CardFixture.GetCard());
            _repositoryManagerMock.Setup(x => x.CardRepository().UpdateDateInitAsync(It.IsAny<string>(), It.IsAny<Card>()));

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            await _serviceManager.CardService().UpdateDateInitAsync("1", CardFixture.PutCardDateInitRequest());

            //Assert
            _repositoryManagerMock.Verify(
                x => x.CardRepository().ExistsAsync(It.IsAny<string>()), 
                Times.Once);
            _repositoryManagerMock.Verify(x => x.CardRepository().GetByIdAsync(It.IsAny<string>()), Times.Once);
            _repositoryManagerMock.Verify(x => x.CardRepository().UpdateDateInitAsync(It.IsAny<string>(), It.IsAny<Card>()), Times.Once);
        }

        [Fact]
        public async Task UpdateDateInit_ShouldValidationException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _serviceManager.CardService()
                .UpdateDateInitAsync("1", new CardPutDateInitRequest()));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
            Assert.Contains("Validation failed:", exception.Message);
        }

        [Fact]
        public async Task UpdateDateInit_ShouldKeyNotFoundException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.CardRepository().ExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _serviceManager.CardService()
                .UpdateDateInitAsync("0", CardFixture.PutCardDateInitRequest()));

            //Assert
            _repositoryManagerMock.Verify(
                x => x.CardRepository().ExistsAsync(It.IsAny<string>()),
                Times.Once);

            Assert.NotNull(exception);
            Assert.IsType<KeyNotFoundException>(exception);
            Assert.Equal("Card not found", exception.Message);
        }

        [Fact]
        public async Task UpdateDateInit_ShouldValidationExceptionDateInitAfterComplete()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.CardRepository().ExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);
            _repositoryManagerMock.Setup(x => x.CardRepository().GetByIdAsync(It.IsAny<string>())).ReturnsAsync(CardFixture.GetCard());


            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _serviceManager.CardService()
                .UpdateDateInitAsync("1", CardFixture.PutCardDateInitRequestInvalid()));

            //Assert
            _repositoryManagerMock.Verify(_repositoryManagerMock => _repositoryManagerMock.CardRepository().ExistsAsync(It.IsAny<string>()), Times.Once);
            _repositoryManagerMock.Verify(_repositoryManagerMock => _repositoryManagerMock.CardRepository().GetByIdAsync(It.IsAny<string>()), Times.Once);

            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
            Assert.Equal("DateInit cannot be after Complete", exception.Message);
        }

        [Fact]
        public async Task UpdateDateComplete_ShouldChangeDateComplete()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.CardRepository().ExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);
            _repositoryManagerMock.Setup(x => x.CardRepository().GetByIdAsync(It.IsAny<string>())).ReturnsAsync(CardFixture.GetCard());
            _repositoryManagerMock.Setup(x => x.CardRepository().UpdateDateCompleteAsync(It.IsAny<string>(), It.IsAny<Card>()));

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            await _serviceManager.CardService().UpdateDateCompleteAsync("1", CardFixture.PutCardDateCompleteRequest());

            //Assert
            _repositoryManagerMock.Verify(
                x => x.CardRepository().ExistsAsync(It.IsAny<string>()), 
                Times.Once);
            _repositoryManagerMock.Verify(x => x.CardRepository().GetByIdAsync(It.IsAny<string>()), Times.Once);
            _repositoryManagerMock.Verify(x => x.CardRepository().UpdateDateCompleteAsync(It.IsAny<string>(), It.IsAny<Card>()), Times.Once);
        }

        [Fact]
        public async Task UpdateDateComplete_ShouldValidationException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _serviceManager.CardService()
                .UpdateDateCompleteAsync("1", new CardPutDateCompleteRequest()));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
            Assert.Contains("Validation failed:", exception.Message);
        }

        [Fact]
        public async Task UpdateDateComplete_ShouldKeyNotFoundException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.CardRepository().ExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _serviceManager.CardService()
                .UpdateDateCompleteAsync("0", CardFixture.PutCardDateCompleteRequest()));

            //Assert
            _repositoryManagerMock.Verify(
                x => x.CardRepository().ExistsAsync(It.IsAny<string>()), 
                Times.Once);

            Assert.NotNull(exception);
            Assert.IsType<KeyNotFoundException>(exception);
            Assert.Equal("Card not found", exception.Message);
        }

        [Fact]
        public async Task UpdateDateComplete_ShouldValidationExceptionDateCompleteBeforeInit()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.CardRepository().ExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);
            _repositoryManagerMock.Setup(x => x.CardRepository().GetByIdAsync(It.IsAny<string>())).ReturnsAsync(CardFixture.GetCard());

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _serviceManager.CardService()
                .UpdateDateCompleteAsync("1", CardFixture.PutCardDateCompleteRequestInvalid()));

            //Assert
            _repositoryManagerMock.Verify(_repositoryManagerMock => _repositoryManagerMock.CardRepository().ExistsAsync(It.IsAny<string>()), Times.Once);
            _repositoryManagerMock.Verify(_repositoryManagerMock => _repositoryManagerMock.CardRepository().GetByIdAsync(It.IsAny<string>()), Times.Once);

            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
            Assert.Equal("DateComplete cannot be before Init", exception.Message);
        }

        [Fact]
        public async Task UpdateStatusAsync_ShouldChangeStatus() 
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.CardRepository().ExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            _repositoryManagerMock.Setup(x => x.CardRepository().UpdateStatusAsync(It.IsAny<string>(), It.IsAny<Card>()));

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            await _serviceManager.CardService().UpdateStatusAsync("1", CardFixture.PutCardStatusRequest());

            //Assert
            _repositoryManagerMock.Verify(
                x => x.CardRepository().ExistsAsync(It.IsAny<string>()), 
                Times.Once);
            _repositoryManagerMock.Verify(x => x.CardRepository().UpdateStatusAsync(It.IsAny<string>(), It.IsAny<Card>()), Times.Once);

        }

        [Fact]
        public async Task UpdateStatusAsync_ShouldValidationException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _serviceManager.CardService()
                .UpdateStatusAsync("1", CardFixture.PutCardStatusRequestInvalid()));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
            Assert.Contains("Validation failed:", exception.Message);
        }

        [Fact]
        public async Task UpdateStatusAsync_ShouldKeyNotFoundException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.CardRepository().ExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _serviceManager.CardService()
                .UpdateStatusAsync("0", CardFixture.PutCardStatusRequest()));

            //Assert
            _repositoryManagerMock.Verify(
                x => x.CardRepository().ExistsAsync(It.IsAny<string>()),
                Times.Once);

            Assert.NotNull(exception);
            Assert.IsType<KeyNotFoundException>(exception);
            Assert.Equal("Card not found", exception.Message);
        }

        /* The Squeezing Reordering implementar tbm realizar para Lista
         * [Fact]
         public async Task UpdateListaAsync_ShouldChangeLista()
         {

         }*/

    }
}
