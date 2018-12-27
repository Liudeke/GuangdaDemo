﻿using UnityEngine;
using UnityEngine.UI;
public class EventPenetrate : MonoBehaviour, ICanvasRaycastFilter
{
    //作为目标点击事件渗透区域
    public Image target;
    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        //没有目标则捕捉事件渗透
        if (target == null)
        {
            return true;
        }
        //在目标范围内做事件渗透
        return !RectTransformUtility.RectangleContainsScreenPoint(target.rectTransform,
            sp, eventCamera);
    }
    // Use this for initialization
    void Start()
    {
    }
}