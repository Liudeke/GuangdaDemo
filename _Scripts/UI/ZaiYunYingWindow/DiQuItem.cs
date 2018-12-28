using System;
using System.Collections;
using System.Collections.Generic;
using Assets._Scripts.Controll;
using UnityEngine;
using UnityEngine.UI;

public class DiQuItem : ReusingScrollItemBase
{
    private List<string> dataList = new List<string>();

    public List<Text> listtext;
    private List<string> listsrt = new List<string>();

    public override void SetContent(int index, Dictionary<string, object> data)
    {

        dataList = (List<string>)data["DiQu"];
        for (int i = 0; i < dataList.Count; i++)
        {
            listsrt.Add(dataList[i]);
            listtext[i].text = dataList[i] + "地区";
        }
        for (int i = 0; i < listtext.Count; i++)
        {
            if (string.IsNullOrEmpty(listtext[i].text))
            {
                listtext[i].transform.parent.gameObject.SetActive(false);
            }
        }

    }

    public override void OnInit()
    {
        AddOnClickListener("Btn1", Btn1_Event);
        AddOnClickListener("Btn2", Btn2_Event);
    }

    private void Btn2_Event(InputUIOnClickEvent inputEvent)
    {
        GlobalEvent.DispatchEvent(ZaiYunYingWindowEvent.ShoWYunYingItem, ItemCreateControll.Instance.GetShilistFromSheng(listsrt[1]));
        GlobalEvent.DispatchEvent(ZaiYunYingWindowEvent.ShoWJianSheItem,ItemCreateControll.Instance.GetShilistFromSheng(listsrt[1]));
    }

    private void Btn1_Event(InputUIOnClickEvent inputEvent)
    {
        GlobalEvent.DispatchEvent(ZaiYunYingWindowEvent.ShoWJianSheItem, ItemCreateControll.Instance.GetShilistFromSheng(listsrt[0]));
        GlobalEvent.DispatchEvent(ZaiYunYingWindowEvent.ShoWYunYingItem, ItemCreateControll.Instance.GetShilistFromSheng(listsrt[0]));
    }
}
