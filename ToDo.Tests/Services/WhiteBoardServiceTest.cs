using Castle.Core.Logging;
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
    public class WhiteBoardServiceTest
    {
        [Fact]
        public async Task CreateAsync_ShouldReturnWhiteBoardResponse()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.WhiteBoardRepository().CreateAsync(It.IsAny<WhiteBoard>())).ReturnsAsync(WhiteBoardFixture.GetWhiteBoard());

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);
            
            //Act 
            var result = await _serviceManager.WhiteBoardService()
                .CreateAsync(WhiteBoardFixture.GetWhiteBoardRequest());
;
            //Assert
            _repositoryManagerMock.Verify(
                x => x.WhiteBoardRepository().CreateAsync(It.IsAny<WhiteBoard>()), 
                Times.Once);

            var resultSrl = JsonConvert.SerializeObject(result);
            var expectedSrl = JsonConvert.SerializeObject(WhiteBoardFixture.GetWhiteBoardResponse());

            Assert.Equal(expectedSrl,resultSrl);
            
        }

        [Fact]
        public async Task CreateAsync_ShoudReturnValidationException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act 
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _serviceManager.WhiteBoardService()
                .CreateAsync(new WhiteBoardRequest()));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
            Assert.Contains("Validation failed:",exception.Message);
        }

        [Fact]
        public async Task GetById_ShouldReturnWhiteBoardResponse()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.WhiteBoardRepository().ExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _repositoryManagerMock.Setup(x => x.WhiteBoardRepository().GetByIdAsync(It.IsAny<string>())).ReturnsAsync(WhiteBoardFixture.GetWhiteBoard());

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act 
            var result = await _serviceManager.WhiteBoardService().GetByIdAsync("1");

            //Assert
            _repositoryManagerMock.Verify(x=> x.WhiteBoardRepository().ExistsAsync(It.IsAny<string>()),Times.Once);

            _repositoryManagerMock.Verify(
                x => x.WhiteBoardRepository().GetByIdAsync(It.IsAny<string>()), 
                Times.Once);

            var resultSrl = JsonConvert.SerializeObject(result);
            var expectedSrl = JsonConvert.SerializeObject(WhiteBoardFixture.GetWhiteBoardResponse());
            Assert.Equal(expectedSrl, resultSrl);
        }

        [Fact]
        public async Task GetById_ShouldReturnKeyNotFoundException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.WhiteBoardRepository().ExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act 
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _serviceManager.WhiteBoardService().GetByIdAsync("0"));

            //Assert
            _repositoryManagerMock.Verify(
                x => x.WhiteBoardRepository().ExistsAsync(It.IsAny<string>()), 
                Times.Once);

            Assert.IsType<KeyNotFoundException>(exception);
            Assert.Equal("WhiteBoard not found", exception.Message);
        }

        [Fact]
        public async Task Update_ShouldReturnWhiteBoardResponse()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();

            _repositoryManagerMock.Setup(x => x.WhiteBoardRepository().ExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            _repositoryManagerMock.Setup(x => x.WhiteBoardRepository().UpdateAsync(It.IsAny<string>(),It.IsAny<WhiteBoard>())).ReturnsAsync(WhiteBoardFixture.PutWhiteBoard());

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act 
            var result = await _serviceManager.WhiteBoardService()
                .UpdateAsync("1",WhiteBoardFixture.GetWhiteBoardRequest());

            //Assert
            _repositoryManagerMock.Verify(
                x => x.WhiteBoardRepository().ExistsAsync(It.IsAny<string>()),
                Times.Once);


            _repositoryManagerMock.Verify(
                x => x.WhiteBoardRepository().UpdateAsync(It.IsAny<string>(),It.IsAny<WhiteBoard>()), 
                Times.Once);

            var resultSrl = JsonConvert.SerializeObject(result);
            var expectedSrl = JsonConvert.SerializeObject(WhiteBoardFixture.PutWhiteBoardResponse());
            Assert.Equal(expectedSrl, resultSrl);
        }

        [Fact]
        public async Task Update_ShouldReturnKeyNotFoundException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();

            _repositoryManagerMock.Setup(x => x.WhiteBoardRepository().ExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act 
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _serviceManager.WhiteBoardService()
                .UpdateAsync("0",WhiteBoardFixture.GetWhiteBoardRequest()));

            //Assert
            _repositoryManagerMock.Verify(
                x => x.WhiteBoardRepository().ExistsAsync(It.IsAny<string>()),
                Times.Once);

            Assert.Equal("WhiteBoard not found", exception.Message);
        }

        [Fact]
        public async Task Delete_ShouldDeleteWhiteBoard_WhenIdExists()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.WhiteBoardRepository().ExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            _repositoryManagerMock.Setup(x => x.WhiteBoardRepository().DeleteAsync(It.IsAny<string>()))
            .Returns(Task.CompletedTask);

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act 
            await _serviceManager.WhiteBoardService().DeleteAsync("1");

            //Assert
            _repositoryManagerMock.Verify(
                x => x.WhiteBoardRepository().ExistsAsync(It.IsAny<string>()),
                Times.Once);

            _repositoryManagerMock.Verify(
                x => x.WhiteBoardRepository().DeleteAsync(It.IsAny<string>()),
                Times.Once);

        }

        [Fact]
        public async Task Delete_ShouldReturnKeyNotFoundException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.WhiteBoardRepository().ExistsAsync(It.IsAny<string>()))
            .ReturnsAsync(false);

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act 
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _serviceManager.WhiteBoardService().DeleteAsync("0"));

            //Assert
            _repositoryManagerMock.Verify(
            x => x.WhiteBoardRepository().ExistsAsync(It.IsAny<string>()),
            Times.Once);

            Assert.Equal("WhiteBoard not found", exception.Message);
        }

        [Fact]
        public async Task Update_ShouldReturnValidationException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act 
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _serviceManager.WhiteBoardService()
                .UpdateAsync("1",new WhiteBoardRequest()));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
            Assert.Contains("Validation failed:",exception.Message);
        }

        [Fact]
        public async Task PutNameWhiteBoard_ShouldChangeName() 
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.WhiteBoardRepository().ExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _repositoryManagerMock.Setup(x => x.WhiteBoardRepository().UpdateNameAsync(It.IsAny<string>(), It.IsAny<WhiteBoard>()));

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            await _serviceManager.WhiteBoardService().UpdateNameAsync("1",WhiteBoardFixture.PutWhiteBoardNameRequest());

            //Assert
            _repositoryManagerMock.Verify(
                x => x.WhiteBoardRepository().ExistsAsync(It.IsAny<string>()),
                Times.Once);
            _repositoryManagerMock.Verify(
                x => x.WhiteBoardRepository().UpdateNameAsync(It.IsAny<string>(), It.IsAny<WhiteBoard>()),
                Times.Once);
        }

        [Fact]
        public async Task PutNameWhiteBoard_ShouldReturnValidationException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _serviceManager.WhiteBoardService().UpdateNameAsync("0",new WhiteBoardPutNameRequest()));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
            Assert.Contains("Validation failed:",exception.Message);
        }

        [Fact]
        public async Task PutNameWhiteBoard_ShouldReturnKeyNotFoundException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.WhiteBoardRepository().ExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                    _serviceManager.WhiteBoardService().UpdateNameAsync("0", WhiteBoardFixture.PutWhiteBoardNameRequest()));

            //Assert
            _repositoryManagerMock.Verify(
                x => x.WhiteBoardRepository().ExistsAsync(It.IsAny<string>()),
                Times.Once);

            Assert.NotNull(exception);
            Assert.IsType<KeyNotFoundException>(exception);
            Assert.Equal("WhiteBoard not found", exception.Message);
        }

        [Fact]
        public async Task PutDescriptionWhiteBoard_ShouldChangeDescription()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.WhiteBoardRepository().ExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _repositoryManagerMock.Setup(x => x.WhiteBoardRepository().UpdateDescriptionAsync(It.IsAny<string>(), It.IsAny<WhiteBoard>()));

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            await _serviceManager.WhiteBoardService().UpdateDescriptionAsync("1", WhiteBoardFixture.PutWhiteBoardDescriptionRequest());
            
            //Assert
            _repositoryManagerMock.Verify(
                x => x.WhiteBoardRepository().ExistsAsync(It.IsAny<string>()),
                Times.Once);
            _repositoryManagerMock.Verify(x=> x.WhiteBoardRepository()
                .UpdateDescriptionAsync(It.IsAny<string>(), It.IsAny<WhiteBoard>()),Times.Once);
        }

        [Fact]
        public async Task PutDescriptionWhiteBoard_SouldValidationException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _serviceManager.WhiteBoardService().UpdateDescriptionAsync("0", new WhiteBoardPutDescriptionRequest()));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
            Assert.Contains("Validation failed:", exception.Message);
        }

        [Fact]
        public async Task PutDescriptionWhiteBoard_ShouldKeyNotFoundException() 
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.WhiteBoardRepository().ExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _serviceManager.WhiteBoardService().UpdateDescriptionAsync("0", WhiteBoardFixture.PutWhiteBoardDescriptionRequest()));

            //Assert
            _repositoryManagerMock.Verify(
                x => x.WhiteBoardRepository().ExistsAsync(It.IsAny<string>()),
                Times.Once);

            Assert.NotNull(exception);
            Assert.IsType<KeyNotFoundException>(exception);
            Assert.Equal("WhiteBoard not found", exception.Message);
        }
    }
}
