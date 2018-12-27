using UnityEngine;
using System.Collections;
using System;

public class UItest : IApplicationStatus 
{
    public override void OnEnterStatus()
    {
        UIManager.OpenUIWindow<ZaiYunYingWindow>();
     // UIManager.OpenUIWindow<ShopWindow>();

    }

    
}
