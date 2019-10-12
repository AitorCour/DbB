using UnityEngine;
using UnityEngine.EventSystems;

public class FixedMoveCameraButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    public bool Pressed;
    private FixedTouchField fixedTouch;

    // Use this for initialization
    void Start()
    {
        fixedTouch = GameObject.FindGameObjectWithTag("TouchField").GetComponent<FixedTouchField>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        fixedTouch.OnPointerDown(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
        fixedTouch.OnPointerUp(eventData);
    }
}
