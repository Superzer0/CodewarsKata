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

        private static IMazeSolver CreateSut()
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
        public void SolveMaze_NoSolutionMaze_EmptyListReturned()
        {
            var inputMaze = new[,] { { 1, 1, 1 } };
            var mazeStart = (0, 0);
            var mazeExit = (0, inputMaze.GetLength(1) - 1);

            var sut = CreateSut();
            var maze = sut.CreateMaze(inputMaze);
            maze.SetStart(mazeStart).SetExit(mazeExit);

            var result = sut.SolveMaze(maze);

            result.ExitFound.Should().BeFalse();
            result.Path.ToList().Should().BeEmpty();
        }

        [Test]
        public void SolveMaze_OneElementMaze_ExitElementReturned()
        {
            var inputMaze = new[,] { { 0 } };
            var mazeStart = (0, 0);
            var mazeExit = (0, 0);
            var expected = new List<(int, int)> { mazeExit };

            var sut = CreateSut();
            var maze = sut.CreateMaze(inputMaze);
            maze.SetStart(mazeStart).SetExit(mazeExit);

            var result = sut.SolveMaze(maze);

            result.ExitFound.Should().BeTrue();
            result.Path.ToList().Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
        }

        [Test]
        [TestCaseSource(nameof(SolvableTestCases))]
        public void SolveMaze_SolvableMaze_ProperSolutionReturned(MazeSolverTestCase testCase)
        {
            var inputMaze = testCase.InputMaze;
            var mazeStart = (0, 0);
            var mazeExit = (inputMaze.GetLength(0) - 1, inputMaze.GetLength(1) - 1);
            var expectedResult = testCase.ExpectedResult;

            var sut = CreateSut();
            var maze = sut.CreateMaze(inputMaze);
            maze.SetStart(mazeStart).SetExit(mazeExit);

            var result = sut.SolveMaze(maze);

            result.ExitFound.Should().BeTrue();
            result.Path.ToList().Should().BeEquivalentTo(expectedResult, options => options.WithStrictOrdering());
        }

        [Test]
        [TestCaseSource(nameof(NonSolvableTestCases))]
        public void SolveMaze_NonSolvableMaze_ProperSolutionReturned(MazeSolverTestCase testCase)
        {
            var inputMaze = testCase.InputMaze;
            var mazeStart = (0, 0);
            var mazeExit = (inputMaze.GetLength(0) - 1, inputMaze.GetLength(1) - 1);
            var expectedResult = testCase.ExpectedResult;

            var sut = CreateSut();
            var maze = sut.CreateMaze(inputMaze);
            maze.SetStart(mazeStart).SetExit(mazeExit);

            var result = sut.SolveMaze(maze);

            result.ExitFound.Should().BeFalse();
            result.Path.ToList().Should().BeEquivalentTo(expectedResult, options => options.WithStrictOrdering());
        }

        private static readonly List<MazeSolverTestCase> SolvableTestCases = new List<MazeSolverTestCase>
        {
            new MazeSolverTestCase
            {
                ExpectedResult = new List<(int x, int y)> {(0, 0), (0, 1), (0, 2)},
                InputMaze = new[,] {{0, 0, 0}}
            },
            new MazeSolverTestCase
            {
                ExpectedResult = new List<(int x, int y)>
                    {(0, 0), (0, 1), (0, 2), (0, 3), (0, 4), (1, 4), (2, 4), (3, 4), (3, 5)},
                InputMaze = new[,]
                {
                    {0, 0, 0, 0, 0, 1},
                    {1, 1, 0, 1, 0, 1},
                    {0, 1, 0, 1, 0, 1},
                    {0, 1, 1, 1, 0, 0}
                }
            },
            new MazeSolverTestCase
            {
                ExpectedResult = new List<(int x, int y)>
                {
                    (0, 0), (0, 1), (0, 2), (0, 3), (0, 4),
                    (1, 4), (1,5), (2, 5),
                    (3, 5), (3, 4), (3, 3), (2, 3), (2, 2), (2, 1), (2, 0),
                    (3, 0), (4, 0), (5, 0), (5, 1), (5, 2), (5, 3), (5, 4), (5, 5)
                },
                InputMaze = new[,]
                {
                    {0, 0, 0, 0, 0, 1},
                    {1, 1, 1, 1, 0, 0},
                    {0, 0, 0, 0, 1, 0},
                    {0, 1, 1, 0, 0, 0},
                    {0, 1, 1, 1, 1, 1},
                    {0, 0, 0, 0, 0, 0}
                }
            }
        };

        private static readonly List<MazeSolverTestCase> NonSolvableTestCases = new List<MazeSolverTestCase>
        {
            new MazeSolverTestCase
            {
                ExpectedResult = new List<(int x, int y)>(),
                InputMaze = new[,] {{0, 1, 0}}
            },
            new MazeSolverTestCase
            {
                ExpectedResult = new List<(int x, int y)>(),
                InputMaze = new[,]
                {
                    {0, 0, 0, 0, 0, 1},
                    {1, 1, 0, 1, 0, 1},
                    {0, 1, 0, 1, 0, 1},
                    {0, 1, 1, 1, 1, 0}
                }
            }
        };
    }

    public class MazeSolverTestCase
    {
        public int[,] InputMaze { get; set; }
        public List<(int x, int y)> ExpectedResult { get; set; }
    }
}
