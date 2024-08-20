﻿using Castle.Core.Logging;
using Domain.Repositories;
using Moq;
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
                .CreateAsync(WhiteBoardRequestFixture.GetWhiteBoardRequest());
;
            //Assert
            _repositoryManagerMock.Verify(
                x => x.WhiteBoardRepository().CreateAsync(It.IsAny<WhiteBoard>()), 
                Times.Once);

            Assert.NotNull(result);
            Assert.IsType<WhiteBoardResponse>(result);
            Assert.Equal("1", result.Id);
            Assert.Equal("Test WhiteBoard", result.Name);
            Assert.Equal("Test Description", result.Description);
            Assert.Equal(new DateTime(2024, 1, 1), result.CreatedAt);
            Assert.Equal(new DateTime(2024, 1, 1), result.LastUpdatedAt);
        }

        [Fact]
        public async Task GetById_ShouldReturnWhiteBoardResponse()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.WhiteBoardRepository().GetByIdAsync(It.IsAny<string>())).ReturnsAsync(WhiteBoardFixture.GetWhiteBoard());

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act 
            var result = await _serviceManager.WhiteBoardService().GetByIdAsync("1");

            //Assert
            _repositoryManagerMock.Verify(
                x => x.WhiteBoardRepository().GetByIdAsync(It.IsAny<string>()), 
                Times.Once);

            Assert.NotNull(result);
            Assert.IsType<WhiteBoardResponse>(result);
            Assert.Equal("1", result.Id);
            Assert.Equal("Test WhiteBoard", result.Name);
            Assert.Equal("Test Description", result.Description);
            Assert.Equal(new DateTime(2024, 1, 1), result.CreatedAt);
            Assert.Equal(new DateTime(2024, 1, 1), result.LastUpdatedAt);
        }

        [Fact]
        public async Task GetById_ShouldReturnKeyNotFoundException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.WhiteBoardRepository().GetByIdAsync(It.IsAny<string>())).ReturnsAsync((WhiteBoard)null);

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act 
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _serviceManager.WhiteBoardService().GetByIdAsync("0"));

            //Assert
            Assert.Equal("WhiteBoard not found", exception.Message);

            _repositoryManagerMock.Verify(
                x => x.WhiteBoardRepository().GetByIdAsync(It.IsAny<string>()), 
                Times.Once);

           
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
                .UpdateAsync("1",WhiteBoardRequestFixture.GetWhiteBoardRequest());

            //Assert
            _repositoryManagerMock.Verify(
                x => x.WhiteBoardRepository().ExistsAsync(It.IsAny<string>()),
                Times.Once);


            _repositoryManagerMock.Verify(
                x => x.WhiteBoardRepository().UpdateAsync(It.IsAny<string>(),It.IsAny<WhiteBoard>()), 
                Times.Once);

            Assert.NotNull(result);
            Assert.IsType<WhiteBoardResponse>(result);
            Assert.Equal("1", result.Id);
            Assert.Equal("Test WhiteBoard Updated", result.Name);
            Assert.Equal("Test Description Updated", result.Description);
            Assert.Equal(new DateTime(2024, 2, 2), result.CreatedAt);
            Assert.Equal(new DateTime(2024, 2, 2), result.LastUpdatedAt);
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
                .UpdateAsync("0",WhiteBoardRequestFixture.GetWhiteBoardRequest()));

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

            _repositoryManagerMock.Verify(
                x => x.WhiteBoardRepository().DeleteAsync(It.IsAny<string>()),
                Times.Never);

            Assert.Equal("WhiteBoard not found", exception.Message);
        }


    }
}
