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

    private SlidingPuzzle slidingPuzzle; // Refer�ncia ao script principal do puzzle

    void Awake()
    {
        if (rightpostext == null)
            rightpostext = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Corrigido: Recebe refer�ncia ao SlidingPuzzle
    public void Initialize(int value, int position, Sprite pieceSprite, SlidingPuzzle puzzle)
    {
        pieceValue = value;
        piecePosition = position;
        sprite = pieceSprite;
        slidingPuzzle = puzzle; // Armazena a refer�ncia
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
            Debug.LogWarning("SlidingPuzzle n�o atribu�do em PuzzlePiece.");
            return;
        }
        int puzzleSize = slidingPuzzle.puzzleSize;
        int row = piecePosition / puzzleSize;
        int col = piecePosition % puzzleSize;
        Debug.Log($"Clicked piece at value: {pieceValue}, position: {piecePosition}, row: {row}, col: {col}");
        slidingPuzzle.MoveTile(row, col);
    }

    public bool IsCorrectPosition()
    {
        return piecePosition == pieceValue;
    }
}