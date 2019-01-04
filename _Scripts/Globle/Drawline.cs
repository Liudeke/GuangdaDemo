using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Vectrosity;


public class Drawline : MonoBehaviour
{
    public Material mat;
    [Header("半径")]
    public float radius = 1;

    [Header("面数")]
    [Range(3, 100)]
    public int segents = 3;
    public List<DrawCircleTest> list;
    List<Vector3> temp1 = new List<Vector3>();
    List<Vector3> temp2 = new List<Vector3>();
    List<int> tempListInt;
    public static Drawline Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        InitInfo();
    }

#if UNITY_EDITOR
    public void Test()
    {
        print("Test");
    }
#endif
    public float[] GetFloats()
    {
        float[] tempFloats = new[] {radius, segents};
        return tempFloats;
    }

    public Material GetMaterial()
    {
        return mat;
    }
    void InitInfo()
    {
        if (list.Count != 0)
        {
            temp1 = list[0].GetVector3s();
            temp2 = list[1].GetVector3s();
        }
        if (Application.isPlaying)
        {
            
        }
        DrawTriangle();
    }
   
    // Update is called once per frame
    void Update()
    {
        if (list.Count != 0)
        {
            temp1 = list[0].GetVector3s();
            temp2 = list[1].GetVector3s();
        }
        DrawTriangle();
    }

    void DrawTriangle()
    {
        if (!gameObject.GetComponent<MeshFilter>())
        {
            gameObject.AddComponent<MeshFilter>();
        }
        if (!gameObject.GetComponent<MeshRenderer>())
        {
            gameObject.AddComponent<MeshRenderer>();
        }

        gameObject.GetComponent<MeshRenderer>().material = mat;

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        //print(temp1.Count);
        List<Vector3> templistV3 = new List<Vector3>();
        for (int i = 0; i < temp1.Count; i++)
        {
            templistV3.Add(temp1[i]);
        }
        for (int i = 0; i < temp2.Count; i++)
        {
            templistV3.Add(temp2[i]);
        }

        Vector3[] tempV3 = templistV3.ToArray();
        //设置顶点
        mesh.vertices = tempV3;//new Vector3[] { temp1[0], temp2[0], temp2[1] };
        tempListInt=new List<int>();
        int number = (tempV3.Length / 2);
        for (int i = 0; i <number ; i++)
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
        //设置三角形顶点顺序，顺时针设置
        mesh.triangles = tempListInt.ToArray(); 
    }
   
   
}