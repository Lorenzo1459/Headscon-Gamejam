using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PuzzleObject", menuName = "Puzzle/PuzzleObject")]
public class PuzzleObject : ScriptableObject
{
    public int puzzleSize = 3;
    public Sprite completed;
    public List<Sprite> spriteList;
}
