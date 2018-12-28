using System.Collections;
using System.Collections.Generic;
using Assets._Scripts.Controll;
using UnityEngine;
using UnityEngine.UI;


public enum ZaiYunYingWindowEvent
{
    ShoWYunYingItem,
    ShoWJianSheItem,
    ShoWDiQuItem,
    EnableMask,
    DisableMask,
    UGUIMove,
    SetIntroduceTex
}

[System.Serializable]
public class MoveInfo
{
    public string ID;
    public GameObject go;
    public Vector3 v3;
}

public class ZaiYunYingWindow : UIWindowBase
{
    ReusingScrollRect m_ItemYunYing;
    ReusingScrollRect m_ItemJianShe;
    ReusingScrollRect m_ItemDiQu;
    public int numberOfline = 7;

    List<List<string>> tempYunYingListstr = new List<List<string>>();
    List<List<string>> tempJianSheListstr = new List<List<string>>();
    Mask maskYunYing;
    Image imgYunying;


    Mask maskJianShe;
    Image imgJianShe;

    private Text introText;

    public List<MoveInfo> listMoveInfo;

    public override void OnOpen()
    {
        GameObject goYunying = GetGameObject("chouhuaWancheng");
        m_ItemYunYing =goYunying.transform.GetChild(1).GetComponent<ReusingScrollRect>(); //GetReusingScrollRect("Items");
        m_ItemYunYing.Init(UIEventKey, "YunYIngItem");
        maskYunYing = goYunying.transform.GetChild(1).GetComponent<Mask>(); //GetMask("Items");
        imgYunying = goYunying.transform.GetChild(1).GetComponent<Image>(); //GetImage("Items");

        GameObject goJianShe = GetGameObject("chouhuazhong");
        m_ItemJianShe =goJianShe.transform.GetChild(1).GetComponent<ReusingScrollRect>(); //GetReusingScrollRect("ItemsJianshe");
        m_ItemJianShe.Init(UIEventKey, "YunYIngItem");
        maskJianShe = goJianShe.transform.GetChild(1).GetComponent<Mask>(); //GetMask("ItemsJianshe");
        imgJianShe = goJianShe.transform.GetChild(1).GetComponent<Image>(); //GetImage("ItemsJianshe");

        GameObject goDiQu = GetGameObject("diqu");
        m_ItemDiQu = goDiQu.transform.GetChild(1).GetComponent<ReusingScrollRect>(); //GetReusingScrollRect("ItemsDiQu");
        m_ItemDiQu.Init(UIEventKey, "DiQuItem");
        AddOnClickListener("return", Return_Event);

        introText= GetText("IntroduceText");
        GlobalEvent.AddEvent(ZaiYunYingWindowEvent.EnableMask, EnableMask_Event);
        GlobalEvent.AddEvent(ZaiYunYingWindowEvent.ShoWYunYingItem, EnterShowYunYingItem);
        GlobalEvent.AddEvent(ZaiYunYingWindowEvent.ShoWJianSheItem, EnterShowJanSheItem);
        GlobalEvent.AddEvent(ZaiYunYingWindowEvent.ShoWDiQuItem, EnterShowDiQuItem);
        GlobalEvent.AddEvent(ZaiYunYingWindowEvent.UGUIMove, MoveUGUI);
        GlobalEvent.AddEvent(ZaiYunYingWindowEvent.SetIntroduceTex,SetIntroDuceText);

        ItemCreateControll.Instance.TestMothed2();
        EnterShowYunYingItem(new[]
            {ItemCreateControll.Instance.GetShilistFromSheng(ItemCreateControll.Instance.GetShengList()[0])});
        EnterShowJanSheItem(new[]
            {ItemCreateControll.Instance.GetShilistFromSheng(ItemCreateControll.Instance.GetShengList()[0])});
    }

    private void SetIntroDuceText(object[] args)
    {
        introText.text=args[0] as string;
    }

    private void Return_Event(InputUIOnClickEvent inputevent)
    {
        //Button
        GlobalEvent.DispatchEvent(ZaiYunYingWindowEvent.UGUIMove, "1");
        GlobalEvent.DispatchEvent(ZaiYunYingWindowEvent.UGUIMove, "2");
        GlobalEvent.DispatchEvent(ZaiYunYingWindowEvent.UGUIMove, "7");
        GlobalEvent.DispatchEvent(ZaiYunYingWindowEvent.UGUIMove, "8");
    }

    private void EnableMask_Event(object[] args)
    {
        maskYunYing.enabled = true;
        imgYunying.enabled = true;
        maskJianShe.enabled = true;
        imgJianShe.enabled = true;
    }

    private void EnterShowYunYingItem(object[] args)
    {
        maskYunYing.enabled = false;
        imgYunying.enabled = false;

        m_ItemYunYing.Dispose();
        tempYunYingListstr.Clear();
        m_ItemYunYing.Init(UIEventKey, "YunYIngItem");

        GlobalEvent.DispatchEvent(ItemCreateControllEvent.ClearYnYingItems);
        List<string> temp = (List<string>) args[0];

        int lines = 0;
        //m_ItemYunYing.SortVeriticalOrHoriticalLine(numberOfline, temp, ref tempYunYingListstr, ref lines);
        m_ItemYunYing.SortVeriticalOrHoriticalLine<string>(numberOfline, temp, ref tempYunYingListstr, ref lines);
        if (tempYunYingListstr.Count == lines)
        {
            m_ItemYunYing.SetData(GetDatas(lines, tempYunYingListstr, "list"));
        }
        else
        {
            Debug.LogWarning("==>tempYunYingListstr<==集合数量不对" + ParseTool.GetCodeOutPutInfo());
        }

        GlobalEvent.DispatchEvent(ItemCreateControllEvent.PlayYunYingItemAni);
    }

    private void EnterShowJanSheItem(object[] args)
    {
        maskJianShe.enabled = false;
        imgJianShe.enabled = false;
        m_ItemJianShe.Dispose();
        tempJianSheListstr.Clear();
        m_ItemJianShe.Init(UIEventKey, "YunYIngItem");
        GlobalEvent.DispatchEvent(ItemCreateControllEvent.ClearJianSheItems);
        List<string> temp = (List<string>) args[0];

        int lines = 0;
        m_ItemJianShe.SortVeriticalOrHoriticalLine<string>(numberOfline, temp, ref tempJianSheListstr, ref lines);
        if (tempJianSheListstr.Count == lines)
        {
            m_ItemJianShe.SetData(GetDatas(lines, tempJianSheListstr, "JianShe_list"));
        }
        else
        {
            Debug.LogWarning("==>tempYunYingListstr<==集合数量不对");
        }


        GlobalEvent.DispatchEvent(ItemCreateControllEvent.PlayJianSheItemAni);
    }

    private void EnterShowDiQuItem(object[] args)
    {
        m_ItemDiQu.Dispose();
        m_ItemDiQu.Init(UIEventKey, "DiQuItem");

        List<string> temp = (List<string>) args[0];
        int lines = 0;
        List<List<string>> tempList = new List<List<string>>();

        m_ItemDiQu.SortVeriticalOrHoriticalLine<string>(2, temp, ref tempList, ref lines);
        m_ItemDiQu.SetData(GetDatas(lines, tempList, "DiQu"));
    }

    void MoveUGUI(object[] ID)
    {
        for (int i = 0; i < listMoveInfo.Count; i++)
        {
            if ((string) ID[0] == listMoveInfo[i].ID)
            {
                AnimSystem.UguiMove(listMoveInfo[i].go, null, listMoveInfo[i].v3, time: 1f,
                    interp: InterpType.InOutBack, repeatType: RepeatType.Once);
            }
        }
    }

    List<Dictionary<string, object>> GetDatas(int lines, List<List<string>> liststr, string NameID)
    {
        List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
        for (int i = 0; i < lines; i++)
        {
            Dictionary<string, object> tmp = new Dictionary<string, object>();
            tmp.Add(NameID, liststr[i]); //"list"
            data.Add(tmp);
        }

        return data;
    }

    List<Dictionary<string, object>> GetDatas1()
    {
        List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
        for (int i = 0; i < 3; i++)
        {
            Dictionary<string, object> tmp = new Dictionary<string, object>();
            List<string> tempList = new List<string>();
            tempList.Add(i.ToString());
            tempList.Add(i.ToString() + "2358");
            print(tempList.Count + "****");
            tmp.Add("DiQu", tempList);
            data.Add(tmp);
        }

        return data;
    }

    public override IEnumerator EnterAnim(UIAnimCallBack l_animComplete, UICallBack l_callBack, params object[] objs)
    {
        GlobalEvent.DispatchEvent(ZaiYunYingWindowEvent.UGUIMove, "1");
        GlobalEvent.DispatchEvent(ZaiYunYingWindowEvent.UGUIMove, "2");
        yield return new WaitForSeconds(0.7f);
    }
}