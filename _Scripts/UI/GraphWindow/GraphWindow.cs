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
           
        }

	    public GameObject obj;
	    void Start()
	    {
	        _graph = obj;//GetGameObject("Graph1");
            //_graph1 = GetGameObject("Graph1 (1)");
            //_graph2 = GetGameObject("Graph1 (2)");
            //_graph3 = GetGameObject("Graph1 (3)");
            //_graph4 = GetGameObject("Graph1 (4)");
            //_graph5 = GetGameObject("Graph1 (5)");
            //_graph6 = GetGameObject("Graph1 (6)");
            //_graph7 = GetGameObject("Graph1 (7)");
            PlayAnimation();
        }
        public override void OnOpen()
		{
			
		}

	    void PlayAnimation()
	    {
	        AnimSystem.Move(_graph, from: _graph.transform.localPosition, to: listVector[0],delayTime:2, time: 1.5f, interp: InterpType.Linear);
	    }
	}
}
