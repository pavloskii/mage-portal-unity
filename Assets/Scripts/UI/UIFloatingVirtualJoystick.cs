using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIFloatingVirtualJoystick : UIVirtualJoystick
{
    //private Canvas uiCanvas;
    //private Camera camera;

    protected override void Start()
    {
        ShowJoystick(false);
        base.Start();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        ShowJoystick(true);
        containerRect.position = eventData.position;
        handleRect.position = Vector2.zero;
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        ShowJoystick(false);
        base.OnPointerUp(eventData);
    }

    //private Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
    //{
    //    Vector2 localPoint = Vector2.zero;
    //    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, camera, out localPoint))
    //    {
    //        Vector2 pivotOffset = baseRect.pivot * baseRect.sizeDelta;
    //        return localPoint - (background.anchorMax * baseRect.sizeDelta) + pivotOffset;
    //    }
    //    return Vector2.zero;
    //}

    private void ShowJoystick(bool show)
    {
        containerRect.gameObject.SetActive(show);
    }
}
