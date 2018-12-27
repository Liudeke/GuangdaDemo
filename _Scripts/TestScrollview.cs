using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScrollview : MonoBehaviour {

    public ScrollRect scrollrect;
    public RectTransform childsize;
	void Start () {

		//Bounds
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    int index = 5;
    public void AddYSize()
    {
        index += 5;
        scrollrect.content.sizeDelta = new Vector2(index, 0);
    }
    public void AddYSize1()
    {
        index += 5;
        childsize.localPosition = new Vector3(index, childsize.localPosition.y,0);
    }
}
