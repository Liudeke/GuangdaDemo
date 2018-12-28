using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Assets._Scripts.Controll;

public class test : UIBase
{
    // public GameObject cube;
    // public Image Image;
    public RectTransform TargeTransform;


    public override void OnInit()
    {
        // MoveToObj(TargeTransform);
        //  
    }

    void Start()
    {
        EventTriggerListener.GetComponent(gameObject).onEnter += OnMouseEnterEvent;
        EventTriggerListener.GetComponent(gameObject).onExit += OnMouseExitEvent;
        EventTriggerListener.GetComponent(gameObject).onClick += OnMouseClickEvent;
    }

    private void OnMouseClickEvent(GameObject obj)
    {

        GlobalEvent.DispatchEvent(ZaiYunYingWindowEvent.UGUIMove, "5");
        GlobalEvent.DispatchEvent(ZaiYunYingWindowEvent.UGUIMove, "6");
        GlobalEvent.DispatchEvent(ZaiYunYingWindowEvent.UGUIMove, "3");
        GlobalEvent.DispatchEvent(ZaiYunYingWindowEvent.UGUIMove, "4");
    }

    private void OnMouseExitEvent(GameObject obj)
    {
        PlayAniWave(99.5f, 121.76f);
    }

    private void OnMouseEnterEvent(GameObject obj)
    {
        PlayAniWave(120, 146.3f);
    }

    private string NameID = "";
    public void SetTextForChild(string content)
    {
        string[] consStrings = content.Split('|');
        NameID = consStrings[1];
        //("Text", content);
        transform.GetChild(0).GetComponent<Text>().text = consStrings[0];

    }


    public void MoveToObj()
    {
        transform.GetChild(0).GetComponent<Text>().enabled = false;
        AnimSystem.UguiSizeDelta(gameObject, GetComponent<RectTransform>().sizeDelta, new Vector2(241f, 308f)
            , time: 0.2f, interp: InterpType.Linear, callBack: CallEvent_SetSize);
    }
    private void CallEvent_SetSize(object[] arg)
    {
        transform.GetChild(0).GetComponent<Text>().enabled = true;

        AnimSystem.UguiSizeDelta(gameObject, GetComponent<RectTransform>().sizeDelta, new Vector2(25f, 35f)
            , time: 0.2f, interp: InterpType.Linear, callBack: CallEvent_SetSize1);
        if (NameID == "JianShe_list")
        {
            GlobalEvent.DispatchEvent(ItemCreateControllEvent.PlayJianSheItemAni);
        }
        else if (NameID == "list")
        {
            GlobalEvent.DispatchEvent(ItemCreateControllEvent.PlayYunYingItemAni);
        }
    }
    private void CallEvent_SetSize1(object[] arg)
    {
        AnimSystem.UguiSizeDelta(gameObject, GetComponent<RectTransform>().sizeDelta, new Vector2(99.5f, 121.76f)
            , time: 0.2f, interp: InterpType.Linear, callBack: CallEvent_SetSize2);
    }

    private void CallEvent_SetSize2(object[] arg)
    {

    }



    public void PlayAniWave(float x, float y)
    {
        AnimSystem.UguiSizeDelta(
            gameObject,
            GetComponent<RectTransform>().sizeDelta,
            new Vector2(x, y),
            time: 0.2f,
            interp: InterpType.Linear
            );

        // GetComponent<RectTransform>().sizeDelta=new Vector2(x,y);

    }
}
