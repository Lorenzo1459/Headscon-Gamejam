using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Make instance static for easy access
    public ActiveNPC anpc;
    //public int anpc.activeNPC = 0;

    void Awake()
    {
        if (instance == null)
        {
            anpc = FindObjectOfType<ActiveNPC>();
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Subscribe to the sceneLoaded event when this object is enabled
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Unsubscribe from the sceneLoaded event when this object is disabled
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public List<NPC> npcList;
    public SlidingPuzzle slidingPuzzle; // This will be set in OnSceneLoaded
    public Image npcImage;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        UpdatePuzzle();
    }

    void UpdatePuzzle()
    {

        slidingPuzzle = FindObjectOfType<SlidingPuzzle>();
        if (npcImage != null) npcImage.sprite = npcList[anpc.activeNPC].sprite;

        if (slidingPuzzle != null)
        {
            if (npcList != null && anpc.activeNPC >= 0 && anpc.activeNPC < npcList.Count)
            {
                if (npcList[anpc.activeNPC] != null && npcList[anpc.activeNPC].puzzleObject != null)
                {
                    slidingPuzzle.puzzleObject = npcList[anpc.activeNPC].puzzleObject;
                    slidingPuzzle.LoadPuzzle();
                }
            }
        }
    }


    public void GoToMinigame()
    {
        PlayerPrefs.SetInt("anpc.activeNPC", anpc.activeNPC);
        PlayerPrefs.Save();
        SceneLoader.Instance.LoadScene(npcList[anpc.activeNPC].minigameSceneName);
    }

    public void NextNPC()
    {
        anpc.activeNPC++;
        if (anpc.activeNPC >= npcList.Count)
        {
            anpc.activeNPC = 0; // Loop back to the first NPC
        }

        UpdatePuzzle();
    }

}