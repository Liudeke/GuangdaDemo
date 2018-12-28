using UnityEngine;
using System.Collections;
using _Scripts.UI.GraphWindow;

public class HotUpdateStatus : IApplicationStatus
{

    public override void OnEnterStatus()
    {
        OpenUI<GraphWindow>();
    }
}
