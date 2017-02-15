using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node
{
	public int posX;
    public int posY;

	int nodeID;

	Node parent;

	float G;
	float H;
	public float getF(){ return G + H;}
    public float Distance(Node End)
	{
		float x = Mathf.Abs(posX - End.posX);
        float y = Mathf.Abs(posY - End.posY);
		return (x + y) * 10;
	}
}

public class Pathfinder : MonoBehaviour 
{
    List<Node> OpenList;
    List<Node> ClosedList;
    List<Vector3> PathToEnd;
    bool InitializedStartandGoal = false;
    bool PathFound;
    Node StartNode;
    Node EndNode;

    public Vector3 StartPos;
    public Vector3 EndPos;

	// Use this for initialization
	void Start ()
    {
	    
	}

    void onValidate()
    {
        InitializedStartandGoal = false;
    }

    public void FindPath(Vector3 startposition, Vector3 endposition)
    {
        for (int i = 0; i < 20; ++i)
        {
            if (!InitializedStartandGoal)
            {
                OpenList.Clear();
                ClosedList.Clear();
                PathToEnd.Clear();
                PathFound = false;
                InitializedStartandGoal = true;
            }

            if (InitializedStartandGoal && !PathFound)
            {
                Grid StartGrid = SharedData.instance.gridmesh.GetGridAtPosition(startposition).GetComponent<Grid>();
                Grid EndGrid = SharedData.instance.gridmesh.GetGridAtPosition(endposition).GetComponent<Grid>();
                StartNode.posX = (int)SharedData.instance.gridmesh.GetGridPosition(StartGrid).x;
                StartNode.posY = (int)SharedData.instance.gridmesh.GetGridPosition(StartGrid).y;
                EndNode.posX = (int)SharedData.instance.gridmesh.GetGridPosition(EndGrid).x;
                EndNode.posY = (int)SharedData.instance.gridmesh.GetGridPosition(EndGrid).y;
                Debug.Log("Start Node: " + StartNode.posX + " " + StartNode.posY + "End Node: " + EndNode.posX + " " + EndNode.posY);
            }
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
