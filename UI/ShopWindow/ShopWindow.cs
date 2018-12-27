using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ShopWindow : UIWindowBase 
{
    ReusingScrollRect m_ShopItem;

    //UI的初始化请放在这里
    public override void OnOpen()
    {
        m_ShopItem = GetReusingScrollRect("Items");
        m_ShopItem.Init(UIEventKey, "ShopWindow_Item");
        //print(m_ShopItem.m_items.Count + "---2");

        m_ShopItem.SetData(GetShopData());//refresh//refreshTwo
        //print(m_ShopItem.m_items.Count + "---3");

        AddOnClickListener("Button_Close", OnClickCLose);
        AddOnClickListener("refresh", OnClickrefresh);
        AddOnClickListener("refreshTwo", OnClickrerefreshTwo);
    }

    private void OnClickrerefreshTwo(InputUIOnClickEvent inputEvent)
    {
        m_ShopItem.Dispose();
        m_ShopItem.Init(UIEventKey, "ShopWindow_Item");

        m_ShopItem.SetData(GetShopData());//r
    }

    private void OnClickrefresh(InputUIOnClickEvent inputEvent)
    {
        m_ShopItem.Dispose();

        m_ShopItem.Init(UIEventKey, "ShopWindow_Item");

        m_ShopItem.SetData(GetShopData1());//refresh
    }

    //请在这里写UI的更新逻辑，当该UI监听的事件触发时，该函数会被调用
    public override void OnRefresh()
    {

    }

    //UI的进入动画
    public override IEnumerator EnterAnim(UIAnimCallBack l_animComplete, UICallBack l_callBack, params object[] objs)
    {
        AnimSystem.UguiAlpha(gameObject, 0, 1, callBack:(object[] obj)=>
        {
            StartCoroutine(base.EnterAnim(l_animComplete, l_callBack, objs));
        });

        AnimSystem.UguiMove(m_uiRoot, new Vector3(1000, 0, 0), Vector3.zero, time: 1, interp: InterpType.InOutBack);

        yield return new WaitForEndOfFrame();
    }

    //UI的退出动画
    public override IEnumerator ExitAnim(UIAnimCallBack l_animComplete, UICallBack l_callBack, params object[] objs)
    {
        AnimSystem.UguiAlpha(gameObject , null, 0, callBack:(object[] obj) =>
        {
            StartCoroutine(base.ExitAnim(l_animComplete, l_callBack, objs));
        });

        AnimSystem.UguiMove(m_uiRoot,Vector3.zero, new Vector3(1000, 0, 0),time:1,interp:InterpType.InOutBack);

        yield return new WaitForEndOfFrame();
    }

    List<Dictionary<string, object>> GetShopData1()
    {
        List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
        for (int i = 0; i < 100; i++)
        {
            Dictionary<string, object> tmp = new Dictionary<string, object>();

            tmp.Add("Name", i.ToString());
            tmp.Add("Cost", i.ToString());
            data.Add(tmp);

        }

        return data;

    }

    List<Dictionary<string,object>> GetShopData()
    {
        List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();

        DataTable itemData = DataManager.GetData("item");

        for (int i = 0; i < itemData.TableIDs.Count; i++)
        {
            SingleData singleData = itemData.GetLineFromKey(itemData.TableIDs[i]);

            string itemName = singleData.GetString("ItemName");
            int cost = singleData.GetInt("Cost");

            Dictionary<string, object> tmp = new Dictionary<string, object>();

            tmp.Add("Name", DataGenerateManager<itemGenerate>.GetData(itemData.TableIDs[i]).m_ItemName);
            tmp.Add("Cost", DataGenerateManager<itemGenerate>.GetData(itemData.TableIDs[i]).m_key);

            data.Add(tmp);
        }


        //itemGenerate data = DataGenerateManager<itemGenerate>.GetData("1");
        //string itemName = data.m_ItemName;
        //int cost = data.m_Cost;
        List<Dictionary<string, object>> data1 = new List<Dictionary<string, object>>();
        for (int i = 0; i < 15; i++)
        {
            Dictionary<string, object> tmp = new Dictionary<string, object>();

            tmp.Add("Name", i.ToString());
            tmp.Add("Cost", i.ToString());
            data.Add(tmp);

        }
      //  data.Insert(1, new Dictionary<string, object> {{ "Name", "Name" } });

        return data1;
    }

    void OnClickCLose(InputUIOnClickEvent e)
    {
        UIManager.CloseUIWindow(this);
    }
}