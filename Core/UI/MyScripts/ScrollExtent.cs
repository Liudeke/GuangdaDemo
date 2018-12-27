using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollExtent : MonoBehaviour
{
    public ScrollRect scroll;
    public string cloneName;
    public int verticalNumber=1;
    public int hiroticalNumber=1;

    public int AllItemNamber;
    void Start ()
    {
        CreatItem();

    }
	
	
	void Update () {
		
	}

    public void CreatItem()
    {
        int temp = GetVerticalnumber();
        verticalNumber = temp;
        for (int i = 0; i < hiroticalNumber; i++)
        {
            for (int j = 0; j < temp; j++)
            {
                ScrollViewExtend(i, j);
            }
        }
    }

    int GetVerticalnumber()
    {
        int temp;
        if (AllItemNamber% hiroticalNumber == 0)
        {
            temp = AllItemNamber/ hiroticalNumber;
        }
        else
        {
            temp = (AllItemNamber/ hiroticalNumber) +1;
        }
        return temp;
    }
    //protected void SetContentAnchoredPosition()
    //{
    //    base.SetLayoutHorizontal();
    //}
    private void ScrollViewExtend(int vritical,int hritical)
    {
        //RectTransform rectTransform = clone.GetComponent<RectTransform>();
        RectTransform rectTransform = ResourceManager.Load<GameObject>(cloneName).GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.Log(cloneName + "---此物体没有RectTransform组件，请检查！！");
            return ;
        }
        float wight = rectTransform.rect.width;
        float height = rectTransform.rect.height;
        scroll.content.sizeDelta = new Vector2(scroll.content.sizeDelta.x + 0, scroll.content.sizeDelta.y + height);
         GameObject tempGameObject = Instantiate<GameObject>(rectTransform.gameObject);
        tempGameObject.GetComponent<RectTransform>().SetParent(scroll.content);
        //print(scroll.content.childCount);
        if (scroll.content.childCount == 1)
        {
            Debug.Log((wight / 2) + "---ver");
            tempGameObject.GetComponent<RectTransform>().localPosition = new Vector2((wight / 2) * (hritical + 1), -height / 2 * (vritical + 1));
        }
        else
        {
           // rectTransform.localPosition = new Vector2(wight / 2 * (hritical + 1), -(height * (scroll.content.childCount) - height / 2) * (vritical + 1));
        }
        if ((vritical + 1) == 1 && scroll.content.childCount >= 1)
        {
            Debug.Log((vritical + 1) + "---ver");

            tempGameObject.GetComponent<RectTransform>().localPosition = new Vector2((wight / 2) * (hritical + 1)+ wight / 2, -height / 2 * (vritical + 1));
        }
        // if (!tempGameObject.activeInHierarchy) tempGameObject.SetActive(true);
        //return tempGameObject;
    }
}
