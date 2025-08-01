using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public CursorManager instance;
    public Texture2D defaultCursorTexture;
    public Texture2D selectCursorTexture;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SetDefaultCursor();
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetDefaultCursor()
    {
        if (defaultCursorTexture != null)
        {
            Cursor.SetCursor(defaultCursorTexture, Vector2.zero, CursorMode.Auto);
        }
    }

    public void SetSelectCursor()
    {
        if (selectCursorTexture != null)
        {
            Cursor.SetCursor(selectCursorTexture, Vector2.zero, CursorMode.Auto);
        }
    }

}
