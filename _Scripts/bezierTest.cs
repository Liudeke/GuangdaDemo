using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bezierTest : MonoBehaviour
{

    //public Transform pos1;
    //public Transform pos2;
    //public Transform pos3;

    //public Transform moveto;
    //private Vector3[] v3 = new Vector3[2] ;
    //public float bezerFloat = 2;
    //float[] floatlist=new float[2];

    public GameObject poolObj;

   
    void Start ()
    {
        //v3[0] = pos1.position;
        //v3[1] = pos2.position;
        //floatlist[0] = bezerFloat;
        //floatlist[1] = bezerFloat;
        ////v3[2] = pos3.position;
        //AnimSystem.BezierMove(gameObject, moveto.position, 5,RepeatType.Once,t_Bezier_contralRadius:floatlist,bezierMoveType:PathType.Bezier2 );
        for (int i = 0; i < 5; i++)
        {
         GameObjectManager.CreateGameObject(poolObj);

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
