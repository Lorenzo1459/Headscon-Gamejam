using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class PuzzlePiece : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int pieceValue;
    public int piecePosition;
    public Sprite sprite;
    public TextMeshProUGUI rightpostext;

    
    private Vector3 hoverScale = new Vector3(1f, 1f, 1f); // Scale when hovered
    private Vector3 originalScale = new Vector3(.95f, .95f, 1f);
    private SlidingPuzzle slidingPuzzle;

    void Awake()
    {
        if (rightpostext == null)
            rightpostext = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Initialize(int value, int position, Sprite pieceSprite, SlidingPuzzle puzzle)
    {
        pieceValue = value;
        piecePosition = position;
        sprite = pieceSprite;
        slidingPuzzle = puzzle; // Armazena a referência
        GetComponent<Image>().sprite = sprite;
        if (rightpostext == null)
            rightpostext = GetComponentInChildren<TextMeshProUGUI>();
        if (rightpostext != null)
            rightpostext.text = (pieceValue + 1).ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (slidingPuzzle == null)
        {
            Debug.LogWarning("SlidingPuzzle não atribuído em PuzzlePiece.");
            return;
        }

        int puzzleSize = slidingPuzzle.puzzleSize;
        int row = piecePosition / puzzleSize;
        int col = piecePosition % puzzleSize;
        Debug.Log($"Clicked piece at value: {pieceValue}, position: {piecePosition}, row: {row}, col: {col}");
        slidingPuzzle.MoveTile(row, col);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Instantly set to hover scale
        transform.localScale = hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Instantly set back to original scale
        transform.localScale = originalScale;
    }

    public bool IsCorrectPosition()
    {
        return piecePosition == pieceValue;
    }
}