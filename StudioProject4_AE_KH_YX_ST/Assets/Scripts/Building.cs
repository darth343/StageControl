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
    //public GameObject Unit; //the unit that this building spawns, spawn script already requires a unit
    public float buildtime, spawntime;// time to construct the building/time it takes to spawn a single unit
    public int size;//building size
    BUILDSTATE b_state;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
