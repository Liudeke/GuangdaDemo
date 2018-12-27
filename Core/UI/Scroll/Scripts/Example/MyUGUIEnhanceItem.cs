using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class MyUGUIEnhanceItem : EnhanceItem
{
    private Button uButton;
    private RawImage rawImage;

    protected override void OnStart()
    {
        rawImage = GetComponent<RawImage>();
        uButton = GetComponent<Button>();
        uButton.onClick.AddListener(OnClickUGUIButton);
        transform.GetChild(1).GetComponent<Button>().onClick.AddListener(OnClickEvent);
    }

    private void OnClickEvent()
    {
       print("点击image");
    }

    private void OnClickUGUIButton()
    {
        OnClickEnhanceItem();
    }

    // Set the item "depth" 2d or 3d
    protected override void SetItemDepth(float depthCurveValue, int depthFactor, float itemCount)
    {
        int newDepth = (int)(depthCurveValue * itemCount);
        this.transform.SetSiblingIndex(newDepth);
    }

    public override void SetSelectState(bool isCenter)
    {
        if (rawImage == null)
            rawImage = GetComponent<RawImage>();
        rawImage.color = isCenter ? Color.white : Color.gray;
        if (isCenter)
        {
            GlobalEvent.DispatchEvent(ZaiYunYingWindowEvent.SetIntroduceTex,
                transform.GetChild(0).GetComponent<Text>().text);
        }
    }
}
