using System;
using UnityEngine;

//MyDataGenerate类
//该类自动生成请勿修改，以避免不必要的损失
public class MyDataGenerate : DataGenerateBase 
{
	public string m_key;

	public override void LoadData(string key) 
	{
		DataTable table =  DataManager.GetData("MyData");

		if (!table.ContainsKey(key))
		{
			throw new Exception("MyDataGenerate LoadData Exception Not Fond key ->" + key + "<-");
		}

		SingleData data = table[key];

		m_key = key;
	}
}
