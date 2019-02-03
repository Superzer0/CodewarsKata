using System;
using System.Collections.Generic;

namespace KataProject.TDD.Maze
{
    internal interface IMazeSolver
    {
        IMaze CreateMaze(int[,] inputTable);
        IEnumerable<(int x, int y)> SolveMaze(IMaze maze);
    }

    public class MazeSolvingDfs : IMazeSolver
    {
        public IMaze CreateMaze(int[,] inputTable)
        {
            return new Maze(inputTable);
        }

        public IEnumerable<(int x, int y)> SolveMaze(IMaze maze)
        {
            if (maze == null)
                throw new ArgumentNullException(nameof(maze));

            IMazeTraversingAdapter mazeTraversing = new MazeTraversingAdapter(maze);



            return new List<(int x, int y)>();
        }



    }
}
