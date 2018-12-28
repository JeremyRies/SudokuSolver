using System.Collections.Generic;
using UnityEngine;

public class SudokuSolver
{
    public void SolveEntry(Board board, Vector2Int position)
    {
        var possibleSolutions = new List<int>();
        for (int i = 1; i <= board.Size; i++)
        {
           
            if (board.IsValidEntry(position.x, position.y, i))
            {
                possibleSolutions.Add(i);
//                board.AddPossibleSolution(position.x, position.y, i);
            }
        }

        if (possibleSolutions.Count == 1)
        {
            board.Set(position.x,position.y, possibleSolutions[0]);
        }
    }
}