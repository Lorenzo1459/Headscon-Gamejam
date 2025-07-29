using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SlidingPuzzle : MonoBehaviour
{
    int puzzlesize = 3;
    int[,] puzzleGrid;
    int[] movableSlots;

    public List<Image> imageList;
    public List<Sprite> spriteList;

    void Start()
    {
        puzzleGrid = new int[puzzlesize, puzzlesize];
        for (int i = 0; i < puzzlesize; i++)
        {
            for (int j = 0; j < puzzlesize; j++)
            {
                puzzleGrid[i, j] = i * puzzlesize + j + 1;
            }
        }
        puzzleGrid[puzzlesize - 1, puzzlesize - 1] = 0;
        RandomizePuzzle();
    }

    public void RandomizePuzzle()
    {
        // Embaralha o puzzle movendo apenas peças adjacentes ao espaço vazio
        for (int i = 0; i < puzzlesize * puzzlesize * 10; i++)
        {
            int emptyRow = 0, emptyCol = 0;
            FindEmptySlot(out emptyRow, out emptyCol);
            List<(int, int)> adjacents = GetAdjacentTiles(emptyRow, emptyCol);
            if (adjacents.Count > 0)
            {
                var (row, col) = adjacents[Random.Range(0, adjacents.Count)];
                MoveTile(row, col);
            }
        }
    }

    public void MoveTile(int row, int col)
    {
        if (row < 0 || row >= puzzlesize || col < 0 || col >= puzzlesize)
        {
            Debug.Log("Invalid tile position: " + row + ", " + col);
            return;
        }
        if (puzzleGrid[row, col] == 0)
            return;

        int emptyRow, emptyCol;
        FindEmptySlot(out emptyRow, out emptyCol);

        // Só permite mover se for adjacente ao espaço vazio
        if (IsAdjacent(row, col, emptyRow, emptyCol))
        {
            puzzleGrid[emptyRow, emptyCol] = puzzleGrid[row, col];
            puzzleGrid[row, col] = 0;
        }
        else
        {
            Debug.Log("Tile at " + row + ", " + col + " is not adjacent to empty slot.");
        }
    }

    bool IsAdjacent(int row1, int col1, int row2, int col2)
    {
        return (Mathf.Abs(row1 - row2) == 1 && col1 == col2) ||
               (Mathf.Abs(col1 - col2) == 1 && row1 == row2);
    }

    void FindEmptySlot(out int emptyRow, out int emptyCol)
    {
        for (int i = 0; i < puzzlesize; i++)
        {
            for (int j = 0; j < puzzlesize; j++)
            {
                if (puzzleGrid[i, j] == 0)
                {
                    emptyRow = i;
                    emptyCol = j;
                    return;
                }
            }
        }
        emptyRow = -1;
        emptyCol = -1;
    }

    List<(int, int)> GetAdjacentTiles(int row, int col)
    {
        List<(int, int)> adjacents = new List<(int, int)>();
        int[,] directions = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
        for (int d = 0; d < 4; d++)
        {
            int newRow = row + directions[d, 0];
            int newCol = col + directions[d, 1];
            if (newRow >= 0 && newRow < puzzlesize && newCol >= 0 && newCol < puzzlesize)
            {
                if (puzzleGrid[newRow, newCol] != 0)
                    adjacents.Add((newRow, newCol));
            }
        }
        return adjacents;
    }

    public void PrintPuzzle()
    {
        for (int i = 0; i < puzzlesize; i++)
        {
            string row = "";
            for (int j = 0; j < puzzlesize; j++)
            {
                row += puzzleGrid[i, j] + " ";
            }
            Debug.Log(row);
        }
    }
}