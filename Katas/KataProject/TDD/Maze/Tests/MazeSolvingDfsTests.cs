using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;

namespace KataProject.TDD.Maze.Tests
{
    [TestFixture]
    public class MazeSolvingDfsTests
    {
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        private static MazeSolvingDfs CreateSut()
        {
            return new MazeSolvingDfs();
        }

        [Test]
        public void MazeSolver_IsAssignableToIMazeSolver()
        {
            var sut = CreateSut();
            sut.Should().BeAssignableTo<IMazeSolver>();
        }

        [Test]
        public void CreateMaze_MazeEmpty_EmptyResult()
        {
            var sut = CreateSut();
            var anonymous = _fixture.Create<int[,]>();
            var result = sut.CreateMaze(anonymous);
            result.Should().NotBeNull("this is correct maze");
        }

        [Test]
        public void CreateMaze_MazeNull_ArgumentExceptionThrown()
        {
            var sut = CreateSut();
            sut.Invoking(p => p.CreateMaze(null)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void CreateMaze_CorrectInput_IsAssignableFromIMaze()
        {
            var sut = CreateSut();
            var anonymous = _fixture.Create<int[,]>();
            var result = sut.CreateMaze(anonymous);
            result.Should().BeAssignableTo<IMaze>();
        }

        [Test]
        public void CreateMaze_MultipleInstances_AreNotTheSame()
        {
            var sut = CreateSut();
            var anonymous = _fixture.Create<int[,]>();
            var result1 = sut.CreateMaze(anonymous);
            var result2 = sut.CreateMaze(anonymous);
            result1.Should().NotBeSameAs(result2);
        }

        [Test]
        public void SolveMaze_NullArgument_ExceptionThrown()
        {
            var sut = CreateSut();
            sut.Invoking(p => p.SolveMaze(null)).Should().Throw<ArgumentNullException>();
        }


        [Test]
        public void SolveMaze_OneDimensionalMaze_ProperSolutionReturned()
        {
            var inputMaze = new[,] { { 0, 0, 0 } };
            var mazeStart = (0, 0);
            var mazeExit = (0, inputMaze.Length);
            var expectedResult = new List<(int x, int y)> {(0, 0), (0, 1), (0, 2)};

            var sut = CreateSut();
            var maze = sut.CreateMaze(inputMaze);
            maze.SetStart(mazeStart).SetExit(mazeExit);

            var result = sut.SolveMaze(maze);
            result.ToList().Should().BeSameAs(expectedResult);
        }
    }
}
