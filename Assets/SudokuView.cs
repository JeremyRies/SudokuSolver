using UnityEngine;

public class SudokuView : MonoBehaviour
{
    [SerializeField] private SudokuEntryView _entryViewPrefab;

    private SudokuEntryView[,] _views;
    [SerializeField] private float _width;
    [SerializeField] private float _height;

    [SerializeField] private float _blockSpacer;

    public void Initialize(int sudokuSize)
    {
        _views = new SudokuEntryView[sudokuSize,sudokuSize];
        var sqrtSize = Mathf.Sqrt(sudokuSize);
        
        for (int y = 0; y < sudokuSize; y++)
        {
            for (int x = 0; x < sudokuSize; x++)
            {
                var entryViewInstance = Instantiate(_entryViewPrefab,transform);

                var xBlockIndex = (int) (x / sqrtSize);
                var xSpacing = xBlockIndex * _blockSpacer;
                
                var yBlockIndex = (int) (y / sqrtSize);
                var ySpacing = yBlockIndex * _blockSpacer;
                
                Vector3 position = new Vector3(x * _width + xSpacing, - y * _height - ySpacing,0);
                entryViewInstance.transform.position = transform.position + position;
                    
                _views[x, y] = entryViewInstance;
            }
        }
    }

    public void Visualize(Board board)
    {
        for (int y = 0; y < board.Size; y++)
        {
            for (int x = 0; x < board.Size; x++)
            {
                var boardEntry = board.Entries[x, y];
//                Debug.Log(boardEntry);
                _views[x,y].SetNumber(boardEntry);
            }
        }
    }
}