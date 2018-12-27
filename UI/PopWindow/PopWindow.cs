using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopWindow : UIWindowBase {
    public override void OnInit()
    {
        base.OnInit();
        print("123");
    }

    public override void OnOpen()
    {
        AddOnClickListener("Button_Start", Button_StartEvent);
        Debug.Log("CloseWindow");

    }

    private void Button_StartEvent(InputUIOnClickEvent inputEvent)
    {
        UIManager.CloseUIWindow< PopWindow >();
        UIManager.OpenUIWindow<MainWindow>();
    }

    public override void OnRefresh()
    {
        base.OnRefresh();
       // print("123");
    }

    public override IEnumerator ExitAnim(UIAnimCallBack l_animComplete, UICallBack l_callBack, params object[] objs)
    {
        //print("123");

        return base.ExitAnim(l_animComplete, l_callBack, objs);
    }

    public override IEnumerator EnterAnim(UIAnimCallBack l_animComplete, UICallBack l_callBack, params object[] objs)
    {
        //print("123");
        AnimSystem.UguiColor(gameObject, Color.white, Color.green, 5f);
        yield return new WaitForEndOfFrame() ;
    }
}
