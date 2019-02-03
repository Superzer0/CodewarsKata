using System;
using System.Collections.Generic;

namespace KataProject.TDD.Maze
{
    internal interface IMazeSolver
    {
        IMaze CreateMaze(int[,] inputTable);
        MazeSolvingDfs.MazeSolution SolveMaze(IMaze maze);
    }

    public class MazeSolvingDfs : IMazeSolver
    {
        public IMaze CreateMaze(int[,] inputTable)
        {
            return new Maze(inputTable);
        }

        public MazeSolution SolveMaze(IMaze maze)
        {
            if (maze == null)
                throw new ArgumentNullException(nameof(maze));

            IMazeTraversingAdapter mazeTraversing = new MazeTraversingAdapter(maze);
            var solution = new List<(int x, int y)>();

            var hasSolution = TraverseMaze(mazeTraversing, solution);
            var result = new MazeSolution
            {
                ExitFound = hasSolution,
                Path = hasSolution ? solution : new List<(int x, int y)>()
            };

            return result;
        }

        private bool TraverseMaze(IMazeTraversingAdapter maze, List<(int x, int y)> solution)
        {
            if (maze.CurrentPosition == maze.Exit)
                return true;

            if (maze.CanMoveRight())
            {
                maze.MoveRight();
                var isGoodDirection = TraverseMaze(maze, solution);
                maze.MoveLeft();  
                if (isGoodDirection)
                {
                    solution.Add(maze.CurrentPosition);
                }
            }

            if (maze.CanMoveLeft())
            {
                maze.MoveLeft();
                var isGoodDirection = TraverseMaze(maze, solution);
                maze.MoveRight();
                if (isGoodDirection)
                {
                    solution.Add(maze.CurrentPosition);
                }
            }
           
            if (maze.CanMoveDown())
            {
                maze.MoveDown();
                var isGoodDirection = TraverseMaze(maze, solution);
                maze.MoveUp();
                if (isGoodDirection)
                {
                    solution.Add(maze.CurrentPosition);
                }
            }

            if (maze.CanMoveUp())
            {
                maze.MoveUp();
                var isGoodDirection = TraverseMaze(maze, solution);
                maze.MoveDown();
                if (isGoodDirection)
                {
                    solution.Add(maze.CurrentPosition);
                }
            }

            return false;
        }

        public class MazeSolution
        {
            public bool ExitFound { get; set; }
            public IEnumerable<(int x, int y)> Path { get; set; }
        }
    }
}
