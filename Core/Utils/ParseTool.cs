using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class ParseTool : MonoBehaviour 
{

	public static Vector2 String2Vector2(string value)
    {
        try
        {
            string[] values = value.Split('|');
            float x = float.Parse(values[0]);
            float y = float.Parse(values[1]);

            return new Vector2(x, y);
        }
        catch (Exception e)
        {
            throw new Exception("ParseVector2: Don't convert value to Vector2 value:" + value + "\n" + e.ToString()); // throw  
        }
    }

    public static Vector3 String2Vector3(string value)
    {
        try
        {
            string[] values = value.Split('|');
            float x = float.Parse(values[0]);
            float y = float.Parse(values[1]);
            float z = float.Parse(values[2]);

            return new Vector3(x, y, z);
        }
        catch (Exception e)
        {
            throw new Exception("ParseVector3: Don't convert value to Vector3 value:" + value + "\n" + e.ToString()); // throw  
        }
    }

    public static Color String2Color(string value)
    {
        try
        {
            string[] values = value.Split('|');
            float r = float.Parse(values[0]);
            float g = float.Parse(values[1]);
            float b = float.Parse(values[2]);
            float a = 1;

            if (values.Length > 3)
            {
                a = float.Parse(values[3]);
            }

            return new Color(r, g, b, a);
        }
        catch (Exception e)
        {
            throw new Exception("ParseColor: Don't convert value to Color value:" + value + "\n" + e.ToString()); // throw  
        }
    }

    static string[] c_NullStringArray = new string[0];

    public static string[] String2StringArray(string value)
    {
        if (value != null
                && value != ""
                && value != "null"
                && value != "Null"
                && value != "NULL"
                && value != "None")
        {
            return value.Split('|');
        }
        else
        {
            return c_NullStringArray;
        }

    }

    public static int[] String2IntArray(string value)
    {
        int[] intArray = null;
        if (!string.IsNullOrEmpty(value))
        {
            string[] strs = value.Split('|');
            intArray = Array.ConvertAll(strs, s => int.Parse(s));
            return intArray;
        }
        else
        {
            return new int[0];
        }
    }

    public static string[] String2StringArrayBaseSplitChar(string value, char splitChar)
    {
        if(!string.IsNullOrEmpty(value))
        {
           return value.Split(splitChar);
        }
        Debug.Log("==>字符串为空，请检查<==");
        return null;
    }

    public static string GetCodeOutPutInfo()
    {
        System.Diagnostics.StackTrace st=new StackTrace(1,true);
        StackFrame sf = st.GetFrame(0);
        string temp = sf.GetFileName();
        if (temp != null && temp.Contains(@"\"))
        {
            int index = temp.LastIndexOf(@"\", StringComparison.Ordinal);
            temp = temp.Remove(0,index+1);
        }
        else
        {
            //Debug.Log("error");
        }

        return "\n" + "\n" + sf.GetFileName() + "\n" + "所在脚本：" + temp + "\n" + "函数名：" + sf.GetMethod().Name + "\n" + "行号：" + sf.GetFileLineNumber();
    }
}
