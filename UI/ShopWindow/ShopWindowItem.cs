using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopWindowItem : ReusingScrollItemBase
{
    private ReusingScrollRect m_hrozontalItem;
    public override void SetContent(int index, Dictionary<string, object> data)
    {
        SetText("Text_Name", (string)data["Name"]);
        SetText("Text_Cost", "$ "+ (string)data["Cost"]);
       
    }

    public override void OnInit()
    {
        base.OnInit();
        //m_hrozontalItem = GetReusingScrollRect("Items");
        //m_hrozontalItem.Init(UIEventKey, "HorizontalItem");
        //m_hrozontalItem.SetData(GetShopData1());
        //AddOnClickListener("listion", listionEvent);
    }

    private void listionEvent( )
    {
        Debug.Log(UIID);

    }

    public override void OnShow()
    {
        
    }
    List<Dictionary<string, object>> GetShopData1()
    {
        List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
        for (int i = 0; i < 20; i++)
        {
            Dictionary<string, object> tmp = new Dictionary<string, object>();

            tmp.Add("ID", i.ToString());
           // tmp.Add("Cost", i.ToString());
            data.Add(tmp);

        }

        return data;

    }
}
