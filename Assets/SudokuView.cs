using System;
using UnityEngine;

public class SudokuView : MonoBehaviour
{
    [SerializeField] private SudokuEntryView _entryViewPrefab;

    private SudokuEntryView[,] _views;

    [SerializeField] private float _blockSpacer;
    [SerializeField] private float _margin;

    public void Initialize(int sudokuSize)
    {
        _views = new SudokuEntryView[sudokuSize,sudokuSize];
        var sqrtSize = Mathf.Sqrt(sudokuSize);
        
        var smallerSide = CalculateDimensions();

        var width = smallerSide / sudokuSize;
        var height = smallerSide / sudokuSize;

        var viewScale = width / ((RectTransform) _entryViewPrefab.transform).rect.width;
        
        for (int y = 0; y < sudokuSize; y++)
        {
            for (int x = 0; x < sudokuSize; x++)
            {
                var entryViewInstance = Instantiate(_entryViewPrefab,transform);
                entryViewInstance.transform.localScale = new Vector3(viewScale,viewScale,viewScale);

                var xBlockIndex = (int) (x / sqrtSize);
                var xSpacing = xBlockIndex * _blockSpacer;
                
                var yBlockIndex = (int) (y / sqrtSize);
                var ySpacing = yBlockIndex * _blockSpacer;
                
                Vector3 position = new Vector3(x * width + xSpacing + _margin, - y * height - ySpacing - _margin,0);
                entryViewInstance.transform.position = transform.position + position;
                    
                _views[x, y] = entryViewInstance;
            }
        }
    }

    private float CalculateDimensions()
    {
        var rectTransform = ((RectTransform) transform);
        var canvasWidth = rectTransform.rect.width;
        var canvasHeight = rectTransform.rect.height;
        var smallerSide = Math.Min(canvasHeight, canvasWidth);
        return smallerSide - _margin;
    }

    public void Cleanup()
    {
        if(_views == null)
            return;
        
        foreach (var sudokuEntryView in _views)
        {
            Destroy(sudokuEntryView.gameObject);
        }
    }

    public void Visualize(Board board)
    {
        for (int y = 0; y < board.Size; y++)
        {
            for (int x = 0; x < board.Size; x++)
            {
                var boardEntry = board.Entries[x, y];
                _views[x,y].SetNumber(boardEntry);
            }
        }
    }
}