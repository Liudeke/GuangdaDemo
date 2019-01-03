using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircleTest : MonoBehaviour {
    public Material mat;

    public Transform targetStart;
    // Use this for initialization
    void Start ()
    {
        DrawCircle(1, 6, targetStart.localPosition);
       

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        //print("147");
    }
    void DrawCircle(float radius, int segments, Vector3 centerCircle,float len=0)
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
            GameObject clone=GameObject.CreatePrimitive(PrimitiveType.Sphere);
            clone.transform.SetParent(transform);
            clone.transform.localPosition = vertices[i];
            clone.transform.localScale=Vector3.one*0.1f;
            currentAngle += deltaAngle;
        }

        //三角形
        int[] triangles = new int[segments * 3];
        for (int i = 0, j = 1; i < segments * 3 - 3; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j;
        }
        triangles[segments * 3 - 3] = 0;
        triangles[segments * 3 - 2] = 1;
        triangles[segments * 3 - 1] = segments;


        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}
