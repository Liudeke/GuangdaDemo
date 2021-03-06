using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainWindow : UIWindowBase 
{

    //UI的初始化请放在这里
    public override void OnOpen()
    {
        AddOnClickListener("Button_Setting", OnClickSetting);
        AddOnClickListener("Button_Shop", OnClickShop);
        AddOnClickListener("Button_Start", OnGameStart);
    }

    //请在这里写UI的更新逻辑，当该UI监听的事件触发时，该函数会被调用
    public override void OnRefresh()
    {
        print("触发事件");
    }

    //UI的进入动画
    public override IEnumerator EnterAnim(UIAnimCallBack l_animComplete, UICallBack l_callBack, params object[] objs)
    {
        AnimSystem.UguiAlpha(gameObject, 0, 1, callBack:(object[] obj)=>
        {
            StartCoroutine(base.EnterAnim(l_animComplete, l_callBack, objs));
        });

        yield return new WaitForEndOfFrame();
    }

    //UI的退出动画
    public override IEnumerator ExitAnim(UIAnimCallBack l_animComplete, UICallBack l_callBack, params object[] objs)
    {
        AnimSystem.UguiAlpha(gameObject , null, 0, callBack:(object[] obj) =>
        {
            StartCoroutine(base.ExitAnim(l_animComplete, l_callBack, objs));
        });

        yield return new WaitForEndOfFrame();
    }

    public void OnGameStart(InputUIOnClickEvent e)
    {
       // ApplicationStatusManager.EnterStatus<GameStatus>();
        UIManager.OpenUIWindow<PopWindow>();
        UIManager.CloseUIWindow(this);
        //SceneManager.LoadScene("test1");
        //Debug.Log("GameStatus");
    }

    public void OnClickShop(InputUIOnClickEvent e)
    {
        //UIManager.OpenUIWindow<LiuTest>();
        UIManager.CloseUIWindow(this);

    }

    public void OnClickSetting(InputUIOnClickEvent e)
    {
        UIManager.CloseLastUI();
    }
}