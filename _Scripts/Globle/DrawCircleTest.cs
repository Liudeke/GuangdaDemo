using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircleTest : MonoBehaviour
{

    public Transform targetStart;
    [HideInInspector]
    public  Material mat;
    [HideInInspector]

    public float radius = 1;
    [HideInInspector]

    public int segents = 3;
    List<Vector3> trs=new List<Vector3>();
    List<Transform> TrsList=new List<Transform>();
    // Use this for initialization
    void Awake()
    {
        float[] temFloats = Drawline.Instance.GetFloats();
        radius = temFloats[0];
        segents = (int)temFloats[1];
        mat = Drawline.Instance.GetMaterial();
        if (targetStart == null)
        {
            targetStart = transform.GetChild(0);
        }
        DrawCircle(radius, segents, targetStart.localPosition);
    }
   public void DrawCircle(float radius, int segments, Vector3 centerCircle, float len = 0)
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        gameObject.GetComponent<MeshRenderer>().material = mat;

        //顶点
        Vector3[] vertices = new Vector3[segments + 1];
        vertices[0] = centerCircle;
        float deltaAngle = Mathf.Deg2Rad * 360f / segments;
        float currentAngle = 0;
        for (int i = 1; i < vertices.Length; i++)
        {
            float cosA = Mathf.Cos(currentAngle);
            float sinA = Mathf.Sin(currentAngle);
            vertices[i] = new Vector3(cosA * radius + centerCircle.x, sinA * radius + centerCircle.y, centerCircle.z);
            //GameObject clone = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //clone.transform.SetParent(transform);
            //clone.transform.localPosition = vertices[i];
            //clone.transform.localScale = Vector3.one * 0.1f;
            GameObject clonePoint=new GameObject("point");
            clonePoint.transform.SetParent(transform);
            clonePoint.transform.localPosition = vertices[i];
            trs.Add(clonePoint.transform.localPosition + transform.localPosition);
            TrsList.Add(clonePoint.transform);
            currentAngle += deltaAngle;
        }

        //三角形
        int[] triangles = new int[segments * 3];
        int[] triangles1 = new int[segments * 3];

        for (int i = 0, j = 1; i < segments * 3 - 3; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j;
        }
        for (int i = 0, j = 1; i < segments * 3 - 3; i += 3, j++)
        {
            triangles1[i] = 0;
            triangles1[i + 1] = j;
            triangles1[i + 2] = j + 1;
        }
        triangles[segments * 3 - 3] = 0;
        triangles[segments * 3 - 2] = 1;
        triangles[segments * 3 - 1] = segments;

        triangles1[segments * 3 - 3] = 0;
        triangles1[segments * 3 - 2] = segments;
        triangles1[segments * 3 - 1] = 1;
        List<int> listint = new List<int>();
        for (int i = 0; i < triangles.Length; i++)
        {
            listint.Add(triangles[i]);
        }
        for (int i = 0; i < triangles1.Length; i++)
        {
            listint.Add(triangles1[i]);
        }
        int[] tempint = listint.ToArray();
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = tempint;
    }

    void Update()
    {
       // GetVector3s();
    }
    public List<Vector3> GetVector3s()
    {
        if (trs.Count != 0)
        {
            for (int i = 0; i < trs.Count; i++)
            {
                trs[i] = TrsList[i].localPosition + transform.localPosition;
            }
        }
        return trs;
    }

   
}