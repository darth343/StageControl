using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SharedData : MonoBehaviour {
    public static SharedData instance = null;
    public GridArray gridmesh;
    public Text debuginfo;
    public Terrain ground;
    
    //handlers
    public HandHandler handhandler;
    public DragHandler draghandler;
    public Instantiation buildhandler;


    public bool isHoldingCard = false;
	// Use this for initialization
	void Start ()
    {
	    if(instance == null)
        {
            instance = this;
        }else
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
