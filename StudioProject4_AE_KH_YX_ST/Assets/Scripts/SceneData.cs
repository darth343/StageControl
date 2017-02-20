using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneData : MonoBehaviour
{
    public static SceneData sceneData;
    public GameObject EntityList;
    public Canvas UI;
    public GridArray gridmesh;
    public Text debuginfo;
    public Terrain ground;


    //Decks
    public GameObject PlayerDeck = null;
    public bool isHoldingCard = false;

    //handlers
    public HandHandler handhandler;
    public DragHandler draghandler;

    //Flocking Settings
    public float CohesionWeight = 1f;
    public float SeperationWeight = 1f;
    public float AlignmentWeight = 1f;

    void Start()
    {
        sceneData = this;
    }
}
