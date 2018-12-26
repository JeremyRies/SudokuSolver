using System.Collections;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private string _emptySudoku;
    [SerializeField] private string _correctSolution;

    [SerializeField] private SudokuView _boardSudokuView;

    private void Start()
    {
        var size = (int) Mathf.Sqrt(_emptySudoku.Length);
        Debug.Log("Sudoku Size: " + size);
        
        _boardSudokuView.Initialize(size);
        
        var board = new Board(size, _emptySudoku);
        _boardSudokuView.Visualize(board);
        
        var solver = new SudokuSolver();

      
        StartCoroutine(Solve(board, solver));
    }

    private IEnumerator Solve(Board board, SudokuSolver solver)
    {
        var isSolved = false;
        while (isSolved == false)
        {
            var field = board.GetRandomEmptyField();

            if (field != null)
            {
                solver.SolveEntry(board, field.Value);
            }
            else
            {
                isSolved = true;
            }

            _boardSudokuView.Visualize(board);
            
            yield return null;
        }
    }

    private void PrintResult(string calculatedSolution)
    {
        if (_correctSolution == calculatedSolution)
        {
            Debug.Log("Correctly calculated");
        }
        else
        {
            Debug.Log("Error in Calculation");
        }
    }
}