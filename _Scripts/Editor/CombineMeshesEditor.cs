using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CombineMeshesEditor : Editor {

    [MenuItem("Tools/合并网格")]
    static void combineMesh()
    {
        GameObject obj = Selection.activeGameObject;
        MeshRenderer[] meshRenders= obj.transform.GetComponentsInChildren<MeshRenderer>();
        MeshFilter[] meshFilters = obj.transform.GetComponentsInChildren<MeshFilter>();
        MyStart(obj, meshRenders, meshFilters);
        Debug.Log("合并成功");
    }

    static void MyStart(GameObject go, MeshRenderer[] meshrender, MeshFilter[] _meshFilters)
    {
        //获取MeshRender;
        MeshRenderer[] meshRenders = meshrender;

        //材质;
        Material[] mats = new Material[meshRenders.Length];
        for (int i = 0; i < meshRenders.Length; i++)
        {
            mats[i] = meshRenders[i].sharedMaterial;
        }

        //合并Mesh;
        MeshFilter[] meshFilters = _meshFilters;

        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
        }

        MeshRenderer mr = go.AddComponent<MeshRenderer>();
        MeshFilter mf = go.AddComponent<MeshFilter>();
        mf.mesh = new Mesh();
        mf.mesh.CombineMeshes(combine, false);
        go.SetActive(true);
        mr.sharedMaterials = mats;
    }
    [MenuItem("Tools/给多物体添加BoxCollider")]
    static void Test()
    {
        Transform parent = Selection.activeGameObject.transform;
        Vector3 postion = parent.position;
        Quaternion rotation = parent.rotation;
        Vector3 scale = parent.localScale;
        parent.position = Vector3.zero;
        parent.rotation = Quaternion.Euler(Vector3.zero);
        parent.localScale = Vector3.one;

        Collider[] colliders = parent.GetComponentsInChildren<Collider>();
        foreach (Collider child in colliders)
        {
            DestroyImmediate(child);
        }
        Vector3 center = Vector3.zero;
        Renderer[] renders = parent.GetComponentsInChildren<Renderer>();
        foreach (Renderer child in renders)
        {
            center += child.bounds.center;
        }
        center /= parent.GetComponentsInChildren<Renderer>().Length;
        Bounds bounds = new Bounds(center, Vector3.zero);
        foreach (Renderer child in renders)
        {
            bounds.Encapsulate(child.bounds);
        }
        BoxCollider boxCollider = parent.gameObject.AddComponent<BoxCollider>();
        boxCollider.center = bounds.center - parent.position;
        boxCollider.size = bounds.size;

        parent.position = postion;
        parent.rotation = rotation;
        parent.localScale = scale;
    }
}
