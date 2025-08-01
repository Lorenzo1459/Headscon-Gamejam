using UnityEngine;

public class ActiveNPC : MonoBehaviour
{
    public int activeNPC = 0;
    public bool showTutorial = true;
    public static ActiveNPC instance;
    
    void Awake()
    {
        if (instance == null)
        {
            activeNPC = -1;
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
