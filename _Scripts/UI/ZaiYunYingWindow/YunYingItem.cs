using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum YunYingItemEvent
{
    AddObj,
    AddObjJianShe
}
public class YunYingItem : ReusingScrollItemBase {

    public List<RectTransform> listItemGameObjects=new List<RectTransform>();
    public List<RectTransform> listTargetList=new List<RectTransform>();
    List<RectTransform> tempListObj=new List<RectTransform>();
    private GameObject cloneGameObject=null;
    public override void OnInit()
    {
        listItemGameObjects.Clear();
        listTargetList.Clear();
        tempListObj.Clear();
        RectTransform rect = GetRectTransform("YunYIngItem");
        AddItemsToList(rect);

    }

    private List<string> tempList;
    public override void SetContent(int index, Dictionary<string, object> data)
    {
        string NameID="";
        //tempList = (List<string>)data["list"];
        object obj;
        if (data.TryGetValue("JianShe_list", out obj))
        {

            tempList = obj as List<string>;
            NameID = "JianShe_list";
        }
        else if(data.TryGetValue("list", out obj))
        {
            tempList = obj as List<string>;
            NameID = "list";
        }
        for (int i = 0; i < tempList.Count; i++)
        {
            GameObject clone = GameObjectManager.CreateGameObjectByPool("YunYingImgItem");
            if (NameID == "JianShe_list")
            {
              GlobalEvent.DispatchEvent(YunYingItemEvent.AddObjJianShe, clone);
            }
            else if(NameID == "list")
            {
              GlobalEvent.DispatchEvent(YunYingItemEvent.AddObj, clone);
            }
            RectTransform clonerect = clone.GetComponent<RectTransform>();
            tempListObj.Add(clonerect);
            clone.GetComponent<test>().SetTextForChild(tempList[i]+"|"+ NameID);
            clonerect.SetParent(listTargetList[i].parent);
            clonerect.localPosition = Vector3.zero;
            clonerect.localScale = Vector3.one;
        }
        #region MyRegion
        //for (int i = 0; i < listItemGameObjects.Count; i++)
        //{
        //    listItemGameObjects[i].localPosition = listItemGameObjects[i].localPosition + new Vector3(0, 77 * index, 0);
        //}
        //for (int i = 0; i < tempListObj.Count; i++)
        //{
        //    tempListObj[i].localPosition = listItemGameObjects[i].localPosition;
        //}
        //for (int i = 0; i < tempListObj.Count; i++)
        //{
        //    tempListObj[i].gameObject.GetComponent<test>().TargeTransform = listTargetList[i];
        //}
        #endregion

    }

   

    void AddItemsToList(RectTransform rect)
    {
        for (int i = 0; i < rect.childCount; i++)
        {
            listItemGameObjects.Add(rect.GetChild(i).GetChild(1).GetComponent<RectTransform>());
            listTargetList.Add(rect.GetChild(i).GetChild(0).GetComponent<RectTransform>());
        }
    }

    public override void OnShow()
    {
        print("Show--"+m_index);
    }

    public override void OnHide()
    {
       
    }
}
