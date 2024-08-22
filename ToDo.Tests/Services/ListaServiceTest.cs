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
    public class ListaServiceTest
    {
        [Fact]
        public async Task GetByIdAsync_ShoudReturnListaRequest()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x=> x.ListaRepository().GetByIdAsync(It.IsAny<string>())).ReturnsAsync(ListaFixture.GetLista());
            
            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var result = await _serviceManager.ListaService().GetByIdAsync("1");

            //Assert
            _repositoryManagerMock.Verify(x => x.ListaRepository().GetByIdAsync(It.IsAny<string>()), Times.Once());

            Assert.NotNull(result);
            Assert.IsType<ListaResponse>(result);
            Assert.Equal("1", result.Id);
            Assert.Equal("Test Lista", result.Name);
            Assert.Equal(1, result.Position);
            Assert.Equal("1", result.WhiteBoardId);
        }


        [Fact]
        public async Task GetByIdAsync_ShoudReturnKeyNotFoundException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.ListaRepository().GetByIdAsync(It.IsAny<string>())).ReturnsAsync((Lista)null);

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _serviceManager.ListaService().GetByIdAsync("0"));

            //Assert
            _repositoryManagerMock.Verify(x => x.ListaRepository().GetByIdAsync(It.IsAny<string>()), Times.Once());

            Assert.NotNull(exception);
            Assert.IsType<KeyNotFoundException>(exception);
            Assert.Equal("Lista not found", exception.Message);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnListaReponse()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();

            _repositoryManagerMock.Setup(x=> x.ListaRepository().CreateAsync(It.IsAny<Lista>())).ReturnsAsync(ListaFixture.GetLista());

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var result = await _serviceManager.ListaService().CreateAsync(ListaFixture.GetListaRequest());
            
            //Assert
            _repositoryManagerMock.Verify(x=> x.ListaRepository().CreateAsync(It.IsAny<Lista>()), Times.Once());

            Assert.NotNull(result);
            Assert.IsType<ListaResponse>(result);
            Assert.Equal("1", result.Id);
            Assert.Equal("Test Lista", result.Name);
            Assert.Equal(1, result.Position);
            Assert.Equal("1", result.WhiteBoardId);
        }

        [Fact]
        public async Task CreateAsync_ShoudReturnValidationException()
        {
            var _repositoryManagerMock = new Mock<IRepositoryManager>();

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _serviceManager.ListaService().CreateAsync(new ListaRequest()));

            _repositoryManagerMock.Verify(x => x.ListaRepository().CreateAsync(It.IsAny<Lista>()), Times.Never);

            Assert.Equal("Validation failed: \r\n -- Name: Name é Obrigatorio! Severity: Error\r\n -- Position: Position é Obrigatorio! Severity: Error\r\n -- WhiteBoardId: WhiteBoardId é Obrigatorio! Severity: Error", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ShoudReturnListaResponse()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();

            _repositoryManagerMock.Setup(x=> x.ListaRepository().ExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            _repositoryManagerMock.Setup(x=> x.ListaRepository().UpdateAsync(It.IsAny<string>(),It.IsAny<Lista>())).ReturnsAsync(ListaFixture.PutListaRequest());

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var result = await _serviceManager.ListaService().UpdateAsync("1",ListaFixture.GetListaRequest());

            //Assert
            _repositoryManagerMock.Verify(
                x=> x.ListaRepository().ExistsAsync(It.IsAny<string>()), Times.Once());

            _repositoryManagerMock.Verify(
                x=> x.ListaRepository().UpdateAsync(It.IsAny<string>(),It.IsAny<Lista>()), Times.Once());

            Assert.NotNull(result);
            Assert.IsType<ListaResponse>(result);
            Assert.Equal("1", result.Id);
            Assert.Equal("Test Lista Update", result.Name);
            Assert.Equal(2, result.Position);
            Assert.Equal("1", result.WhiteBoardId);
        }

        [Fact]
        public async Task GetById_ShouldReturnKeyNotFoundException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();

            _repositoryManagerMock.Setup(x => x.ListaRepository().GetByIdAsync(It.IsAny<string>())).ReturnsAsync((Lista)null);

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => 
                _serviceManager.ListaService().GetByIdAsync("0"));

            //Assert
            _repositoryManagerMock.Verify(
                x => x.ListaRepository().GetByIdAsync(It.IsAny<string>()), Times.Once());

            Assert.Equal("Lista not found", exception.Message);
        }

        [Fact]
        public async Task Delete_ShouldDeleteLista_WhenIdExists()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.ListaRepository().ExistsAsync(It.IsAny<string>())).ReturnsAsync(true
                );

            _repositoryManagerMock.Setup(x => x.ListaRepository().DeleteAsync(It.IsAny<string>())).Returns(Task.CompletedTask);

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            await _serviceManager.ListaService().DeleteAsync("1");

            //Assert
            _repositoryManagerMock.Verify(x=> x.ListaRepository().ExistsAsync(It.IsAny<string>()),Times.Once());

            _repositoryManagerMock.Verify(x=> x.ListaRepository().DeleteAsync(It.IsAny<string>()),Times.Once());
        }

        [Fact]
        public async Task Delete_ShouldReturnKeyNotFoundException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.ListaRepository().ExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _serviceManager.ListaService().DeleteAsync("0"));

            //Assert
            _repositoryManagerMock.Verify(x => x.ListaRepository().ExistsAsync(It.IsAny<string>()), Times.Once);

            Assert.Equal("Lista not found", exception.Message);
        }



    }
}
