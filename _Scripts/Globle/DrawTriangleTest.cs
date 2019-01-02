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

public class DrawTriangleTest : MonoBehaviour
{

    public Material mat;

    public PlaneState planeState;
    //public Vector3 v3;
    public Vector3 Addv3;
   // public Transform trTemp;
    public List<Transform> moveTr;
    public List<Vector3> moveTemp=new List<Vector3>();
    public List<Transform> trans = new List<Transform>();

    // Use this for initialization
    void Start()
    {
       // v3 = trTemp.localPosition;
        for (int i = 0; i < moveTr.Count; i++)
        {
            moveTemp[i] = moveTr[i].localPosition;
        }
        switch (planeState)
        {
            case PlaneState.Triangle:
                DrawTriangle1();
                break;
            case PlaneState.Square:
                DrawSquare();
                break;
            case PlaneState.Circle:
                DrawCircle(2, 50, Vector3.zero);
                break;
            case PlaneState.Ring:
                DrawRing(2, 3, 50, Vector3.zero);
                break;
        }
    }

    #region 画三角形
    void DrawTriangle()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        gameObject.GetComponent<MeshRenderer>().material = mat;

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        //设置顶点
        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0) };
        //设置三角形顶点顺序，顺时针设置
        mesh.triangles = new int[] { 0, 1, 2 };
    }

    void Update()
    {
        for (int i = 0; i < moveTr.Count; i++)
        {
            moveTr[i].localPosition = Vector3.Lerp(moveTr[i].localPosition, moveTemp[i] + Addv3, 0.3f);
        }
        DrawTriangle1();
    }

    public void SetVector(float x, float y, float z)
    {
        Addv3 =new Vector3(x,y,z);
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

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        //设置顶点
        mesh.vertices = new Vector3[] {
            trans[0].localPosition,
            trans[1].localPosition,
            trans[2].localPosition,
            trans[3].localPosition,
            trans[4].localPosition,
            trans[5].localPosition,
            trans[6].localPosition,
            trans[7].localPosition
        };
        //设置三角形顶点顺序，顺时针设置
        mesh.triangles = new int[]
        {
            0,1,2,0,2,1,0,2,3,0,3,2,
            0,4,3,0,3,4,0,4,7,0,7,4,
            0,1,6,0,6,1,0,6,7,0,7,6,
            5,2,4,5,4,2,2,3,4,2,4,3,
            5,1,2,5,2,1,5,1,6,5,6,1,
            5,7,4,5,4,7,5,6,7,5,7,6
            
        };
    }
    #endregion

    #region 画正方形
    void DrawSquare()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        gameObject.GetComponent<MeshRenderer>().material = mat;

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0), new Vector3(1, 0, 0) };
        mesh.triangles = new int[]
        { 0, 1, 2,
          0, 2, 3
        };
    }
    #endregion

    #region 画圆
    /// <summary>
    /// 画圆
    /// </summary>
    /// <param name="radius">圆的半径</param>
    /// <param name="segments">圆的分割数</param>
    /// <param name="centerCircle">圆心得位置</param>
    void DrawCircle(float radius, int segments, Vector3 centerCircle)
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
            vertices[i] = new Vector3(cosA * radius + centerCircle.x, sinA * radius + centerCircle.y, 0);
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
    #endregion

    #region 画圆环
    /// <summary>
    /// 画圆环
    /// </summary>
    /// <param name="radius">圆半径</param>
    /// <param name="innerRadius">内圆半径</param>
    /// <param name="segments">圆的分个数</param>
    /// <param name="centerCircle">圆心坐标</param>
    void DrawRing(float radius, float innerRadius, int segments, Vector3 centerCircle)
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        gameObject.GetComponent<MeshRenderer>().material = mat;

        //顶点
        Vector3[] vertices = new Vector3[segments * 2];
        float deltaAngle = Mathf.Deg2Rad * 360f / segments;
        float currentAngle = 0;
        for (int i = 0; i < vertices.Length; i += 2)
        {
            float cosA = Mathf.Cos(currentAngle);
            float sinA = Mathf.Sin(currentAngle);
            vertices[i] = new Vector3(cosA * innerRadius + centerCircle.x, sinA * innerRadius + centerCircle.y, 0);
            vertices[i + 1] = new Vector3(cosA * radius + centerCircle.x, sinA * radius + centerCircle.y, 0);
            currentAngle += deltaAngle;
        }

        //三角形
        int[] triangles = new int[segments * 6];
        for (int i = 0, j = 0; i < segments * 6; i += 6, j += 2)
        {
            triangles[i] = j;
            triangles[i + 1] = (j + 1) % vertices.Length;
            triangles[i + 2] = (j + 3) % vertices.Length;

            triangles[i + 3] = j;
            triangles[i + 4] = (j + 3) % vertices.Length;
            triangles[i + 5] = (j + 2) % vertices.Length;
        }

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
    #endregion
}