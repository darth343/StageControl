using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
    //base class for all buildings
    //call the spawning stuff here samuel
    public enum BUILDSTATE
    {
        B_HOLOGRAM,
        B_CONSTRUCT,
        B_ACTIVE
    };
    //public GameObject Unit; //the unit that this building spawns, spawn script already requires a unit
    public float buildtime, spawntime;// time to construct the building/time it takes to spawn a single unit
    public int size;//building size
    public Material holo,undamaged,damaged;
    public BUILDSTATE b_state;
    public bool isfriendly;
	// Use this for initialization
	void Start () {
        //b_state = BUILDSTATE.B_HOLOGRAM;
        
        
        isfriendly = true;//default to the player's units
	
	}
	
	// Update is called once per frame
	void Update () {

        switch (b_state)
        {
            case BUILDSTATE.B_HOLOGRAM:
                for (int i = 0; i < gameObject.transform.GetChild(0).childCount; ++i)
                {
                    gameObject.transform.GetChild(0).transform.GetChild(i).GetComponent<MeshRenderer>().material = holo;

                }

                break;
            case BUILDSTATE.B_CONSTRUCT:
                for (int i = 0; i < gameObject.transform.GetChild(0).childCount; ++i)
                {
                    gameObject.transform.GetChild(0).transform.GetChild(i).GetComponent<MeshRenderer>().material = undamaged;
                }
                break;
            case BUILDSTATE.B_ACTIVE:
                for (int i = 0; i < gameObject.transform.GetChild(0).childCount; ++i)
                {
                    gameObject.transform.GetChild(0).transform.GetChild(i).GetComponent<MeshRenderer>().material = undamaged;
                }


                break;


        }
	
	}

    public void SetBuilding()
    {
        b_state = BUILDSTATE.B_CONSTRUCT;


    }
}
