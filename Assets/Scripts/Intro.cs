using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; 

public class Intro : MonoBehaviour
{
    public List<Sprite> spritelist;
    private int id = 0;
    public float cooldown = 1f;
    private float timer = 0f;
    public GameObject continueText;

    public Image imageObject;

    void Start()
    {
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
         
            if (id >= spritelist.Count - 1)
            {
                continueText.SetActive(false);
                return; 
            }

            if (Input.GetMouseButtonDown(0))
            {
                continueText.SetActive(false);
                timer = 0f;
                id++;
                if (spritelist != null && id < spritelist.Count && imageObject != null)
                {
                    imageObject.sprite = spritelist[id];
                }
            }
        }
    }
}