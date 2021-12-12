using FileSharer.Business.Services.Implementations;
using FileSharer.Common.Entities;
using FileSharer.Data.Repositories.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace FileSharer.Tests.BusinessTests.Service
{
    [TestFixture]
    public class FileServiceTests
    {
        [Test]
        public void GetById_WhenRepositroryHasData_ShouldReturnFileItemById()
        {
            // Arrange
            var expected = new FileItem()
            {
                Id = 13,
                Name = "TextFile"
            };

            var repositoryMock = new Mock<IFileItemRepository>();
            repositoryMock.Setup(repos => repos.GetById(13)).Returns(expected);

            var fileItemService = new FileItemService(repositoryMock.Object);

            // Act
            var actual = fileItemService.GetById(13);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAll_WhenRepositoryHasData_ShouldReturnAllFileItems()
        {
            // Arrange
            var expected = new List<FileItem>()
            {
                new FileItem()
                {
                    Id = 10,
                    Name = "TestFile1",
                },
                new FileItem()
                {
                    Id = 11,
                    Name = "TestFile2",
                }
            };

            var repositoryMock = new Mock<IFileItemRepository>();
            repositoryMock.Setup(repos => repos.GetAll()).Returns(expected);
            var fileService = new FileItemService(repositoryMock.Object);

            // Act
            var actual = fileService.GetAll();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
