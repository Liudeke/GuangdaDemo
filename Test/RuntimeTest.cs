using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;
using UniRx;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;

public class RuntimeTest : MonoBehaviour 
{
	Dictionary<string, SingleField> dic=new Dictionary<string, SingleField>();

    void Awake()
    {
        ResourcesConfigManager.Initialize();

    }
    void Start ()
	{
	    string IOTest = "旺财";
        //ResourceIOTool.CreateFile(Application.persistentDataPath+"/IOTest.test",System.Text.Encoding.UTF8.GetBytes(IOTest));
        //HotUpdateManager.StartHotUpdate(HotEventCall);
        //print(Application.persistentDataPath);
        //StartCoroutine(LoadAb());

    }

    

    // Update is called once per frame
    void Update () 
    {
	    if(Input.GetKey(KeyCode.A))
        {
            //GameObject testTmp = (GameObject)ResourceManager.Load("GameObject");

            //Instantiate(testTmp);
            //HotUpdateManager.StartHotUpdate(CallBackEvent);
           
        }

        if(Input.GetKey(KeyCode.B))
        {
            GameObject testTmp = (GameObject)ResourceManager.Load("UItest");

            Instantiate(testTmp);
        }

        if (Input.GetKey(KeyCode.U))
        {
            UIManager.OpenUIWindow("MianMenu");
        }

        if (Input.GetKey(KeyCode.I))
        {
            UIManager.CloseUIWindow("MianMenu");
        }

        if (Input.GetKey(KeyCode.C))
        {
            AssetsBundleManager.UnLoadBundle("UItest");
        }

        

        if (Input.GetKey(KeyCode.D))
        {
            loadCount++;
            ResourceManager.LoadAsync("UItest", (LoadState state, object obj) => 
            {
                if (state.isDone)
                {
                    callbackCount++;
                    Debug.Log(state.progress);
                    GameObject go = (GameObject)obj;

                    Instantiate(go);

                    Debug.Log(loadCount+"  " + callbackCount);
                }
                else
                {
                    Debug.Log(state.progress);
                }
            });
        }
	}

    //private void CallBackEvent(HotUpdateStatusInfo info)
    //{
    //   // throw new NotImplementedException();
    //}

    public Texture2D Texture2D;
    IEnumerator LoadAb()
    {
        UnityWebRequest request = UnityWebRequest.GetAssetBundle("http://localhost:11575/Version.assetBundle");
        yield return request.SendWebRequest();
        AssetBundle ab = ((DownloadHandlerAssetBundle)request.downloadHandler).assetBundle;
        TextAsset tex = (TextAsset) ab.mainAsset;

        print(tex.text);
        //GameObject go = ab.LoadAsset<GameObject>("shouhuoji");
        // Instantiate(go);

        //UnityWebRequest request = UnityWebRequestTexture.GetTexture("http://localhost/test33.png");
        //yield return request.SendWebRequest();




    }
    int loadCount = 0;
    int callbackCount = 0;
}
