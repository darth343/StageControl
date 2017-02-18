using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
    //base class for all buildings
    //call the spawning stuff here samuel
    enum BUILDSTATE
    {
        B_HOLOGRAM,
        B_CONSTRUCT,
        B_ACTIVE
    };
    //GameObject Unit;//the unit that this building spawns
    public float buildtime, spawntime;//time to construct the building/time it takes to spawna asingle unit
    public int size;//building size
    BUILDSTATE b_state;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
