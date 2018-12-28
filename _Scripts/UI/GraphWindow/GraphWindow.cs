﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.UI.GraphWindow
{
	public class GraphWindow : UIWindowBase
	{
	    private GameObject _graph;
	    private GameObject _graph1;
	    private GameObject _graph2;
	    private GameObject _graph3;
	    private GameObject _graph4;
	    private GameObject _graph5;
	    private GameObject _graph6;
	    private GameObject _graph7;
	    public List<Vector3> listVector;
	    public List<Vector3> listOrigin;
        public override void OnInit()
        {
            _graph = GetGameObject("Graph1");
            _graph1 = GetGameObject("Graph1 (1)");
            _graph2 = GetGameObject("Graph1 (2)");
            _graph3 = GetGameObject("Graph1 (3)");
            _graph4 = GetGameObject("Graph1 (4)");
            _graph5 = GetGameObject("Graph1 (5)");
            _graph6 = GetGameObject("Graph1 (6)");
            _graph7 = GetGameObject("Graph1 (7)");
        }

        public override void OnOpen()
		{
			AddOnClickListener("test",btn_Event);
            PlayAnimation();
        }

	    public override void OnHide()
	    {
            PlayBackAnimation();
	    }

	    private void btn_Event(InputUIOnClickEvent inputEvent)
        {
            PlayBackAnimation();
            
        }

        void PlayAnimation()
	    {
	        AnimSystem.Move(_graph, from: _graph.transform.localPosition, to: listVector[0],delayTime:1, time: 1f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph1, from: _graph1.transform.localPosition, to: listVector[1], delayTime: 1, time: 1.2f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph2, from: _graph2.transform.localPosition, to: listVector[2], delayTime: 1, time: 1f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph3, from: _graph3.transform.localPosition, to: listVector[3], delayTime: 1, time: 1.2f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph4, from: _graph4.transform.localPosition, to: listVector[4], delayTime: 1, time: 1f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph5, from: _graph5.transform.localPosition, to: listVector[5], delayTime: 1, time: 1.2f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph6, from: _graph6.transform.localPosition, to: listVector[6], delayTime: 1, time: 1f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph7, from: _graph7.transform.localPosition, to: listVector[7], delayTime: 1, time: 1.2f, interp: InterpType.InOutBack);

        }

	    void PlayBackAnimation()
	    {
	        AnimSystem.Move(_graph, from: _graph.transform.localPosition, to: listOrigin[0],  time: 1f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph1, from: _graph1.transform.localPosition, to: listOrigin[0],  time: 1.2f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph2, from: _graph2.transform.localPosition, to: listOrigin[1],  time: 1f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph3, from: _graph3.transform.localPosition, to: listOrigin[1],  time: 1.2f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph4, from: _graph4.transform.localPosition, to: listOrigin[2],  time: 1f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph5, from: _graph5.transform.localPosition, to: listOrigin[2],  time: 1.2f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph6, from: _graph6.transform.localPosition, to: listOrigin[3], time: 1f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph7, from: _graph7.transform.localPosition, to: listOrigin[3],  time: 1.2f, interp: InterpType.InOutBack);
        }
    }
}
