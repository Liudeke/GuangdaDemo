using System;
using System.Collections;
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
	    public GameObject eathGameObject;
	    public Transform erathMoveTo;
	    public Transform bezierTransform;
        public override void OnInit()
        {
            GameObject uicamera= GameObject.FindGameObjectWithTag("UICamera");
          
            _graph = GetGameObject("Graph1");
            _graph1 = GetGameObject("Graph1 (1)");
            _graph2 = GetGameObject("Graph1 (2)");
            _graph3 = GetGameObject("Graph1 (3)");
            _graph4 = GetGameObject("Graph1 (4)");
            _graph5 = GetGameObject("Graph1 (5)");
            _graph6 = GetGameObject("Graph1 (6)");
            _graph7 = GetGameObject("Graph1 (7)");
            eathGameObject = uicamera.transform.GetChild(2).gameObject;// GetGameObject("Earth");
            erathMoveTo = uicamera.transform.GetChild(0); //GetRectTransform("moveto");
            bezierTransform = uicamera.transform.GetChild(1); // GetRectTransform("bezer");
        }

	    public override IEnumerator ExitAnim(UIAnimCallBack l_animComplete, UICallBack l_callBack, params object[] objs)
	    {
	        AnimSystem.UguiAlpha(gameObject, null, 0, callBack: (object[] obj) =>
	        {
	            StartCoroutine(base.ExitAnim(l_animComplete, l_callBack, objs));
	        });
            ErathPlayBezier();
            PlayBackAnimation();
            yield return new WaitForSeconds(1.3f);
	    }

	    public override IEnumerator EnterAnim(UIAnimCallBack l_animComplete, UICallBack l_callBack, params object[] objs)
	    {
	        PlayAnimation();
            yield return new WaitForSeconds(0.5f);
	    }

	    public override void OnOpen()
		{
			AddOnClickListener("test",btn_Event);
		    //ErathPlayBezier();
		}

	    public override void OnHide()
	    {
            //PlayBackAnimation();
	    }

	    void ErathPlayBezier()
	    {
	        AnimSystem.BezierMove(eathGameObject, 
                erathMoveTo.localPosition,
                new []{bezierTransform.localPosition},
                1, 
                InterpType.OutQuad, 
                RepeatType.Once);
	        AnimSystem.Scale(eathGameObject, from: Vector3.one*11, to: Vector3.one , time: 0.5f,
	            callBack: AniBezierCallBack);
	    }

	    private void AniBezierCallBack(object[] arg)
	    {
	        AnimSystem.Scale(eathGameObject, from: Vector3.one, to: Vector3.one*11 , time: 0.5f
	           );
        }

	    private void btn_Event(InputUIOnClickEvent inputEvent)
        {
            //PlayBackAnimation();
            UIManager.CloseUIWindow(this);
            UIManager.OpenUIWindow<ZaiYunYingWindow>();
        }

        void PlayAnimation()
	    {
	        AnimSystem.Move(_graph, from: _graph.transform.localPosition, to: listVector[0], time: 1f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph1, from: _graph1.transform.localPosition, to: listVector[1],  time: 1.2f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph2, from: _graph2.transform.localPosition, to: listVector[2],  time: 1f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph3, from: _graph3.transform.localPosition, to: listVector[3],  time: 1.2f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph4, from: _graph4.transform.localPosition, to: listVector[4],  time: 1f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph5, from: _graph5.transform.localPosition, to: listVector[5],  time: 1.2f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph6, from: _graph6.transform.localPosition, to: listVector[6],  time: 1f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph7, from: _graph7.transform.localPosition, to: listVector[7],  time: 1.2f, interp: InterpType.InOutBack);

        }

	    void PlayBackAnimation()
	    {
	        AnimSystem.Move(_graph, from: _graph.transform.localPosition, to: listOrigin[0],  time: 0.6f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph1, from: _graph1.transform.localPosition, to: listOrigin[0],  time: 0.6f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph2, from: _graph2.transform.localPosition, to: listOrigin[1],  time: 0.6f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph3, from: _graph3.transform.localPosition, to: listOrigin[1],  time: 0.6f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph4, from: _graph4.transform.localPosition, to: listOrigin[2],  time: 0.6f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph5, from: _graph5.transform.localPosition, to: listOrigin[2],  time: 0.6f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph6, from: _graph6.transform.localPosition, to: listOrigin[3], time: 0.6f, interp: InterpType.InOutBack);
	        AnimSystem.Move(_graph7, from: _graph7.transform.localPosition, to: listOrigin[3],  time: 0.6f, interp: InterpType.InOutBack);
        }
    }
}
