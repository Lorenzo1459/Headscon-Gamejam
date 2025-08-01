using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    CursorManager cursorManager;

    void Start()
    {
        cursorManager = FindFirstObjectByType<CursorManager>();
        if (cursorManager != null)
        {
            cursorManager.SetDefaultCursor();
        }
    }

    // For GameObjects with Collider components
    void OnMouseOver()
    {
        cursorManager.instance.SetSelectCursor();
    }

    void OnMouseExit()
    {
        cursorManager.instance.SetDefaultCursor();
    }

    // For UI elements
    public void OnPointerEnter(PointerEventData eventData)
    {
        cursorManager.instance.SetSelectCursor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cursorManager.instance.SetDefaultCursor();
    }

}
