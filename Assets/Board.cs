using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;


public class Board
{
    private int[,] _entries;
    private readonly Dictionary<Vector2Int, HashSet<int>> _possibleSolutions = new Dictionary<Vector2Int, HashSet<int>>();
    
    private int _size;
    private int _boxSize;
    
    private Random _randgen;

    public void AddPossibleSolution(int xpos, int ypos, int value)
    {
        HashSet<int> hashSet = _possibleSolutions[new Vector2Int(xpos, ypos)];
        hashSet.Add(value);
    }

    public int GetPossibleSolutionCount(int xpos, int ypos)
    {
        return _possibleSolutions[new Vector2Int(xpos, ypos)].Count;
    }

    public int[,] Entries
    {
        get { return _entries; }
    }

    public int Size
    {
        get { return _size; }
    }

    private int BoxSize
    {
        get { return _boxSize; }
    }

    private Random RandGen
    {
        get
        {
            if (_randgen == null)
            {
                _randgen = new Random();
            }

            return _randgen;
        }
    }
    
    public Vector2Int? GetRandomEmptyField()
    {
        var emptyFields = new List<Vector2Int>();
        for (int x = 0; x < Size; x++)
        {
            for (var y = 0; y < Size; y++)
            {
                if (IsEmpty(x,y))
                {
                    emptyFields.Add(new Vector2Int(x,y));
                }
            }
        }

        var entryIndex = RandGen.Next(emptyFields.Count);
        if (!emptyFields.Any())
        {
            return null;
        }
        
        return emptyFields[entryIndex];
    }

    public bool IsValidEntry(int x, int y, int number)
    {
        if (IsOccupied(x,y))
        {
            return false;
        }

        List<int> entriesToCheck = new List<int>();
        entriesToCheck.AddRange(GetColumn(x));
        entriesToCheck.AddRange(GetRow(y));
        entriesToCheck.AddRange(GetBox(x,y).Cast<int>());
        
        foreach (var entry in entriesToCheck)
        {
            if (entry == number)
            {
                return false;
            }
        }

        return true;
    }
    
    public bool IsEmpty(int x, int y)
    {
        return !IsOccupied(x, y);
    }
    
    public bool IsOccupied(int x, int y)
    {
        return _entries[x, y] != 0;
    }

    public int[] GetColumn(int x)
    {
        var column = new int[Size];
        for (int y = 0; y < Size; y++)
        {
            column[y] = _entries[x, y];
        }
        return column;
    }

    public int[] GetRow(int y)
    {
        var row = new int[Size];
        for (int x = 0; x < Size; x++)
        {
            row[x] = _entries[x, y];
        }
        return row;
    }
    
    public int[] GetBox(int xPos, int yPos)
    {
        int upperLeftXPos = xPos - (xPos % BoxSize);
        int upperLeftYPos = yPos - (yPos % BoxSize);
        
        int[] boxValues= new int[BoxSize * BoxSize];
//        Debug.Log("Original PosX: " + xPos + " Ypos " + yPos+ " ---------------------------");
//        Debug.Log("upperLeftXPos: " + upperLeftXPos);
//        Debug.Log("upperLeftYPos: " + upperLeftYPos);
        
        for (int x = 0; x < BoxSize; x++)
        {
            for (int y = 0; y < BoxSize; y++)
            {
                var xpos = upperLeftXPos + x;
                var ypos = upperLeftYPos + y;

                var boxValue = _entries[xpos, ypos];
                
//                Debug.Log("Xpos: " + xpos + " ypos: " + ypos + " value " + boxValue);
                boxValues[y * BoxSize + x] = boxValue;
            }
        }
        
        return boxValues;
    }

    public Board(int size, string emptySudoku)
    {
        _size = size;
        _boxSize = (int) Mathf.Sqrt(_size);
        
        if (size * size != emptySudoku.Length)
        {
            Debug.LogError("Sudoku Too long");
        }
        
        _entries = new int[size,size];
        int xValue = 0;
        int yValue = 0;
        foreach (var entry in emptySudoku)
        {
            var number = int.Parse("" + entry);
//            Debug.Log("XValue: " + xValue + " yValue: " + yValue + " Entry: " + number);
            _entries[xValue, yValue] = number;
            
            if (xValue < size-1)
            {
                xValue++;
            }
            else
            {
                yValue++;
                xValue = 0;
            }
            
            _possibleSolutions.Add(new Vector2Int(xValue,yValue), new HashSet<int>());
        }
        
        
    }

    public void Set(int positionX, int positionY, int number)
    {
        _entries[positionX, positionY] = number;
    }
}