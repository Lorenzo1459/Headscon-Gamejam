using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SlidingPuzzle : MonoBehaviour
{
    public SlidingPuzzle instance;
    void awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { 
            Destroy(gameObject);
        }
    }


    // Puzzle Configuration
    public PuzzleObject puzzleObject;
    
    public int puzzleSize = 3;
    int[,] puzzleGrid;

    // Puzzle UI Elements
    public GameObject puzzlePiecePrefab;
    public GameObject parent;
    private List<GameObject> piecesList = new List<GameObject>();
    public List<Sprite> spriteList;


    public void LoadPuzzle()
    {
        piecesList = new List<GameObject>(); 
        while (parent.transform.childCount > 0)
        {
            DestroyImmediate(parent.transform.GetChild(0).gameObject);
        }

        puzzleSize = puzzleObject.puzzleSize;
        spriteList = puzzleObject.spriteList;

        float backgroundWidth = parent.GetComponent<RectTransform>().rect.width;
        float backgroundHeight = parent.GetComponent<RectTransform>().rect.height;

        puzzleGrid = new int[puzzleSize, puzzleSize];
        for (int i = 0, k = 0; i < puzzleSize; i++)
        {
            for (int j = 0; j < puzzleSize; j++)
            {
                puzzleGrid[i, j] = k;

                GameObject piece = Instantiate(puzzlePiecePrefab, parent.transform);
                PuzzlePiece puzzlePieceComponent = piece.GetComponent<PuzzlePiece>(); // Get the component first

                RectTransform pieceRect = piece.GetComponent<RectTransform>();
                pieceRect.sizeDelta = new Vector2(backgroundWidth / puzzleSize, backgroundHeight / puzzleSize);
                pieceRect.localScale = new Vector3(0.95f, 0.95f, 1f);

                if (puzzlePieceComponent != null) // Add a null check for robustness
                {
                    puzzlePieceComponent.Initialize(k, k, spriteList[k], this);
                }
                else
                {
                    Debug.LogError("PuzzlePiece component not found on the instantiated prefab! Make sure your puzzlePiecePrefab has the PuzzlePiece script attached.", piece);
                }

                // CORREÇÃO AQUI: Ajuste na coordenada Y
                piece.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    (j - (puzzleSize - 1) / 2f) * (backgroundWidth / puzzleSize),
                    ((puzzleSize - 1) / 2f - i) * (backgroundHeight / puzzleSize) // Alterado para criar de cima para baixo
                );

                piecesList.Add(piece);
                k++;
            }
        }

        puzzleGrid[puzzleSize - 1, puzzleSize - 1] = -1;
        piecesList[puzzleSize * puzzleSize - 1].GetComponent<PuzzlePiece>().pieceValue = -1;
        piecesList[puzzleSize * puzzleSize - 1].SetActive(false);
    }

    // Inside SlidingPuzzle.cs
    void Start()
    {
        LoadPuzzle();
        //RandomizePuzzle();
    }

    public void RandomizePuzzle()
    {
        // Embaralha o puzzle movendo apenas peças adjacentes ao espaço vazio
        for (int i = 0; i < puzzleSize * puzzleSize * 10; i++)
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
        if (row < 0 || row >= puzzleSize || col < 0 || col >= puzzleSize)
        {
            Debug.Log("Invalid tile position: " + row + ", " + col);
            return;
        }
        if (puzzleGrid[row, col] == -1)
            return;

        int emptyRow, emptyCol;
        FindEmptySlot(out emptyRow, out emptyCol);

        // Só permite mover se for adjacente ao espaço vazio
        if (IsAdjacent(row, col, emptyRow, emptyCol))
        {
            // Troca os valores no grid
            puzzleGrid[emptyRow, emptyCol] = puzzleGrid[row, col];
            puzzleGrid[row, col] = -1;

            // Atualiza as peças na lista
            GameObject movedPiece = piecesList[puzzleGrid[emptyRow, emptyCol]];
            movedPiece.GetComponent<PuzzlePiece>().piecePosition = emptyRow * puzzleSize + emptyCol;

            // CORREÇÃO AQUI: Ajuste na coordenada Y para o movimento da peça
            movedPiece.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                (emptyCol - (puzzleSize - 1) / 2f) * (parent.GetComponent<RectTransform>().rect.width / puzzleSize),
                ((puzzleSize - 1) / 2f - emptyRow) * (parent.GetComponent<RectTransform>().rect.height / puzzleSize) // Alterado para corresponder à nova lógica de cima para baixo
            );
        }
        else
        {
            Debug.Log("Tile at " + row + ", " + col + " is not adjacent to empty slot.");
        }

        //Debug.Log("CheckSolve");
        if (IsPuzzleSolved())
        {
            Debug.Log("Puzzle Solved!");
            // Aqui você pode adicionar lógica para o que acontece quando o puzzle é resolvido
        }
    }

    bool IsAdjacent(int row1, int col1, int row2, int col2)
    {
        return (Mathf.Abs(row1 - row2) == 1 && col1 == col2) ||
               (Mathf.Abs(col1 - col2) == 1 && row1 == row2);
    }

    void FindEmptySlot(out int emptyRow, out int emptyCol)
    {
        for (int i = 0; i < puzzleSize; i++)
        {
            for (int j = 0; j < puzzleSize; j++)
            {
                if (puzzleGrid[i, j] == -1)
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
            if (newRow >= 0 && newRow < puzzleSize && newCol >= 0 && newCol < puzzleSize)
            {
                if (puzzleGrid[newRow, newCol] != -1)
                    adjacents.Add((newRow, newCol));
            }
        }
        return adjacents;
    }

    public void PrintPuzzle()
    {
        for (int i = 0; i < puzzleSize; i++)
        {
            string row = "";
            for (int j = 0; j < puzzleSize; j++)
            {
                row += puzzleGrid[i, j] + " ";
            }
            Debug.Log(row);
        }
    }

    public bool IsPuzzleSolved()
    {
        int expectedValue = 0;
        for (int i = 0; i < puzzleSize; i++)
        {
            for (int j = 0; j < puzzleSize; j++)
            {
                if (i == puzzleSize - 1 && j == puzzleSize - 1)
                {
                    if (puzzleGrid[i, j] != -1) // Last piece should be empty
                        return false;
                }
                else
                {
                    if (puzzleGrid[i, j] != expectedValue)
                        return false;
                    expectedValue++;
                }
            }
        }
        return true;
    }
}

