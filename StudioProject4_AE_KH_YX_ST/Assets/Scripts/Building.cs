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
    public Material holo,undamage,damaged;
    BUILDSTATE b_state;
	// Use this for initialization
	void Start () {
        b_state = BUILDSTATE.B_HOLOGRAM;
	
	}
	
	// Update is called once per frame
	void Update () {

        //switch(b_state)
        //{
        //    case BUILDSTATE.B_HOLOGRAM:
        //        gameObject.transform.GetChild(0).GetComponent<Renderer>().material = holo;

        //        break;
        //    case BUILDSTATE.B_CONSTRUCT:
        //        gameObject.transform.GetChild(0).GetComponent<Renderer>().material = undamage;

        //        break;


        //}
	
	}
}
