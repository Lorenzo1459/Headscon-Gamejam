using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NPC", menuName = "NPC/New NPC")]
public class NPC : ScriptableObject
{
    [Header("Atributos")]
    public string nome;
    public Sprite sprite;
    //bool minigameSolved = false;

    [Header("Minigame")]
    public PuzzleObject puzzleObject;
    public string minigameSceneName;
    //public string [] dialogos;
}
