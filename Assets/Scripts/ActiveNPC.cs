using UnityEngine;

public class ActiveNPC : MonoBehaviour
{
    public int activeNPC = 0;
    public static ActiveNPC instance;
    
    void Awake()
    {
        if (instance == null)
        {
            activeNPC = 0;
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
