using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCubeItem : ObjMouseEventBase
{
    List<GameObject> golist = new List<GameObject>();
    List<GameObject> golist1 = new List<GameObject>();
    public List<Transform> trans = new List<Transform>();
    public BoxCollider _BcBoxCollider;
    private List<int> tempListInt;
    public Material mat;
    public List<Transform> moveTr;
    public List<Transform> moveTr1=new List<Transform>();

    // Use this for initialization
    void Start ()
    {
        Init();

    }
    void Init()
    {
        Vector3[] v3s = GetBoxColliderVertexPositions(_BcBoxCollider);
        for (int i = 0; i < v3s.Length; i++)
        {
            GameObject clone = new GameObject(i.ToString()+ i.ToString());
            clone.transform.position = v3s[i];
            clone.transform.SetParent(transform, true);
            golist1.Add(clone);
        }
        for (int i = 4; i < (trans.Count); i++)
        {
            moveTr1.Add(trans[i]);
        }
    }
    // Update is called once per frame
    void Update()
    {
        DrawTriangle1();
    }
    public void Test()
    {
        _BcBoxCollider = transform.GetChild(0).GetComponent<BoxCollider>();
        if (golist.Count != 0)
        {
            foreach (var go in golist)
            {
                DestroyImmediate(go);
            }
            golist.Clear();
        }
        if (GetComponent<BoxCollider>())
        {
            DestroyImmediate(GetComponent<BoxCollider>());
        }
        Vector3[] v3s = GetBoxColliderVertexPositions(_BcBoxCollider);
        for (int i = 0; i < v3s.Length; i++)
        {
            GameObject clone = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            clone.transform.position = v3s[i];
            clone.transform.localScale = Vector3.one * 3;
            clone.transform.SetParent(transform, true);
            trans[i] = clone.transform;
            clone.name = i.ToString();
            golist.Add(clone);
        }
        for (int i = 0; i < moveTr.Count; i++)
        {
            moveTr[i] = golist[i + 4].transform;
        }
        if (!GetComponent<BoxCollider>())
        {
            gameObject.AddComponent<BoxCollider>();
        }
       
        DrawTriangle1();
    }
    void DrawTriangle1()
    {
        if (!gameObject.GetComponent<MeshFilter>())
        {
            gameObject.AddComponent<MeshFilter>();
        }
        if (!gameObject.GetComponent<MeshRenderer>())
        {
            gameObject.AddComponent<MeshRenderer>();
        }

        //  gameObject.AddComponent<MeshRenderer>();
        gameObject.GetComponent<MeshRenderer>().material = mat;
        Mesh mesh=null;
        if (Application.isPlaying)
        {
            mesh = GetComponent<MeshFilter>().mesh;

        }
        else
        {
            mesh = GetComponent<MeshFilter>().sharedMesh;
        }
        mesh.Clear();
        #region MyRegion


        #endregion
        List<Vector3> templistV3 = new List<Vector3>();
        for (int i = 0; i < trans.Count; i++)
        {
            templistV3.Add(trans[i].localPosition);
        }


        Vector3[] tempV3 = templistV3.ToArray();
        //设置顶点
        mesh.vertices = tempV3;//new Vector3[] { temp1[0], temp2[0], temp2[1] };
        tempListInt = new List<int>();
        int number = (tempV3.Length / 2);
        for (int i = 0; i < number; i++)
        {
            if (i == number - 1)
            {
                tempListInt.Add(i);
                tempListInt.Add(i + number);
                tempListInt.Add(number);

                tempListInt.Add(i);
                tempListInt.Add(number);
                tempListInt.Add(0);
            }
            else
            {
                tempListInt.Add(i);
                tempListInt.Add(i + number);
                tempListInt.Add(i + number + 1);

                tempListInt.Add(i);
                tempListInt.Add(i + number + 1);
                tempListInt.Add(i + 1);
            }

        }
        //底部和顶部面
        tempListInt.Add(0);
        tempListInt.Add(0 + 1);
        tempListInt.Add(0 + 2);

        tempListInt.Add(0);
        tempListInt.Add(0 + 2);
        tempListInt.Add(0 + 3);

        tempListInt.Add(number);
        tempListInt.Add(0 + number + 2);
        tempListInt.Add(0 + number + 1);

        tempListInt.Add(number);
        tempListInt.Add(0 + number + 3);
        tempListInt.Add(0 + number + 2);
        //设置三角形顶点顺序，顺时针设置
        mesh.triangles = tempListInt.ToArray();
    }
    Vector3[] GetBoxColliderVertexPositions(BoxCollider boxcollider)
    {
        var vertices = new Vector3[8];
        //下面4个点
        vertices[0] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(boxcollider.size.x, -boxcollider.size.y, boxcollider.size.z) * 0.5f);
        vertices[1] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(-boxcollider.size.x, -boxcollider.size.y, boxcollider.size.z) * 0.5f);
        vertices[2] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(-boxcollider.size.x, -boxcollider.size.y, -boxcollider.size.z) * 0.5f);
        vertices[3] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(boxcollider.size.x, -boxcollider.size.y, -boxcollider.size.z) * 0.5f);
        //上面4个点
        vertices[4] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(boxcollider.size.x, boxcollider.size.y, boxcollider.size.z) * 0.5f);
        vertices[5] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(-boxcollider.size.x, boxcollider.size.y, boxcollider.size.z) * 0.5f);
        vertices[6] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(-boxcollider.size.x, boxcollider.size.y, -boxcollider.size.z) * 0.5f);
        vertices[7] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(boxcollider.size.x, boxcollider.size.y, -boxcollider.size.z) * 0.5f);

        return vertices;
    }
    public override void OnMouseEnterEvent()
    {
        //for (int i = 0; i < moveTr.Count; i++)
        //{
        //    AnimSystem.Move(moveTr[i].gameObject, null, golist[i + 4].transform.localPosition, isLocal: true);
        //}
    }
    void OnMouseExit()
    {
        for (int i = 0; i < moveTr.Count; i++)
        {
          AnimSystem.Move(moveTr1[i].gameObject, null, golist1[i].transform.localPosition, isLocal: true);
        }
    }
    void OnMouseEnter()
    {
        for (int i = 0; i < moveTr.Count; i++)
        {
          AnimSystem.Move(moveTr1[i].gameObject, null, golist1[i + 4].transform.localPosition, isLocal: true);
           //Destroy(golist[i]);
        }

    }

    public void SetZero()
    {
        for (int i = 0; i < moveTr.Count; i++)
        {
            moveTr[i].localPosition = golist[i].transform.localPosition;
        }
        DrawTriangle1();

    }

    private bool IsShow = false;
    public void SetCubeMeshHide()
    {
        if (golist.Count != 0)
        {
            for (int i = 0; i < golist.Count; i++)
            {
                if (!IsShow)
                {
                  golist[i].GetComponent<MeshRenderer>().enabled = false;
                    transform.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    golist[i].GetComponent<MeshRenderer>().enabled = true;
                    transform.GetChild(0).gameObject.SetActive(true);

                }
            }
        }
        IsShow = !IsShow;

    }
    public override void OnMouseExitEvent()
    {
        //for (int i = 0; i < moveTr.Count; i++)
        //{
        //    AnimSystem.Move(moveTr[i].gameObject, null, golist[i].transform.localPosition, isLocal: true);
        //}
    }
}
