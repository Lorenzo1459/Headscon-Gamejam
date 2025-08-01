using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Make instance static for easy access
    public ActiveNPC anpc;
    public int activeNPCIndex = -1; // This can be used if you want to track the active NPC index separately
    public bool isPuzzleSolved = false;
    public GameObject tutorialObject;
    public List<NPC> npcList;
    public SlidingPuzzle slidingPuzzle;
    public List<Image> npcImageList;
    //public int anpc.activeNPC = 0;

    void Awake()
    {
        if (instance == null)
        {
            anpc = FindFirstObjectByType<ActiveNPC>();
            if(anpc != null) activeNPCIndex = anpc.activeNPC;
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        slidingPuzzle = FindFirstObjectByType<SlidingPuzzle>();
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

    

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (anpc != null)
        {
            activeNPCIndex = anpc.activeNPC;
            if (anpc.showTutorial)
            {
                tutorialObject.SetActive(true);
            }
        }

        Debug.Log("Scene loaded: " + scene.name);
        //UpdatePuzzle();
    }

    void UpdatePuzzle()
    {
        for (int i = 0; i < npcImageList.Count; i++)
        {
            if (i == activeNPCIndex) npcImageList[i].gameObject.SetActive(true);
            else npcImageList[i].gameObject.SetActive(false);
        }

        if (slidingPuzzle != null)
        {
            if (npcList != null && activeNPCIndex >= 0 && activeNPCIndex < npcList.Count)
            {
                if (npcList[activeNPCIndex] != null && npcList[activeNPCIndex].puzzleObject != null)
                {
                    slidingPuzzle.puzzleObject = npcList[activeNPCIndex].puzzleObject;
                    slidingPuzzle.LoadPuzzle();
                }
            }
        }
    }


    public void GoToMinigame()
    {
        SceneLoader.Instance.LoadScene(npcList[activeNPCIndex].minigameSceneName);
    }

    public void NextNPC()
    {
        NotFirstTime();
        activeNPCIndex++;
        if (activeNPCIndex >= npcList.Count)
        {
            activeNPCIndex = 0; // Loop back to the first NPC
        }

        if (anpc != null) anpc.activeNPC++;

        UpdatePuzzle();
    }

    public void NotFirstTime()
    {
        if(anpc != null)
        {
            anpc.showTutorial = false;
            tutorialObject.SetActive(false);
        }
    }

    public void StartMinigame()
    {
        SceneLoader.Instance.LoadScene(npcList[activeNPCIndex].minigameSceneName);
    }
}