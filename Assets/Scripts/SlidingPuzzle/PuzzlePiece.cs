using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems; // Required for IPointerClickHandler

[RequireComponent(typeof(Image))]
public class PuzzlePiece : MonoBehaviour, IPointerClickHandler // Implement IPointerClickHandler
{
    public int pieceValue;
    public int piecePosition;
    public Sprite sprite;
    public TextMeshProUGUI rightpostext;

    // Add a reference to the SlidingPuzzle script
    private SlidingPuzzle slidingPuzzleRef; // This will hold a reference to your main puzzle script

    void Awake()
    {
        if (rightpostext == null)
            rightpostext = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        // This might be redundant if Initialize is always called right after Instantiate
        // piecePosition = pieceValue;
        // GetComponent<Image>().sprite = sprite;

        // Find the SlidingPuzzle script in the scene (assuming it's on the parent GameObject or an easily findable object)
        // A more robust way might be to pass it during initialization if the parent is not guaranteed
        slidingPuzzleRef = FindObjectOfType<SlidingPuzzle>();
        if (slidingPuzzleRef == null)
        {
            Debug.LogError("SlidingPuzzle script not found in the scene! Cannot handle clicks.");
        }
    }

    // Inside PuzzlePiece.cs
    // Remove FindObjectOfType from Start() or Awake()
    // private SlidingPuzzle slidingPuzzleRef; // No need to find it, it will be passed

    // Modify the Initialize method signature to accept the SlidingPuzzle reference
    public void Initialize(int value, int position, Sprite pieceSprite, SlidingPuzzle puzzleManager)
    {
        pieceValue = value;
        piecePosition = position;
        sprite = pieceSprite;
        GetComponent<Image>().sprite = sprite;
        if (rightpostext == null)
            rightpostext = GetComponentInChildren<TextMeshProUGUI>();
        if (rightpostext != null)
            rightpostext.text = (pieceValue + 1).ToString();

        // Assign the passed reference
        slidingPuzzleRef = puzzleManager;
    }

    // Keep OnPointerClick as is, it will use the assigned slidingPuzzleRef
    public void OnPointerClick(PointerEventData eventData)
    {
        if (slidingPuzzleRef != null)
        {
            int puzzlesize = slidingPuzzleRef.puzzlesize;
            int row = piecePosition / puzzlesize;
            int col = piecePosition % puzzlesize;
            Debug.Log($"Clicked piece at value: {pieceValue}, position: {piecePosition}, row: {row}, col: {col}");
            slidingPuzzleRef.MoveTile(row, col);
        }
    }

    public bool IsCorrectPosition()
    {
        return piecePosition == pieceValue;
    }
}