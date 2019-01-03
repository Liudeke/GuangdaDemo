using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

[System.Serializable]
public class LineInfo
{
    public bool IsUpdate = false;
    public Transform StarTransform;
    public Transform EndTransform;
    public Texture lineTexture;
    public float lineWidth = 8.0f;
    public float textureScale = 1.0f;
    private VectorLine line;
    public void SetLine()
    {
        var linepoints3 = new List<Vector3>();
        linepoints3.Add(StarTransform.position);
        linepoints3.Add(EndTransform.position);
        // Make a VectorLine object using the above points, with the texture as specified in the inspector, and set the texture scale
        line = new VectorLine("Line", linepoints3, lineTexture, lineWidth);
        line.textureScale = textureScale;
        //line.points3 = linepoints3;
        // Draw the line
        line.Draw3D();
    }
    public void Update()
    {
        if (IsUpdate)
        {
            line.points3[0] = StarTransform.position;
            line.points3[1] = EndTransform.position;
            line.Draw3D();
        }
        

    }
}
//[RequireComponent(typeof(LineRenderer))]
public class Drawline : MonoBehaviour
{

    public List<LineInfo> listinfo;
    public int length;
    public float rightPos;
    public float leftPos;
    public MeshFilter mesh;
    void Start ()
	{
	    if (listinfo.Count != 0)
	    {
	        for (int i = 0; i < listinfo.Count; i++)
	        {
	            listinfo[i].SetLine();
	        }
	    }
        UpdateMesh(mesh.mesh,20,20,0.1f,8);
    }
	
	// Update is called once per frame
	void Update () {
	    if (listinfo.Count != 0)
	    {
	        for (int i = 0; i < listinfo.Count; i++)
	        {
	            listinfo[i].Update();
	        }
	    }
    }
    //圆柱体是由两个圆和一个长方形组成的  先输入长方形的顶点 然后在输入圆顶点
    private void UpdateMesh(Mesh mesh, int edg_x, int edg_y, float rad, float len)
    {
        edg_x = Mathf.Max(2, edg_x);//保证最低2个边
        edg_y = Mathf.Max(2, edg_y);
        int _deglen = (edg_x + 1) * edg_y;//长方体
        int totalcount = _deglen + (1 + edg_x + 1) * 2; //加两个圆


        Vector3[] normals = new Vector3[totalcount];
        Vector3[] verts = new Vector3[totalcount];
        Vector2[] uvs = new Vector2[totalcount];
        int[] trians = new int[edg_x * edg_y * 6];
        float reg = 6.28318f / edg_x;
        float _len = len / (edg_y - 1);



        for (int y = 0; y < edg_y; y++)
            for (int x = 0; x < edg_x + 1; x++)//多一个边来保存UV值
            {
                int i = x + y * (edg_x + 1);
                verts[i] = new Vector3(Mathf.Sin((reg * (x % edg_x)) % 6.28318f) * rad, Mathf.Cos((reg * (x % edg_x)) % 6.28318f) * rad, rightPos + y * _len);//计算顶点坐标
                normals[i] = new Vector3(verts[i].x, verts[i].y, 0);//计算法线方向
                int id = x % (edg_x + 1) * 6 + y * edg_x * 6;
                if (x < edg_x + 1 && y < edg_y - 1 && (id + 5) < trians.Length)//计算顶点数组
                {
                    if (length > 0)
                    {
                        trians[id] = i;
                        trians[id + 1] = trians[id + 4] = i + edg_x + 1;
                        trians[id + 2] = trians[id + 3] = i + 1;
                        trians[id + 5] = i + edg_x + 2;
                    }
                    else
                    {
                        trians[id] = i;
                        trians[id + 1] = trians[id + 3] = i + 1;
                        trians[id + 2] = trians[id + 5] = i + edg_x + 1;
                        trians[id + 4] = i + edg_x + 2;
                    }

                }
                //if (edg_x != 2)//计算UV，考虑到2个边的情况
                //    uvs[i] = new Vector2(x == edg_x ? 1f : quaduvStep.x * x, y == edg_y - 1 ? (2*rad+len)/totalLen : quaduvStep.y * y);
                //else
                //    uvs[i] = new Vector2(x % edg_x, y == edg_y - 1 ? (2 * rad + len) / totalLen : quaduvStep.y * y);
            }

        int maxId = edg_x * (edg_y - 1) * 6;
        verts[_deglen] = new Vector3(0, 0, rightPos);

        normals[_deglen] = -Vector3.forward;

        //uvs[_deglen] = new Vector2(0.5f, (rad) / totalLen);
        //原点一面
        for (int x = 0; x < edg_x + 1; x++)
        {
            verts[_deglen + 1 + x] = new Vector3(Mathf.Sin((reg * (x % edg_x)) % 6.28318f) * rad, Mathf.Cos((reg * (x % edg_x)) % 6.28318f) * rad, rightPos);
            normals[_deglen + 1 + x] = -Vector3.forward;
            if (x == edg_x) continue;

            if (length > 0)
            {
                trians[3 * x + maxId] = _deglen;
                trians[3 * x + 1 + maxId] = _deglen + 1 + x;
                trians[3 * x + 2 + maxId] = _deglen + 2 + x;
            }
            else
            {
                trians[3 * x + maxId] = _deglen;
                trians[3 * x + 1 + maxId] = _deglen + 2 + x;
                trians[3 * x + 2 + maxId] = _deglen + 1 + x;
            }
        }


        //远点一面
        maxId += 3 * edg_x;
        verts[_deglen + 2 + edg_x] = new Vector3(0, 0, leftPos);
        normals[_deglen + 2 + edg_x] = Vector3.forward;
        //uvs[_deglen + 1] = new Vector2(0.5f, (3 * rad + len) / totalLen);

        for (int x = 0; x < edg_x + 1; x++)
        {
            verts[1 + x + edg_x + 2 + _deglen] = new Vector3(Mathf.Sin((reg * (x % edg_x)) % 6.28318f) * rad, Mathf.Cos((reg * (x % edg_x)) % 6.28318f) * rad, leftPos);
            normals[1 + x + edg_x + 2 + _deglen] = Vector3.forward;
            if (x == edg_x) continue;
            if (length > 0)
            {
                trians[3 * x + maxId] = _deglen + 2 + edg_x;
                trians[3 * x + 1 + maxId] = _deglen + 2 + edg_x + x + 2;
                trians[3 * x + 2 + maxId] = _deglen + 2 + edg_x + x + 1;
            }
            else
            {
                trians[3 * x + maxId] = _deglen + 2 + edg_x;
                trians[3 * x + 1 + maxId] = _deglen + 2 + edg_x + x + 1;
                trians[3 * x + 2 + maxId] = _deglen + 2 + edg_x + x + 2;
            }
        }
        mesh.Clear();
        mesh.vertices = verts;
        mesh.triangles = trians;
        //mesh.uv = uvs;
        mesh.normals = normals;
        
        mesh.RecalculateBounds();
    }
}
