using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{

    [SerializeField] private SudokuView _boardSudokuView;
    [SerializeField] private InputField _inputField;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartSolving();
        }
    }

    private void StartSolving()
    {
        var sudokuString = _inputField.text;
        sudokuString =  new string(sudokuString.Where(Char.IsDigit).ToArray());
        var size = (int) Mathf.Sqrt(sudokuString.Length);
        
        _boardSudokuView.Cleanup();
        _boardSudokuView.Initialize(size);
        
        var board = new Board(size, sudokuString);
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
}