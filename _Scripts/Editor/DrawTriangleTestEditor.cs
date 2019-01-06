using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEditor;

[CanEditMultipleObjects,CustomEditor(typeof(DrawTriangleTest))]
public class DrawTriangleTestEditor : Editor
{
    private DrawTriangleTest targeTriangleTest;

    void OnEnable()
    {
        targeTriangleTest = target as DrawTriangleTest;
    }

    private int size;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        // 绘制全部原有属性
        // base.DrawDefaultInspector();
        // 后面可以扩展自己功能
        //EditorGUILayout.PropertyField(serializedObject.FindProperty("mat"));
        //EditorGUILayout.PropertyField(serializedObject.FindProperty("hgiht"));
        //GUILayout.Space(20);

        //var boxCollider = serializedObject.FindProperty("_BcBoxCollider");
        //EditorGUILayout.PropertyField(boxCollider);
        //GUILayout.Space(20);

        #region /*MyRegion*/

        var elements = serializedObject.FindProperty("listDrawCube");
        if (EditorGUILayout.PropertyField(elements))
        {
            EditorGUI.indentLevel++;
            elements.arraySize = EditorGUILayout.DelayedIntField("Size", elements.arraySize);
            for (int i = 0, size = elements.arraySize; i < size; i++)
            {
                var element = elements.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(element);
            }
            EditorGUI.indentLevel--;
        }
        //var elementsMove = serializedObject.FindProperty("moveTr");
        //if (EditorGUILayout.PropertyField(elementsMove))
        //{
        //    EditorGUI.indentLevel++;
        //    elementsMove.arraySize = EditorGUILayout.DelayedIntField("Size", elementsMove.arraySize);
        //    for (int i = 0, size = elementsMove.arraySize; i < size; i++)
        //    {
        //        var element = elementsMove.GetArrayElementAtIndex(i);
        //        EditorGUILayout.PropertyField(element);
        //    }
        //    EditorGUI.indentLevel--;
        //}

        #endregion
        GUILayout.BeginHorizontal();
        GUIStyle strStyle=new GUIStyle();
        strStyle.fixedWidth = 90;
        if (GUILayout.Button("开始"))
        {
            targeTriangleTest.Init();
        }
        if (GUILayout.Button("恢复"))
        {
           targeTriangleTest.SetZeroObjs();
        }
       // [Space(20)]
        if (GUILayout.Button("HideMesh"))
        {
            targeTriangleTest.HideMeshRanderer();
        }
        GUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
    }
}
