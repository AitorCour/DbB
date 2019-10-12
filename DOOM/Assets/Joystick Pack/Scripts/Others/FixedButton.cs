using UnityEngine;
using UnityEngine.EventSystems;

public class FixedButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    public bool Pressed;

    // Use this for initialization
    void Start()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }
}
