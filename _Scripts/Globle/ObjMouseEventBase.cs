using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMouseEventBase : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        OnMouseDownEvent();
    }

    void OnMouseUp()
    {
        OnMouseUpEvent();
    }

    void OnMouseEnter()
    {
        OnMouseEnterEvent();
    }

    void OnMouseExit()
    {
        OnMouseExitEvent();
    }

    public virtual void OnMouseDownEvent()
    {
       // print("OnMouseDown");
    }
    public virtual void OnMouseUpEvent()
    {
        //print("OnMouseUp");
    }
    public virtual void OnMouseEnterEvent()
    {
        //print("OnMouseEnter");
    }
    public virtual void OnMouseExitEvent()
    {
        print("OnMouseExit");
    }
}
