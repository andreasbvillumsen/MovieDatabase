using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using MovieDatabase.Controllers;
using MovieDatabase.Core.ApplicationLogic.implementation;
using MovieDatabase.Core.DomainLogic;
using MovieDatabase.Data;
using MovieDatabase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace XUnitMoviesTest
{
    public class MovieTest
    {
        private List<Movie> _movies = null;
        private readonly Mock<IMovieRepository> _MovieRepoMock;

        public MovieTest()
        {
            _MovieRepoMock = new Mock<IMovieRepository>();
            _MovieRepoMock.Setup(repo => repo.Read()).Returns(() => _movies);
        }
        [Fact]
        public void CreateTaskWithValidInputTest()
        {
            //Arrange
            var movie = new Movie()
            {
                Title = "Some Description",
                Price = 2,
                Genre = "something",
                ReleaseDate = DateTime.Now
            };

            var service = new MovieService(_MovieRepoMock.Object);

            _movies = new List<Movie>();

            //Act
            var newTask = service.Create(movie);
            _movies.Add(movie);

            //Assert
            _MovieRepoMock.Setup(repo => repo.Create(movie)).Returns(newTask);
            _MovieRepoMock.Verify(repo => repo.Create(movie), Times.Once);
            _movies.Should().Contain(movie);
        }


        [Fact]
        public void DeleteTaskTest()
        {
            // ARRANGE
            var movie = new Movie()
            {
                Id = 1,
                Title = "Some Description",
                Price = 2,
                Genre = "something",
                ReleaseDate = DateTime.Now
            };

            var service = new MovieService(_MovieRepoMock.Object);

            // check if existing
            _MovieRepoMock.Setup(repo => repo.Read(It.Is<int>(t => t == movie.Id))).Returns(() => movie);

            // ACT
            var deletedTask = service.Delete(movie.Id);

            // ASSERT
            _MovieRepoMock.Verify(repo => repo.Delete(movie.Id), Times.Once);
            deletedTask.Should().BeNull();
        }

        [Fact]
        public void UpdateTaskWithValidInputTest()
        {
            // ARRANGE
            var movie = new Movie()
            {
                Id = 1,
                Title = "Some Description",
                Price = 2,
                Genre = "something",
                ReleaseDate = DateTime.Now
            };

            var service = new MovieService(_MovieRepoMock.Object);

            _MovieRepoMock.Setup(repo => repo.Read(It.Is<int>(z => z == movie.Id))).Returns(() => movie);

            // ACT
            var updatedTask = service.Update(movie.Id, movie);

            // ASSERT
            _MovieRepoMock.Verify(repo => repo.Update(movie.Id, It.Is<Movie>(t => t == movie)), Times.Once);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllTasksTest(int listCount)
        {
            // ARRANGE
            var _movies = new List<Movie>()
            {
                new Movie()
                {
                Id = 1,
                Title = "Some Description",
                Price = 2,
                Genre = "something",
                ReleaseDate = DateTime.Now
                },
                new Movie()
                {
                Id = 2,
                Title = "Some Description",
                Price = 2,
                Genre = "something",
                ReleaseDate = DateTime.Now
                }
        };

            var service = new MovieService(_MovieRepoMock.Object);

            _MovieRepoMock.Setup(repo => repo.Read()).Returns(() => _movies.GetRange(0, listCount));

            // ACT
            var tasksFound = service.Read();

            // ASSERT
            Assert.Equal(_movies.GetRange(0, listCount), tasksFound);
            _MovieRepoMock.Verify(repo => repo.Read(), Times.Once);
        }
    }
}
