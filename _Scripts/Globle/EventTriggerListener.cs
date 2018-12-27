using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(LayoutElement))]
public class EventTriggerListener : EventTrigger
{
    public delegate void VoidDelegate(GameObject obj);
    //点击
    public VoidDelegate onClick;
    //鼠标按下
    public VoidDelegate onDown;
    //鼠标抬起
    public VoidDelegate onUp;
    //鼠标移入
    public VoidDelegate onEnter;
    //鼠标移出
    public VoidDelegate onExit;
    private Vector2 currentSize;
    private Vector2 targetSize;
    private float speed = 4.0f;
    public static EventTriggerListener GetComponent(GameObject obj)
    {
        EventTriggerListener listener = obj.GetComponent<EventTriggerListener>();
        if (listener == null)
        {
            listener = obj.AddComponent<EventTriggerListener>();
        }


        return listener;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null)
        {
            onClick(gameObject);
        }
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null) onDown(gameObject);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) onUp(gameObject);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null) onEnter(gameObject);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null) onExit(gameObject);
    }
}
