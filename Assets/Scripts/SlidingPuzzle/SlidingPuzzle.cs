using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SlidingPuzzle : MonoBehaviour
{
    public static SlidingPuzzle instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Configuração do Puzzle
    public PuzzleObject puzzleObject;
    public int puzzleSize = 3;
    private int[,] puzzleGrid;

    // Elementos da UI
    public GameObject puzzlePiecePrefab;
    public GameObject parent;
    public GameObject solvedImage;
    private readonly List<GameObject> piecesList = new List<GameObject>();
    public List<Sprite> spriteList;
    private Sprite solvedSprite;

    private bool checking = false;
    private bool solved = false;

    void Start()
    {
        LoadPuzzle();
    }

    public void ClearPuzzle()
    {
        solved = false;
        solvedImage.SetActive(false);
        foreach (GameObject piece in piecesList)
        {
            DestroyImmediate(piece);
        }
        piecesList.Clear();
    }

    public void LoadPuzzle()
    {
        ClearPuzzle();

        puzzleSize = puzzleObject.puzzleSize;
        spriteList = puzzleObject.spriteList;
        solvedSprite = puzzleObject.completed;

        puzzleGrid = new int[puzzleSize, puzzleSize];
        for (int i = 0, k = 0; i < puzzleSize; i++)
        {
            for (int j = 0; j < puzzleSize; j++)
            {
                puzzleGrid[i, j] = k;

                GameObject piece = Instantiate(puzzlePiecePrefab, parent.transform);
                PuzzlePiece puzzlePieceComponent = piece.GetComponent<PuzzlePiece>();

                if (puzzlePieceComponent != null)
                {
                    puzzlePieceComponent.Initialize(k, (i * puzzleSize + j), spriteList[k], this);
                }
                else
                {
                    Debug.LogError("O componente PuzzlePiece não foi encontrado no prefab!", piece);
                }

                piecesList.Add(piece);
                UpdatePieceVisuals(i, j);
                k++;
            }
        }

        int lastIndex = puzzleSize - 1;
        puzzleGrid[lastIndex, lastIndex] = -1;
        piecesList[puzzleSize * puzzleSize - 1].SetActive(false);

        RandomizePuzzle();
    }

    public void RandomizePuzzle()
    {
        checking = false;
        for (int i = 0; i < puzzleSize * puzzleSize * 10; i++)
        {
            MoveTile(Random.Range(0, puzzleSize), Random.Range(0, puzzleSize));
        }
        checking = true;
    }

    public void MoveTile(int clickedRow, int clickedCol)
    {
        if (solved) return;
        if (clickedRow < 0 || clickedRow >= puzzleSize || clickedCol < 0 || clickedCol >= puzzleSize || puzzleGrid[clickedRow, clickedCol] == -1)
        {
            return;
        }

        FindEmptySlot(out int emptyRow, out int emptyCol);

        if (clickedRow != emptyRow && clickedCol != emptyCol)
        {
            return;
        }

        if (clickedRow == emptyRow)
        {
            if (clickedCol < emptyCol)
            {
                for (int j = emptyCol; j > clickedCol; j--)
                {
                    puzzleGrid[emptyRow, j] = puzzleGrid[emptyRow, j - 1];
                }
            }
            else
            {
                for (int j = emptyCol; j < clickedCol; j++)
                {
                    puzzleGrid[emptyRow, j] = puzzleGrid[emptyRow, j + 1];
                }
            }
        }
        else
        {
            if (clickedRow < emptyRow)
            {
                for (int i = emptyRow; i > clickedRow; i--)
                {
                    puzzleGrid[i, emptyCol] = puzzleGrid[i - 1, emptyCol];
                }
            }
            else
            {
                for (int i = emptyRow; i < clickedRow; i++)
                {
                    puzzleGrid[i, emptyCol] = puzzleGrid[i + 1, emptyCol];
                }
            }
        }

        puzzleGrid[clickedRow, clickedCol] = -1;

        if (clickedRow == emptyRow)
        {
            for (int j = 0; j < puzzleSize; j++) UpdatePieceVisuals(clickedRow, j);
        }
        else
        {
            for (int i = 0; i < puzzleSize; i++) UpdatePieceVisuals(i, clickedCol);
        }

        if (IsPuzzleSolved())
        {   
            Debug.Log("Puzzle Resolvido!");
            piecesList[puzzleSize * puzzleSize - 1].SetActive(true);
            UpdatePieceVisuals(puzzleSize - 1, puzzleSize - 1);
            ClearPuzzle();
            solvedImage.SetActive(true);
        }
    }

    private void UpdatePieceVisuals(int row, int col)
    {
        int pieceValue = puzzleGrid[row, col];
        if (pieceValue == -1) return;

        GameObject piece = piecesList[pieceValue];

        PuzzlePiece puzzlePieceComponent = piece.GetComponent<PuzzlePiece>();
        if (puzzlePieceComponent != null)
        {
            puzzlePieceComponent.piecePosition = row * puzzleSize + col;
        }

        RectTransform pieceRect = piece.GetComponent<RectTransform>();
        float backgroundWidth = parent.GetComponent<RectTransform>().rect.width;
        float backgroundHeight = parent.GetComponent<RectTransform>().rect.height;

        pieceRect.sizeDelta = new Vector2(backgroundWidth / puzzleSize, backgroundHeight / puzzleSize);
        pieceRect.localScale = new Vector3(0.95f, 0.95f, 1f);

        pieceRect.anchoredPosition = new Vector2(
            (col - (puzzleSize - 1) / 2f) * (backgroundWidth / puzzleSize),
            ((puzzleSize - 1) / 2f - row) * (backgroundHeight / puzzleSize)
        );
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

    public bool IsPuzzleSolved()
    {
        if (!checking) return false;

        int expectedValue = 0;
        for (int i = 0; i < puzzleSize; i++)
        {
            for (int j = 0; j < puzzleSize; j++)
            {
                if (i == puzzleSize - 1 && j == puzzleSize - 1)
                {
                    if (puzzleGrid[i, j] != -1) return false;
                }
                else
                {
                    if (puzzleGrid[i, j] != expectedValue) return false;
                    expectedValue++;
                }
            }
        }

        solved = true;
        return true;
    }
}