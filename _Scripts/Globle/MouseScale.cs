using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScale : ObjMouseEventBase {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnMouseEnterEvent()
    {
        AnimSystem.Scale(gameObject, new Vector3(1,1,1), new Vector3(1, 3, 1), 0.5f, InterpType.OutQuad);
    }

    public override void OnMouseExitEvent()
    {
        AnimSystem.Scale(gameObject, new Vector3(1, 3, 1), Vector3.one , 0.5f, InterpType.OutQuad);
    }
}
