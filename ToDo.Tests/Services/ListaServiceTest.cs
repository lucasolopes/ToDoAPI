using Domain.Repositories;
using FluentValidation;
using Moq;
using Newtonsoft.Json;
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
            _repositoryManagerMock.Setup(x=> x.ListaRepository().ExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _repositoryManagerMock.Setup(x=> x.ListaRepository().GetByIdAsync(It.IsAny<string>())).ReturnsAsync(ListaFixture.GetLista());
            
            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var result = await _serviceManager.ListaService().GetByIdAsync("1");

            //Assert
            _repositoryManagerMock.Verify(x => x.ListaRepository().ExistsAsync(It.IsAny<string>()), Times.Once());
            _repositoryManagerMock.Verify(x => x.ListaRepository().GetByIdAsync(It.IsAny<string>()), Times.Once());

            var resultSrl = JsonConvert.SerializeObject(result);
            var expectedSrl = JsonConvert.SerializeObject(ListaFixture.GetListaResponse());

            Assert.Equal(expectedSrl,resultSrl);
            
        }


        [Fact]
        public async Task GetByIdAsync_ShoudReturnKeyNotFoundException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.ListaRepository().ExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _serviceManager.ListaService().GetByIdAsync("0"));

            //Assert
            _repositoryManagerMock.Verify(x => x.ListaRepository().ExistsAsync(It.IsAny<string>()), Times.Once());

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

            var resultSrl = JsonConvert.SerializeObject(result);
            var expectedSrl = JsonConvert.SerializeObject(ListaFixture.GetListaResponse());

            Assert.Equal(expectedSrl,resultSrl);
        }

        [Fact]
        public async Task CreateAsync_ShoudReturnValidationException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _serviceManager.ListaService().CreateAsync(new ListaRequest()));

            //Assert
            _repositoryManagerMock.Verify(x => x.ListaRepository().CreateAsync(It.IsAny<Lista>()), Times.Never);

            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
            Assert.Contains("Validation failed:", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ShoudReturnListaResponse()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();

            _repositoryManagerMock.Setup(x=> x.ListaRepository().ExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            _repositoryManagerMock.Setup(x=> x.ListaRepository().UpdateAsync(It.IsAny<string>(),It.IsAny<Lista>())).ReturnsAsync(ListaFixture.PutLista());

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var result = await _serviceManager.ListaService().UpdateAsync("1",ListaFixture.GetListaRequest());

            //Assert
            _repositoryManagerMock.Verify(
                x=> x.ListaRepository().ExistsAsync(It.IsAny<string>()), Times.Once());

            _repositoryManagerMock.Verify(
                x=> x.ListaRepository().UpdateAsync(It.IsAny<string>(),It.IsAny<Lista>()), Times.Once());

            var resultSrl = JsonConvert.SerializeObject(result);
            var expectedSrl = JsonConvert.SerializeObject(ListaFixture.PutListaResponse());
            Assert.Equal(expectedSrl,resultSrl);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnKeyNotFoundException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            _repositoryManagerMock.Setup(x => x.ListaRepository().ExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _serviceManager.ListaService().UpdateAsync("0", ListaFixture.GetListaRequest()));

            //Assert
            _repositoryManagerMock.Verify(x => x.ListaRepository().ExistsAsync(It.IsAny<string>()), Times.Once);

            Assert.Equal("Lista not found", exception.Message);

        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteLista_WhenIdExists()
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
        public async Task DeleteAsync_ShouldReturnKeyNotFoundException()
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

        [Fact]
        public async Task UpdateAsync_ShouldReturnValidationException()
        {
            //Arrange
            var _repositoryManagerMock = new Mock<IRepositoryManager>();
            var _serviceManager = new ServiceManager(_repositoryManagerMock.Object);

            //Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _serviceManager.ListaService().UpdateAsync("1", new ListaRequest()));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
            Assert.Contains("Validation failed:", exception.Message);
        }


    }
}
