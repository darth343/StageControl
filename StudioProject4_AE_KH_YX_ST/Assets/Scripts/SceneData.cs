using UnityEngine;
using System.Collections;

public class SceneData : MonoBehaviour
{
    public static SceneData sceneData;
    public GameObject EntityList;

    //Flocking Settings
    public float CohesionWeight = 1f;
    public float SeperationWeight = 1f;
    public float AlignmentWeight = 1f;

    void Start()
    {
        sceneData = this;
    }
}
