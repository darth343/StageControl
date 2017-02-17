using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node
{
	public int posX;
    public int posY;

	public int GetNodeID()
    {
        return posX + posY * 5000;
    }

    public Node parent = null;

    // Distance from Start Node
    public float G;
    // Distance from End Node
    public float H;
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
    List<Node> OpenList = new List<Node>();
    List<Node> VisitedList = new List<Node>();
    List<Vector3> PathToEnd = new List<Vector3>();
    bool InitializedStartandGoal = false;
    Node StartNode = new Node();
    Node EndNode = new Node();

    public bool PathFound = true;
    public Vector3 StartPos = new Vector3();
    public Vector3 EndPos = new Vector3();

    float ResetTimer = 0f;

	// Use this for initialization
	void Start ()
    {
	    
	}

    void OnValidate()
    {
    }

    public void Reset()
    {
        PathFound = false;
        InitializedStartandGoal = false;

        foreach (Node node in OpenList)
        {
            Grid tempGrid = SharedData.instance.gridmesh.gridmesh[node.posX, node.posY].GetComponent<Grid>();
            tempGrid.GetComponent<Grid>().ChangeState(Grid.GRID_STATE.AVAILABLE);
        }

        foreach (Node node in VisitedList)
        {
            Grid tempGrid = SharedData.instance.gridmesh.gridmesh[node.posX, node.posY].GetComponent<Grid>();
            tempGrid.GetComponent<Grid>().ChangeState(Grid.GRID_STATE.AVAILABLE);
        }
    }

    public void FindPath(Vector3 startposition, Vector3 endposition)
    {
        for (int i = 0; i < 10; ++i)
        {
            if (!InitializedStartandGoal)
            {
                OpenList.Clear();
                VisitedList.Clear();
                PathToEnd.Clear();
                PathFound = false;
            }

            if (!InitializedStartandGoal && !PathFound)
            {
                Grid StartGrid = SharedData.instance.gridmesh.GetGridAtPosition(startposition).GetComponent<Grid>();
                Grid EndGrid = SharedData.instance.gridmesh.GetGridAtPosition(endposition).GetComponent<Grid>();
                StartNode.posX = (int)SharedData.instance.gridmesh.GetGridPosition(StartGrid).x;
                StartNode.posY = (int)SharedData.instance.gridmesh.GetGridPosition(StartGrid).y;
                StartNode.G = 0;
                StartNode.H = StartNode.Distance(EndNode);
                EndNode.posX = (int)SharedData.instance.gridmesh.GetGridPosition(EndGrid).x;
                EndNode.posY = (int)SharedData.instance.gridmesh.GetGridPosition(EndGrid).y;
                Debug.Log("END NODE POS: " + EndNode.posX + ", " + EndNode.posY);
                EndNode.G = EndNode.Distance(StartNode);
                EndNode.H = 0;
                OpenList.Add(StartNode);
                InitializedStartandGoal = true;
            }
            if (InitializedStartandGoal && !PathFound)
            {
                ContinueSearch();
            }
        }
    }

    void ContinueSearch()
    {
        if (OpenList.Count == 0)
        {
            InitializedStartandGoal = false;
            PathFound = true;
            return;
        }

        Node currentNode = getNextNodeFromOpenList();

        if (currentNode != null)
        {
            if (currentNode.GetNodeID() == EndNode.GetNodeID())
            {
                Debug.Log("Found");
                PathFound = true;
                Node getPath;
                for (getPath = currentNode; getPath != null; getPath = getPath.parent)
                {
                    SharedData.instance.gridmesh.gridmesh[getPath.posX, getPath.posY].GetComponent<Grid>().ChangeState(Grid.GRID_STATE.ISPATH);
                    PathToEnd.Add(new Vector3(getPath.posX * SharedData.instance.gridmesh.GridSizeX, 1, getPath.posY * SharedData.instance.gridmesh.GridSizeZ));
                }
            }
            else 
            {
                OpenNode(currentNode.posX + 1, currentNode.posY, 10, currentNode);
                OpenNode(currentNode.posX - 1, currentNode.posY, 10, currentNode);
                OpenNode(currentNode.posX, currentNode.posY + 1, 10, currentNode);
                OpenNode(currentNode.posX, currentNode.posY - 1, 10, currentNode);

                OpenNode(currentNode.posX - 1, currentNode.posY - 1, 14, currentNode);
                OpenNode(currentNode.posX - 1, currentNode.posY + 1, 14, currentNode);
                OpenNode(currentNode.posX + 1, currentNode.posY + 1, 14, currentNode);
                OpenNode(currentNode.posX + 1, currentNode.posY - 1, 14, currentNode);
            }
        }
        else 
        {
            Debug.Log("PATHFINDER BUG BUG BUG");
        }

    }

    void 
        OpenNode(int posX, int posY, float newCost, Node parent)
    {
        if (posX < 0 || posX > 49 || posY < 0 || posY > 49)
        {
            //Debug.Log("X:" + posX + "Y:" + posY);
            return;
        }

        if (SharedData.instance.gridmesh.gridmesh[posX, posY])
        {
            //Debug.Log("Index X:" + posX + "Index Z: " + posY);
            if (SharedData.instance.gridmesh.gridmesh[posX, posY].GetComponent<Grid>().state == Grid.GRID_STATE.UNAVAILABLE)
            {
                return;
            }
            else 
            {
                //Debug.Log();
            }
            //Grid not walkable
        }
        Node newNode = new Node();
        newNode.posX = posX;
        newNode.posY = posY;
        foreach (Node node in VisitedList)
        {
            if (node.GetNodeID() == newNode.GetNodeID())
            {
                // Dont retrace back
                return;
            }
        }
        newNode.parent = parent;
        newNode.G = newCost;
        newNode.H = newNode.Distance(EndNode);

        foreach(Node node in OpenList)
        {
            // if new adjacent node is already in the openlist
            // check to see if current processing path to adjacent node is shorter than prev path
            if (newNode.GetNodeID() == node.GetNodeID())
            {
                float newF = newNode.G + newCost + newNode.H;
                if (node.getF() > newF)
                {
                    node.G = newNode.G + newCost;
                    node.parent = parent;
                }
                else 
                {
                    return;
                }
            }
        }
        Grid tempGrid = SharedData.instance.gridmesh.gridmesh[posX, posY].GetComponent<Grid>();
        tempGrid.GetComponent<Grid>().ChangeState(Grid.GRID_STATE.INOPENLIST);
        OpenList.Add(newNode);
    }

    Node getNextNodeFromOpenList()
    {
        float lowestF = 9999;
        Node nextnode = null;

        foreach(Node node in OpenList)
        {
            if (node.getF() < lowestF)
            {
                lowestF = node.getF();
                nextnode = node;
            }
            else if (node.getF() == lowestF)
            {
                nextnode = node;
            }
        }

        if (nextnode != null)
        {
            OpenList.Remove(nextnode);
            Grid tempGrid = SharedData.instance.gridmesh.gridmesh[nextnode.posX, nextnode.posY].GetComponent<Grid>();
            tempGrid.GetComponent<Grid>().ChangeState(Grid.GRID_STATE.INCLOSELIST);
            VisitedList.Add(nextnode);
        }

        return nextnode;

    }

	// Update is called once per frame
	void Update ()
    {
        if (!PathFound)
        {
            FindPath(StartPos, EndPos);
        }
        else 
        {
            if (ResetTimer > 3f)
            {
                ResetTimer = 0f;
                EndPos.Set(Random.Range(50, 450), 0, Random.Range(50, 450));
                while (SharedData.instance.gridmesh.GetGridAtPosition(EndPos).GetComponent<Grid>().state == Grid.GRID_STATE.UNAVAILABLE)
                {
                    EndPos.Set(Random.Range(50, 450), 0, Random.Range(50, 450));
                }
                Reset();
            }
            else 
            {
                ResetTimer += Time.deltaTime;
            }
        }
	}
}
