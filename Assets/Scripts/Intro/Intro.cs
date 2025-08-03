using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; 

public class Intro : MonoBehaviour
{
    public List<Sprite> spritelist;
    private int id = 0;
    public float cooldown = .1f;
    public float autopass = 5f;
    private float timer = 0f;
    public GameObject continueText;

    public Image imageObject;

    private CursorManager cursorManager;

    void Start()
    {
        cursorManager = FindFirstObjectByType<CursorManager>();
        continueText.SetActive(false);
        if (spritelist != null && spritelist.Count > 0 && imageObject != null)
        {
            id = 0;
            imageObject.sprite = spritelist[0]; // Inicializa a imagem
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= cooldown)
        {
            continueText.SetActive(true);
         
            if (Input.GetMouseButtonDown(0) || timer >= autopass) // || timer >= autopass)
            {
                if (cursorManager != null)
                {
                    cursorManager.instance.SetDefaultCursor();
                }
                timer = 0f;
                continueText.SetActive(false);
                id++;
                if (spritelist != null && id < spritelist.Count && imageObject != null)
                {
                    imageObject.sprite = spritelist[id];
                } else {
                    SceneLoader.Instance.LoadScene("Ingame");
                }
            }
        }
    }
}