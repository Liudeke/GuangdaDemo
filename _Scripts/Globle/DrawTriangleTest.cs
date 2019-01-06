using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlaneState
{
    Triangle,
    Square,
    Circle,
    Ring
}

public class DrawTriangleTest : ObjMouseEventBase
{
    public List<DrawCubeItem> listDrawCube = new List<DrawCubeItem>();
    void Start()
    {
        #region MyRegion

        //// v3 = trTemp.localPosition;
        // for (int i = 0; i < moveTr.Count; i++)
        // {
        //     moveTemp[i] = moveTr[i].localPosition;
        // }
        // switch (planeState)
        // {
        //     case PlaneState.Triangle:
        //         DrawTriangle1();
        //         break;
        //     case PlaneState.Square:
        //         DrawSquare();
        //         break;
        //     case PlaneState.Circle:
        //         DrawCircle(2, 50, Vector3.zero);
        //         break;
        //     case PlaneState.Ring:
        //         DrawRing(2, 3, 50, Vector3.zero);
        //         break;
        // }

        #endregion

       
    }

    public void Init()
    {
        if (listDrawCube.Count != 0)
        {
            listDrawCube.Clear();
        }
        foreach (Transform item in transform)
        {
            listDrawCube.Add(item.GetComponent<DrawCubeItem>());
        }
        foreach (DrawCubeItem item in listDrawCube)
        {
            item.Test();
        }
    }

    public void SetZeroObjs()
    {
        foreach (DrawCubeItem item in listDrawCube)
        {
            item.SetZero();
        }
    }

    public void HideMeshRanderer()
    {
        foreach (DrawCubeItem item in listDrawCube)
        {
            item.SetCubeMeshHide();
        }
    }
   
    
}
